namespace Paul.PhysicsSimulator.Engine.Gaming
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Paul.PhysicsSimulator.Engine.Physics.Particles;

    /// <summary>
    /// Graphics object that have to be rendered
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// Get or Set if the object have to be drawn or it's disabled
        /// </summary>
        bool IsEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Draw the object on the screen
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <param name="cameraView"></param>
        /// <param name="cameraProjection"></param>        
        void Draw(GraphicsDevice graphicsDevice, Matrix cameraView, Matrix cameraProjection);
       
    }
}
