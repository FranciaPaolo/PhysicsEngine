namespace Paul.PhysicsSimulator.Engine.Physics.Force
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Paul.PhysicsSimulator.Engine.Physics.Particles;
    using Paul.PhysicsSimulator.Engine.Physics.RigidBodies;
    using Paul.PhysicsSimulator.Engine.Physics.Commons;

    /// <summary>
    /// Manatains a registry to list all the active forces applyied to the objects.
    /// </summary>
    public class ForceRegistry 
    {
        /// <summary>
        /// Registration that hold the force generator and the object where the force is applied
        /// </summary>
        private class ForceRegistration
        {
            public IPhysicsObject physicsComponent;
            public IForceGenerator fg;
            public ForceRegistration()
            {
                physicsComponent = null;
                fg = null;
            }
            public ForceRegistration(IPhysicsObject component, IForceGenerator forceGenerator)
            {
                physicsComponent = component;
                fg = forceGenerator;
            }
        }

        /// <summary>
        /// Create the Force Registry
        /// </summary>
        public ForceRegistry()
        {
            registrations=new List<ForceRegistration>();
        }    
        private List<ForceRegistration> registrations;

        /// <summary>
        /// Add a force generator to a physiscs compoment
        /// </summary>
        /// <param name="component">Physiscs component to apply the force</param>
        /// <param name="forceGenerator">Force generator</param>
        public void Add(IPhysicsObject component, IForceGenerator forceGenerator)
        {
            registrations.Add(new ForceRegistration(component, forceGenerator));
        }

        /// <summary>
        /// Remove the force generator from the specified object
        /// </summary>
        /// <param name="component">Componen on which remove the force generator</param>
        /// <param name="forceGenerator">Force generator to remove</param>
        public void Remove(IPhysicsObject component, IForceGenerator forceGenerator)
        {
            registrations.Remove(new ForceRegistration(component, forceGenerator));
        }

        /// <summary>
        /// Remove all applied forces from an object
        /// </summary>
        /// <param name="component">Physics object on wich remove the forces</param>
        public void RemoveAll(IPhysicsObject component)
        {
            List<ForceRegistration> registrationToDelete=registrations.Where(w=>w.physicsComponent==component).ToList();
            for (int i = 0; i < registrationToDelete.Count;i++)
                registrations.Remove(registrationToDelete[i]);
        }

        /// <summary>
        /// Remove all registered forces
        /// </summary>
        public void Clear()
        {
            registrations.Clear();
        }

        /// <summary>
        /// Applica le forze registrate
        /// </summary>
        /// <param name="secondElapsed">Seconds since last update</param>
        public void Update(float secondElapsed)
        {
            foreach(ForceRegistration registry in registrations)
                registry.fg.UpdateForce(registry.physicsComponent, secondElapsed);
        }        
    }
}
