namespace Paul.PhysicsSimulator.Engine.Graphics.Shapes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Paul.PhysicsSimulator.Engine.Physics.Particles;    

    /// <summary>
    /// Rectangle that use a Particle model
    /// </summary>
    public class Rettangle : Paul.PhysicsSimulator.Engine.Gaming.IDrawable, IComponentParticle
    {
        private VertexPositionTexture[] vertexPos;
        private Vector4 color;
        private float dimensionX;
        private float dimensionZ;
        private Effect effect;

        /// <summary>
        /// Get or Set if the object have to be drawn or it's disabled
        /// </summary>
        public bool IsEnabled
        {
            get;
            set;
        }
        /// <summary>
        /// Dimension along the X axis
        /// </summary>
        public float DimensionX
        {
            get
            {
                return dimensionX;
            }
            set
            {
                dimensionX = value;
            }
        }
        /// <summary>
        /// Dimension along the Z axis
        /// </summary>
        public float DimensionZ
        {
            get
            {
                return dimensionZ;
            }
            set
            {
                dimensionZ = value;
            }
        }        

        /// <summary>
        /// Particle Physics component
        /// </summary>
        public Particle Particle
        {
            get;
            set;
        }

        /// <summary>
        /// Xna Model of the object
        /// </summary>
        public Model Model
        {
            get;
            set;
        }

        public Rettangle(Game game, float dimensionX, float dimensionZ,  Color color)
        {            
            this.color = color.ToVector4();
            this.dimensionX = dimensionX;
            this.dimensionZ = dimensionZ;
            this.effect = game.Content.Load<Effect>("Effect\\OneColor");

            this.Particle = new Particle();
            vertexPos = new VertexPositionTexture[4];
            vertexPos[0].Position = new Vector3(0, 0, dimensionX);
            vertexPos[1].Position = new Vector3(0, 0, 0);
            vertexPos[2].Position = new Vector3(dimensionZ, 0, dimensionX);            
            vertexPos[3].Position = new Vector3(dimensionZ, 0, 0);
        }

        /// <summary>
        /// Draw the object on the screen
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <param name="cameraView"></param>
        /// <param name="cameraProjection"></param>   
        public void Draw(GraphicsDevice graphicsDevice, Matrix cameraView, Matrix cameraProjection)
        {
            //graphicsDevice.RasterizerState=RasterizerState.CullNone;
            
            //Disegno il rettangolo
            effect.CurrentTechnique = effect.Techniques["Textured"];
            effect.Parameters["xWorldViewProjection"].SetValue(this.Particle.WorldTrasformation * cameraView * cameraProjection);
            effect.Parameters["coloreDiffuso"].SetValue(color);

            effect.CurrentTechnique.Passes[0].Apply();
            effect.CurrentTechnique.Passes[0].Apply();
            graphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleStrip, vertexPos, 0, 2);
        }
    }
}
