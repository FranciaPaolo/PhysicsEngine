namespace Paul.PhysicsSimulator.Engine.Physics.Force
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Paul.PhysicsSimulator.Engine.Physics.Particles;
    using Paul.PhysicsSimulator.Engine.Physics.RigidBodies;
    using Paul.PhysicsSimulator.Engine.Physics.Commons;

    /// <summary>
    /// Component that apply force to a physics object
    /// </summary>
    public interface IForceGenerator
    {
        /// <summary>
        /// Generate and apply force to the object
        /// </summary>
        /// <param name="component">Object to apply forces</param>
        /// <param name="secondElapsed">Seconds since last update</param>
        void UpdateForce(IPhysicsObject component, float secondElapsed);
    }
}
