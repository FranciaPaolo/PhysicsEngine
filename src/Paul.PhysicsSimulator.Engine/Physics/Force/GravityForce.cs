using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paul.PhysicsSimulator.Engine.Physics.Particles;
using Paul.PhysicsSimulator.Engine.Physics.RigidBodies;
using Paul.PhysicsSimulator.Engine.Physics.Commons;

namespace Paul.PhysicsSimulator.Engine.Physics.Force
{
    /// <summary>
    /// Apply the gravity force to the object
    /// </summary>
    public class GravityForce : IForceGenerator
    {
        /// <summary>
        /// Gravity constant
        /// </summary>GravityForce
        public static Vector3 gravityConstant=new Vector3(0.0f, -15.0f, 0.0f);
        private Vector3 gravity;

        public GravityForce(Vector3 gravity)
        {
            this.gravity = gravity;
        }

        /// <summary>
        /// Generate and apply force to the object
        /// </summary>
        /// <param name="component">Object to apply forces</param>
        /// <param name="secondElapsed">Seconds since last update</param>
        public void UpdateForce(IPhysicsObject component, float secondElapsed)
        {
            //if has finite mass
            component.AddForce(gravity * component.Mass);
        }
    }
}
