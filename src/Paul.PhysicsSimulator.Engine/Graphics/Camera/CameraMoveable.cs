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

namespace Paul.PhysicsSimulator.Engine.Graphics.Camera
{
    /// <summary>
    /// Camera that can be moved, traslating or rotating, using the designed methods 
    /// </summary>
    public class CameraMoveable: ICamera
    {
        #region ICamera_Methods

        /// <summary>
        /// Current View of the camera
        /// </summary>
        public Matrix CameraView { get; protected set; }
        /// <summary>
        /// Current Projection of the camera
        /// </summary>
        public Matrix CameraProjection { get; protected set; }

        #endregion
        
        /// <summary>
        /// Position in the wolrd of the camera
        /// </summary>
        public Vector3 CameraPosition { get; private set; }

        /// <summary>
        /// Vector that represent the direction where the camera is looking for
        /// </summary>
        public Vector3 CameraDirection { get; private set; }

        //State of the rotation 
        private float currentYaw = 0;
        private float currentPitch = 0;
        private float currentRoll = 0;
        //Up direction of the camera
        private Vector3 cameraUp;

        /// <summary>
        /// Crea una camera base
        /// </summary>
        /// <param name="cameraPosition">World coordinate of the camera</param>
        /// <param name="targetPoint">World coordinate of the point where the camera is looking</param>
        /// <param name="up">Up direction</param>
        /// <param name="nearPlane">Near plane distance</param>
        /// <param name="farPlane">Far plane distance</param>
        /// <param name="aspectRatio">Game.Window.ClientBounds.Width/Game.Window.ClientBounds.Height</param>
        public CameraMoveable(Vector3 cameraPosition, Vector3 targetPoint, Vector3 up, float nearPlane, float farPlane, float aspectRatio)
        {
            CameraPosition = cameraPosition;
            CameraDirection = targetPoint - cameraPosition;
            CameraDirection.Normalize();
            cameraUp = up;         

            CreateLookAt();
            CameraProjection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, nearPlane, farPlane);
        }

        private void CreateLookAt()
        {
            CameraView = Matrix.CreateLookAt(CameraPosition, CameraPosition + CameraDirection, cameraUp);
        }

        /// <summary>
        /// Rotate around Y (relative) axis
        /// </summary>
        /// <param name="offset">amount of rotation</param>
        public void RotateYaw(float offset)
        {            
            CameraDirection = Vector3.Transform(CameraDirection, Matrix.CreateFromAxisAngle(cameraUp, offset));
            currentYaw += offset;
            CreateLookAt();
        }
        /// <summary>
        /// Rotate around Z (relative) axis
        /// </summary>
        /// <param name="offset">amount of rotation</param>
        public void RotatePitch(float offset)
        {
            CameraDirection = Vector3.Transform(CameraDirection, Matrix.CreateFromAxisAngle(Vector3.Cross(CameraDirection, cameraUp), offset));
            currentPitch += offset;
            CreateLookAt();
        }
        /// <summary>
        /// Rotate around X (relative) axis
        /// </summary>
        /// <param name="offset">amount of rotation</param>
        public void RotateRoll(float offset)
        {
            cameraUp = Vector3.Transform(cameraUp, Matrix.CreateFromAxisAngle(CameraDirection, offset));            
            currentRoll += offset;
            CreateLookAt();
        }

        /// <summary>
        /// Move backward the camera
        /// </summary>
        /// <param name="offset">amount of movement</param>
        public void MoveBackwardDirection(float offset)
        {
            CameraPosition -= CameraDirection * offset;
            CreateLookAt();
        }
        /// <summary>
        /// Move forward the camera
        /// </summary>
        /// <param name="offset">amount of movement</param>
        public void MoveForwardDirection(float offset)
        {
            CameraPosition += CameraDirection * offset;
            CreateLookAt();
        }
        /// <summary>
        /// Move right the camera
        /// </summary>
        /// <param name="offset">amount of movement</param>
        public void MoveRightDirection(float offset)
        {
            CameraPosition += Vector3.Cross(CameraDirection, cameraUp)*offset;
            CreateLookAt();
        }
        /// <summary>
        /// Move left the camera
        /// </summary>
        /// <param name="offset">amount of movement</param>
        public void MoveLeftDirection(float offset)
        {
            CameraPosition -= Vector3.Cross(CameraDirection, cameraUp) * offset;
            CreateLookAt();
        }
        /// <summary>
        /// Move upward the camera
        /// </summary>
        /// <param name="offset">amount of movement</param>
        public void MoveUpwardDirection(float offset)
        {
            CameraPosition += cameraUp * offset;
            CreateLookAt();
        }
        /// <summary>
        /// Move downward the camera
        /// </summary>
        /// <param name="offset">amount of movement</param>
        public void MoveDownwardDirection(float offset)
        {
            CameraPosition -= cameraUp * offset;
            CreateLookAt();
        }
    }
}