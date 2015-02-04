namespace Paul.PhysicsSimulator.Game.Scenes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Paul.PhysicsSimulator.Engine.Graphics.Camera;

    /// <summary>
    /// Game scene
    /// </summary>
    public interface IGameScene
    {
        ICamera DefaultCamera{get;}

        /// <summary>
        /// Create the scene objects
        /// </summary>
        void InitializeScene();

        /// <summary>
        /// Update of the gamePlay
        /// </summary>
        /// <param name="gameTime"></param>
        void Update(GameTime gameTime);

        /// <summary>
        /// Draw custom objects
        /// </summary>
        /// <param name="gameTime"></param>
        void Draw(GameTime gameTime);
    }
}
