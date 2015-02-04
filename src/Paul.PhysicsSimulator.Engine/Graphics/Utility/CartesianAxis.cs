namespace Paul.PhysicsSimulator.Engine.Graphics.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Paul.PhysicsSimulator.Engine.Graphics.Camera;

    /// <summary>
    /// DrawableGameComponent that draw the cartesian axis on the screen
    /// </summary>
    public class CartesianAxis: Paul.PhysicsSimulator.Engine.Gaming.IDrawable, Paul.PhysicsSimulator.Engine.Gaming.IGameComponent
    {
        #region Fields
        VertexPositionColor[] axisX;// Primitive to draw the x axis
        VertexPositionColor[] axisY;// Primitive to draw the y axis
        VertexPositionColor[] axisZ;// Primitive to draw the z axis
        
        BasicEffect basicEffect;// Rendering effect
        private ICamera camera;// Perspective camera

        #endregion

        /// <summary>
        /// Get or Set if the object have to be drawn or it's disabled
        /// </summary>
        public bool IsEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Create the axis
        /// </summary>
        /// <param name="graphicsDevice">Used to create the graphics objects</param>
        /// <param name="axisLenght">Positive lenght of the axis</param>
        /// <param name="coloAxisX">Color of the x axis</param>
        /// <param name="coloAxisY">Color of the y axis</param>
        /// <param name="coloAxisZ">Color of the z axis</param>
        /// <param name="camera">Camera used to draw the axis in projection</param>
        public CartesianAxis(GraphicsDevice graphicsDevice, float axisLenght, Color coloAxisX, Color coloAxisY, Color coloAxisZ, ICamera camera)            
        {
            this.camera = camera;

            axisX = new VertexPositionColor[2];
            axisX[0] = new VertexPositionColor(new Vector3(0.0f, 0.0f, 0.0f), coloAxisX);
            axisX[1] = new VertexPositionColor(new Vector3(axisLenght, 0.0f, 0.0f), coloAxisX);
            axisY = new VertexPositionColor[2];
            axisY[0] = new VertexPositionColor(new Vector3(0.0f, 0.0f, 0.0f), coloAxisY);
            axisY[1] = new VertexPositionColor(new Vector3(0.0f, axisLenght, 0.0f), coloAxisY);
            axisZ = new VertexPositionColor[2];
            axisZ[0] = new VertexPositionColor(new Vector3(0.0f, 0.0f, 0.0f), coloAxisZ);
            axisZ[1] = new VertexPositionColor(new Vector3(0.0f, 0.0f, axisLenght), coloAxisZ);
            this.IsEnabled = true;
            basicEffect = new BasicEffect(graphicsDevice);
            updateEffect();
        }

        
        /// <summary>
        /// Draw the object
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <param name="cameraView"></param>
        /// <param name="cameraProjection"></param>        
        public void Draw(GraphicsDevice graphicsDevice, Matrix cameraView, Matrix cameraProjection)
        {
            updateEffect();
            basicEffect.CurrentTechnique.Passes[0].Apply();
            graphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, axisX, 0, 1);
            graphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, axisY, 0, 1);
            graphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, axisZ, 0, 1);
        }

        /// <summary>
        /// Update the graphics properties based on the camera matrices
        /// </summary>
        private void updateEffect()
        {
            basicEffect.VertexColorEnabled = true;
            basicEffect.View = camera.CameraView;
            basicEffect.Projection = camera.CameraProjection;
        }

    }
}
