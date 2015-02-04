namespace Paul.PhysicsSimulator.Engine.Physics.Particles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.GamerServices;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Media;
    using Paul.PhysicsSimulator.Engine.Physics.Commons;    

    /// <summary>
    /// Phisics component modeled by RigidBody
    /// </summary>
    public class Particle: IPhysicsObject
    {
        #region Properties
        private Vector3 position;
        
        private Vector3 forceAccum;
        protected float damping;
        protected float mass;
        protected float inverseMass;

        /// <summary>
        /// Position of the particle
        /// </summary>
        public Vector3 Position
        {
            get
            {
                return position;
            }
            set {
                position = value;
                this.CalculateWorldTrasformationMatrix();
            }
        }
        /// <summary>
        /// Velocity of the particle
        /// </summary>
        public Vector3 Velocity
        {
            get;
            set;
        }
        /// <summary>
        /// Acceleration of the body
        /// </summary>
        public Vector3 LinearAcceleration
        {
            get;
            set;
        }        
        /// <summary>
        /// Holdds the amount of damping (smorzamento) applied to linear motion.
        /// Damping is required to remove energy added through numerical instability in the integrator.
        /// </summary>
        public float Damping
        {
            get
            { return damping; }       
            set
            { damping=value; }       
        }

        /// <summary>
        /// Get or Set the mass of the body
        /// </summary>
        public float Mass
        {
            get
            {
                return mass;
            }
            set
            {
                if (value <= 0)
                {
                    mass = 0;
                    inverseMass = 0;
                }
                else
                {
                    mass = value;
                    inverseMass = 1 / mass;
                }
            }
        }
        /// <summary>
        /// Get or Set the inverse of the mass of the object
        /// </summary>
        public float InverseMass
        {
            get
            {
                return inverseMass;
            }
        }

        /// <summary>
        /// Holds a tranform matrix for converting body space into world space and vice versa.
        /// </summary>
        public Matrix WorldTrasformation
        { get; private set; }
        #endregion

        /// <summary>
        /// Create the particle object
        /// </summary>
        public Particle()
        {
            Position = Vector3.Zero;
            Velocity = Vector3.Zero;
            LinearAcceleration = Vector3.Zero;
            damping = 0.95f;
            mass = 1;
            inverseMass = 1;
        }

        #region Methods

        /// <summary>
        /// Update the position, velocity and acceleration based on the applied forces
        /// </summary>
        /// <param name="secondsElapsed">Second since last Update</param>
        public void Update(float secondsElapsed)
        {            
            IntegrateNewtonEuler(secondsElapsed);            
        }

        /// <summary>
        /// Update position, velocity, orientation and rotation of the body
        /// </summary>
        /// <param name="secondsElapsed">Seconds from last integration</param>
        private void IntegrateNewtonEuler(float secondsElapsed)
        {
            if (secondsElapsed <= 0.0f)
                return;

            LinearAcceleration = (forceAccum * inverseMass);// a=a0+F risultante
            Velocity += LinearAcceleration * secondsElapsed;//v'=v0+at
            Position += Velocity * secondsElapsed;//p'=p0+vt (velocità approssimata costante)
            //Velocity *= (float)Math.Pow(damping, duration);

            forceAccum = Vector3.Zero;
        }

        /// <summary>
        /// Update position, velocity, orientation and rotation of the body
        /// </summary>
        /// <param name="secondsElapsed">Seconds from last integration</param>
        private void IntegratePaul(float secondsElapsed)
        {
            if (secondsElapsed <= 0.0f)
                return;
            
            Vector3 resultingAcc = forceAccum * inverseMass;
            Position =Position+ Velocity * secondsElapsed + (0.5f*secondsElapsed*secondsElapsed*resultingAcc);
            Velocity += resultingAcc * secondsElapsed;

            forceAccum = Vector3.Zero;
        }
        
        /// <summary>
        /// Add the force to the object. It will be removed in the update method.
        /// </summary>
        /// <param name="force">Vector amount of force</param>
        public void AddForce(Vector3 force)
        {
            this.forceAccum += force;
        }

        /// <summary>
        /// Calculate the trasformation matrix that enable tranformation from bodySpace to worldSpace
        /// </summary>
        public void CalculateWorldTrasformationMatrix()
        {
            WorldTrasformation = Matrix.Identity                
                //* Matrix.CreateRotationY(rotation.Y)
                * Matrix.CreateTranslation(position);
        }

        #endregion
    }
}