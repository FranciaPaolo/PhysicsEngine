namespace Paul.PhysicsSimulator.Engine.Graphics.Shapes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Paul.PhysicsSimulator.Engine.Physics.Particles;    
    using Paul.PhysicsSimulator.Engine.Physics.RigidBodies;
    using Paul.PhysicsSimulator.Engine.Gaming;
    using Paul.PhysicsSimulator.Engine.Physics.Commons;

    /// <summary>
    /// Parallelepiped
    /// </summary>
    public class Cube:  Paul.PhysicsSimulator.Engine.Gaming.IDrawable, 
                        IRigidBodyComponent, 
                        Paul.PhysicsSimulator.Engine.Gaming.IGameComponent
    {
        private float dimensionX;
        private float dimensionY;
        private float dimensionZ;
        private Vector3 color;
        private bool useDiffuseColor;
        private bool enableDefaultLight;

        /// <summary>
        /// Get or Set if the object have to be drawn or it's disabled
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Get the lenght of the parallelepiped along X axis
        /// </summary>
        public float DimensionX
        {
            get
            {
                return dimensionX;
            }
        }
        /// <summary>
        /// Get the lenght of the parallelepiped along Y axis
        /// </summary>
        public float DimensionY
        {
            get
            {
                return dimensionY;
            }
        }
        /// <summary>
        /// Get the lenght of the parallelepiped along Z axis
        /// </summary>
        public float DimensionZ
        {
            get
            {
                return dimensionZ;
            }
        }

        /// <summary>
        /// Xna Model of the object
        /// </summary>
        public Model Model
        {
            get;
            private set;
        }

        /// <summary>
        /// RigidBody Physics component
        /// </summary>
        public RigidBody RigidBody { get; set; }

        /// <summary>
        /// Istantiate the a new parallelepiped
        /// </summary>
        /// <param name="dimension">Vector that represent the dimesions of the object</param>
        /// <param name="game">Xna Game required to load from the content Pipeline the model "Models\Cubo"</param>
        /// <param name="modelPath">Path to the 3d model in the content pipeline</param>
        /// <param name="color">Color of the object</param>
        public Cube(Game game, Vector3 dimension, string modelPath, Color? color, bool enableLight):base()
        {
            this.dimensionX = dimension.X;
            this.dimensionY = dimension.Y;
            this.dimensionZ = dimension.Z;
            this.enableDefaultLight = enableLight;
            this.useDiffuseColor = color.HasValue;
            if (color.HasValue)
                this.color = color.Value.ToVector3();
            this.Model = game.Content.Load<Model>(modelPath);
            this.RigidBody = new RigidBody();
            this.IsEnabled = true;
        }

        /// <summary>
        /// Draw the object on the screen
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <param name="cameraView"></param>
        /// <param name="cameraProjection"></param>   
        public void Draw(GraphicsDevice graphicsDevice, Matrix cameraView, Matrix cameraProjection)
        {
            //Copy any parent transforms
            Matrix[] transforms = new Matrix[this.Model.Bones.Count];
            this.Model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in this.Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    //effect.EnableDefaultLighting();
                    
                    if(this.useDiffuseColor)
                        effect.DiffuseColor = this.color;
                    effect.World = transforms[mesh.ParentBone.Index] //*
                        //Matrix.CreateFromYawPitchRoll(gameobject.rotation.Y, gameobject.rotation.X, gameobject.rotation.Z)
                        * Matrix.CreateScale(dimensionX, dimensionY, dimensionZ)
                        * this.RigidBody.WorldTrasformation;
                    effect.View = cameraView;
                    effect.Projection = cameraProjection;
                }
                mesh.Draw();
            }
        }
    }
}
