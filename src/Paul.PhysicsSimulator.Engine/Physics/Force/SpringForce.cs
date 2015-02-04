namespace Paul.PhysicsSimulator.Engine.Physics.Force
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Paul.PhysicsSimulator.Engine.Physics.Particles;
    using Paul.PhysicsSimulator.Engine.Physics.Commons;

    /// <summary>
    /// Generate an elastic force applied to the object
    /// </summary>
    public class SpringForce:IForceGenerator
    {
        float springConstant;
        float restLength;
        IPhysicsObject other;

        /// <summary>
        /// Create the elastic force generator
        /// </summary>
        /// <param name="other">Other object where the spring is attached</param>
        /// <param name="springConstant">Elastic costant</param>
        /// <param name="restLength">Initial lenght, or rest lenght, of the spring</param>
        public SpringForce(IPhysicsObject other,float springConstant,float restLength)
        {
            this.other = other;
            this.springConstant = springConstant;
            this.restLength = restLength;
        }

        /// <summary>
        /// Generate and apply force to the object
        /// </summary>
        /// <param name="component">Object to apply forces</param>
        /// <param name="secondElapsed">Seconds since last update</param>
        public void UpdateForce(IPhysicsObject component, float secondElapsed)
        {
            Vector3 force = component.Position - other.Position;

            //Calculate magnitude of the force
            float magnitude = force.Length();//l
            magnitude = (magnitude - restLength) * springConstant;//|l-l0|            

            force.Normalize();
            force *= -magnitude;

            component.AddForce(force);
        }   
    }
    /// <summary>
    /// Generate an elastic force applied to the object anchored to a point
    /// TODO: understand if this force generator is no more required
    /// </summary>
    public class AnchoredSpringForce : IForceGenerator
    {
        float springConstant;
        float restLength;
        Vector3 other;

        /// <summary>
        /// Create the elastic force generator
        /// </summary>
        /// <param name="other">Other object where the spring is attached</param>
        /// <param name="springConstant">Elastic costant</param>
        /// <param name="restLength">Initial lenght, or rest lenght, of the spring</param>
        public AnchoredSpringForce(Vector3 other, float springConstant, float restLength)
        {
            this.other = other;
            this.springConstant = springConstant;
            this.restLength = restLength;
        }

        /// <summary>
        /// Generate and apply force to the object
        /// </summary>
        /// <param name="component">Object to apply forces</param>
        /// <param name="secondElapsed">Seconds since last update</param>
        public void UpdateForce(IPhysicsObject component, float secondElapsed)
        {
            Vector3 force = component.Position - other;

            //Calculate magnitude of the force
            float magnitude = force.Length();//l
            magnitude =  (magnitude - restLength) * springConstant;//|l-l0|
            magnitude *= springConstant;

            force.Normalize();
            force *= -magnitude;

            component.AddForce(force);
        }
    }
}
