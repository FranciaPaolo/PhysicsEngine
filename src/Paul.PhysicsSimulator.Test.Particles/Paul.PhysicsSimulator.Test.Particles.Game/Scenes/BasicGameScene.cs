namespace Paul.PhysicsSimulator.Game.Scenes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Paul.PhysicsSimulator.Engine.Gaming;
    using Paul.PhysicsSimulator.Engine.Graphics.Camera;
    using Microsoft.Xna.Framework;
    using Paul.PhysicsSimulator.Engine.Graphics.Utility;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Basic game scene
    /// </summary>
    public class BasicGameScene:IGameScene
    {
        protected PhysicsGame physicsGame;
        protected FPSCameraMover fpsCameraMover;
        protected CartesianAxis cartesianAxis;
        protected Game xnaGame;

        public ICamera DefaultCamera { get { return fpsCameraMover.CameraMoveable; } }

        public BasicGameScene(PhysicsGame physicsGame, Game xnaGame)
        {
            this.physicsGame = physicsGame;
            this.xnaGame = xnaGame;

            Mouse.SetPosition(xnaGame.Window.ClientBounds.Width / 2, xnaGame.Window.ClientBounds.Height / 2);

            //Camera
            CameraMoveable camera = new CameraMoveable(new Vector3(0, 8, 12), new Vector3(0, 0, 0), Vector3.Up, 0.01f, 4000.0f, (float)xnaGame.Window.ClientBounds.Width / (float)xnaGame.Window.ClientBounds.Height);
            fpsCameraMover = new FPSCameraMover(xnaGame, camera);
            
            physicsGame.ActiveCamera = camera;

            //Cartesian Axis
            cartesianAxis = new CartesianAxis(xnaGame.GraphicsDevice, 1000, Color.Red, Color.Black, Color.Green, physicsGame.ActiveCamera);
             
        }

        /// <summary>
        /// Setup for the scene
        /// </summary>
        public virtual void InitializeScene()
        {
            physicsGame.AddComponent("fpsCameraMover", fpsCameraMover);
            physicsGame.AddComponent("axis", cartesianAxis);           
        }

        /// <summary>
        /// Update of the gamePlay
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        { }

        /// <summary>
        /// Draw custom objects
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Draw(GameTime gameTime)
        {
            this.xnaGame.GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.CornflowerBlue, 1.0f, 0);
            this.xnaGame.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
        }
    }
}
