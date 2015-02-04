namespace Paul.PhysicsSimulator.Engine.Physics.RigidBodies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Paul.PhysicsSimulator.Engine.Physics.Force;

    /// <summary>
    /// Manage a list of RigidBody: handle applied forces, run the necessary physics, integrates, resolve contacts.
    /// </summary>
    public class RigidBodyWorld : Paul.PhysicsSimulator.Engine.Gaming.IUpdateable, Paul.PhysicsSimulator.Engine.Gaming.IGameComponent
    {
        //List of RigidBody
        private List<RigidBody> rigidBodies;        
        private ForceRegistry forceRegistry;

        /// <summary>
        /// Get the forceRegistry
        /// </summary>
        public ForceRegistry ForceRegistry { get{return forceRegistry;}}

        /// <summary>
        /// Managed RigidBodies
        /// </summary>
        public IEnumerable<RigidBody> RigidBodies { get { return rigidBodies.AsEnumerable(); } }
        
        /// <summary>
        /// Create the manager that handles the RigidBodies
        /// </summary>
        /// <param name="maxContacts">Maximum of contacts managed in a single iteration</param>
        /// <param name="iteration">Number of iterations used to resolve contacts</param>
        public RigidBodyWorld(uint maxContacts, uint iteration)
        {
            rigidBodies = new List<RigidBody>();
            forceRegistry = new ForceRegistry();
        }
        
        /// <summary>
        /// Add a rigidBody object
        /// </summary>
        /// <param name="body">rigidBody object</param>
        internal void AddRigidBody(RigidBody body)
        {
            rigidBodies.Add(body);
        }
        /// <summary>
        /// Remove the rigidBody
        /// </summary>
        /// <param name="body">body to remove</param>
        internal void RemoveRigidBody(RigidBody body)
        {
            rigidBodies.Remove(body);
            forceRegistry.RemoveAll(body);
        }

        
        /// <summary>
        /// Processes all the physics for the particle world.
        /// </summary>
        /// <param name="secondsElapsed">Seconds since last run</param>
        private void RunPhysics(float secondsElapsed)
        {
            //Apply all force generators
            forceRegistry.Update(secondsElapsed);

            //Integrate to update position, velocity, acceleration and orientation
            Integrate(secondsElapsed);

            //TODO: implements contact generators and resolvers
            //Genera i contacts tra le particles
            //uint usedContacts = GenerateContacts();

            //Processa i contacts
            //if (calculateIterations) resolver.SetIterations(usedContacts * 2);   
            //if (usedContacts > 0)
            //    resolver.ResolveContacts(contacts, usedContacts, duration);

            // Svuota gli accumularori di forze all'interno delle particelle
            //for (int i = 0; i < rigidBodies.Count; i++)
            //    rigidBodies[i].ClearAccumulator();
        }

        /// <summary>
        /// Integrates all the particles in this world forward in time.
        /// </summary>
        /// <param name="secondsElapsed">Seconds since last Integration</param>
        private void Integrate(float secondsElapsed)
        {
            foreach (RigidBody rigidBody in rigidBodies)
            {
                if (rigidBody.InverseMass != 0)
                    rigidBody.Update(secondsElapsed);
            }
        }

        /// <summary>
        /// Update the status of the component: run the required physics
        /// </summary>
        /// <param name="secondElapsed">Time elapsed since last update (0 for the first time)</param>
        public void Update(float secondElapsed)
        {
            this.RunPhysics(secondElapsed);
        }
    }
}
