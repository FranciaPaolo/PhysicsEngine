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
    /// Simulate the resistence force that is opposite to the objects way. 
    /// F=-v(k1*v+k2*a)
    /// </summary>
    public class DragForce: IForceGenerator
    {
        float k1;
        float k2;

        /// <summary>
        /// Create the generator
        /// </summary>
        /// <param name="k1">Coefficient applied to the velocity</param>
        /// <param name="k2">Coefficient applied to the acceleration</param>
        public DragForce(float k1,float k2)
        {
            this.k1 = k1;
            this.k2 = k2;
        }

        /// <summary>
        /// Generate and apply force to the object
        /// </summary>
        /// <param name="component">Object to apply forces</param>
        /// <param name="secondElapsed">Seconds since last update</param>
        public void UpdateForce(IPhysicsObject component, float secondElapsed)
        {
            Vector3 force = component.Velocity;
            
            if (force != Vector3.Zero)
            {
                float dragCoeff = force.Length();
                dragCoeff = k1 * dragCoeff + k2 * dragCoeff * dragCoeff;

                force.Normalize();
                force *= -dragCoeff;
                component.AddForce(force);
            }
        }        
    }
}