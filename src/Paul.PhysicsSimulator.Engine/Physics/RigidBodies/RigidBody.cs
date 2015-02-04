namespace Paul.PhysicsSimulator.Engine.Physics.RigidBodies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Paul.PhysicsSimulator.Engine.Physics.Commons;

    /// <summary>
    /// Phisics component modeled by RigidBody
    /// </summary>
    public class RigidBody: IPhysicsObject
    {
        #region Properties
        private Vector3 position;
        private Quaternion orientation;
        private Vector3 rotation;
        private Vector3 angularAcceleration;

        private Vector3 forceAccum;
        private Vector3 torqueAccum;

        private float linearDamping;

        private float mass;
        private float inverseMass;

        private Matrix inertiaTensor;
        private Matrix inverseInertiaTensor;

        /// <summary>
        /// Position of the body
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
        /// Velocity of the body
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
        /// Holdds the amount of damping applied to linear motion.
        /// Damping is required to remove energy added through numerical instability in the integrator.
        /// </summary>
        public float LinearDamping
        {
            get
            { return linearDamping; }
            set
            { linearDamping = value; }
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
        /// Create the RigidBody object
        /// </summary>
        public RigidBody()
        {
            position = Vector3.Zero;
            Velocity = Vector3.Zero;
            orientation = Quaternion.Identity;
            rotation = Vector3.Zero;
            LinearAcceleration = Vector3.Zero;
            angularAcceleration = Vector3.Zero;            
            linearDamping = 0.95f;
            mass = 1;
            inverseMass = 1;
            inertiaTensor = Matrix.Identity;
            inverseInertiaTensor = Matrix.Identity;

            CalculateWorldTrasformationMatrix();
        }

        #region Methods
        
        /// <summary>
        /// Update the position, velocity and acceleration based on the applied forces
        /// </summary>
        /// <param name="secondsElapsed">Second since last Update</param>
        public void Update(float secondsElapsed)
        {
            IntegrateNewtonEuler(secondsElapsed);
            CalculateWorldTrasformationMatrix();
        }

        /// <summary>
        /// Add the torque to the object. It will be removed in the update method.
        /// </summary>
        /// <param name="torque"></param>
        public void AddTorque(Vector3 torque)
        {
            this.torqueAccum += torque;
        }

        /// <summary>
        /// Add the force to the object, applied to center of mass. It will be removed in the update method.
        /// </summary>
        /// <param name="force">Vector amount of force</param>
        public void AddForce(Vector3 force)
        {
            this.forceAccum += force;
        }

        /// <summary>
        /// Add the force to the object, applied to a specific point of the object. It will be removed in the update method.
        /// </summary>
        /// <param name="force">Vector amount of force</param>
        /// <param name="point">Vector that represent in Local space, relative to the object, the point where the force is applied.</param>
        public void AddForceAtBodyPoint(Vector3 force, Vector3 point)
        {
            Vector3 ptAtWorldSpace = GetPointInWorldSpace(point);
            
            //AddForceAtPoint(force, ptAtWorldSpace);

            Vector3 pt = ptAtWorldSpace - position;
            AddForce(force);
            AddTorque(Vector3.Cross(pt, force));
        }

        /// <summary>
        /// Update position, velocity, orientation and rotation of the body
        /// </summary>
        /// <param name="secondsElapsed">Seconds from last integration</param>
        private void IntegrateNewtonEuler(float secondsElapsed)
        {
            if (secondsElapsed <= 0.0f)
                return;

            //Integrazione della posizione lineare
            LinearAcceleration = forceAccum * inverseMass;// a=F/m risultante
            Velocity += LinearAcceleration * secondsElapsed;//v'=v0+at
            position += Velocity * secondsElapsed;//p'=p0+vt (velocità approssimata costante)            

            //Integrazione della rotazione
            angularAcceleration = Vector3.Transform(torqueAccum, inverseInertiaTensor);// o=t/I
            rotation += angularAcceleration * secondsElapsed;
            orientation *= Quaternion.CreateFromYawPitchRoll(rotation.Y * secondsElapsed, rotation.X * secondsElapsed, rotation.Z * secondsElapsed); // orientation.addScalarVector(rotation, secondsElapsed);
            orientation.Normalize();
            
            //angle += rotation.Y * secondsElapsed;

            forceAccum = Vector3.Zero;
            torqueAccum = Vector3.Zero;
        }

        /// <summary>
        /// Calculate the trasformation matrix that enable tranformation from bodySpace to worldSpace
        /// </summary>
        public void CalculateWorldTrasformationMatrix()
        {
            WorldTrasformation = Matrix.Identity
                * Matrix.CreateFromQuaternion(orientation)
                //* Matrix.CreateRotationY(rotation.Y)
                * Matrix.CreateTranslation(position);
        }

        /// <summary>
        /// Get the angle of orientation of the body
        /// </summary>
        /// <returns></returns>
        private float GetAngle()
        {
            return 2.0f * (float)Math.Acos(orientation.W);
        }

        /// <summary>
        /// Translate the point from bodySpace to world space
        /// </summary>
        /// <param name="point">Point in local coordinate to translate</param>
        /// <returns>Point in wolrd coordinate</returns>
        public Vector3 GetPointInWorldSpace(Vector3 point)
        {
            CalculateWorldTrasformationMatrix();
            return Vector3.Transform(point, WorldTrasformation);
        }
        #endregion
    }
}
