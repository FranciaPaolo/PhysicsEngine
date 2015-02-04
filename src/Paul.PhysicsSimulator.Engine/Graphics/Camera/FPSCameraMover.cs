namespace Paul.PhysicsSimulator.Engine.Graphics.Camera
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Use the Mouse and Keyboard Input to move the camera, like a First Person Shooter
    /// </summary>
    public class FPSCameraMover: Paul.PhysicsSimulator.Engine.Gaming.IUpdateable, Paul.PhysicsSimulator.Engine.Gaming.IGameComponent
    {
        private MouseState prevMouseState;
        private MouseState currMouseState;
        private KeyboardState prevKeyState;
        private KeyboardState currentKeyState;
        private Game xnaGame;
        private bool validState;

        /// <summary>
        /// Get or Set the camera object
        /// </summary>
        public CameraMoveable CameraMoveable { get; set; }

        /// <summary>
        /// Create the camera object
        /// </summary>
        /// <param name="xnaGame">Xna Game</param>
        /// <param name="cameraMoveable">Camaera Object to move</param>
        public FPSCameraMover(Game xnaGame, CameraMoveable cameraMoveable)            
        {
            this.CameraMoveable = cameraMoveable;
            this.xnaGame = xnaGame;
            this.validState = false;
        }

        /// <summary>
        /// Update the status of the component
        /// </summary>
        /// <param name="secondElapsed">Time elapsed since last update (0 for the first time)</param>
        public void Update(float secondElapsed)
        {
            currMouseState = Mouse.GetState();
            currentKeyState = Keyboard.GetState();

            //Camera Rotation Yaw
            if (this.validState && prevMouseState.X != currMouseState.X)
                CameraMoveable.RotateYaw(((float)prevMouseState.X - (float)currMouseState.X) * 0.005f);
            
            //Camera Rotation Pitch
            if (this.validState && prevMouseState.Y != currMouseState.Y)
                CameraMoveable.RotatePitch(((float)prevMouseState.Y - (float)currMouseState.Y) * 0.0005f);

            //Camera Translation
            if (prevKeyState.IsKeyDown(Keys.Left))
                CameraMoveable.MoveLeftDirection(0.05f);
            if (prevKeyState.IsKeyDown(Keys.Right))
                CameraMoveable.MoveRightDirection(0.05f);
            if (prevKeyState.IsKeyDown(Keys.Down))
                CameraMoveable.MoveBackwardDirection(0.05f);
            if (prevKeyState.IsKeyDown(Keys.Up))
                CameraMoveable.MoveForwardDirection(0.05f);

            prevMouseState = currMouseState;
            prevKeyState = currentKeyState;
            this.validState = true;
        }
    }
}
