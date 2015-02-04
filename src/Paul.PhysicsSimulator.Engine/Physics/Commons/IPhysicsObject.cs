namespace Paul.PhysicsSimulator.Engine.Physics.Commons
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Generic physics game object
    /// </summary>
    public interface IPhysicsObject
    {
        /// <summary>
        /// Position of the object
        /// </summary>
        Vector3 Position{get; set;}

        /// <summary>
        /// Velocity of the object
        /// </summary>
        Vector3 Velocity{get;set;}

        /// <summary>
        /// Add the force to the object. It will be removed in the update method.
        /// </summary>
        /// <param name="force">Vector amount of force</param>
        void AddForce(Vector3 force);

        /// <summary>
        /// Get or Set the mass of the body
        /// </summary>
        float Mass { get; set; }        
    }
}
