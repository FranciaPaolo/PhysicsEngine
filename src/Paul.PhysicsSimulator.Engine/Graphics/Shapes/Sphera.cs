namespace Paul.PhysicsSimulator.Engine.Graphics.Shapes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Paul.PhysicsSimulator.Engine.Physics.Particles;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Paul.PhysicsSimulator.Engine.Physics.RigidBodies;

    /// <summary>
    /// Sfera tridimensionale che eredita da particle
    /// </summary>
    public class Sphere :   Paul.PhysicsSimulator.Engine.Gaming.IDrawable, 
                            IRigidBodyComponent
    {        
        private float radius;
        private Vector3 color;

        /// <summary>
        /// Get or Set if the object have to be drawn or it's disabled
        /// </summary>
        public bool IsEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Get the radius of the sphere
        /// </summary>
        public float Radius
        {
            get
            {
                return radius;
            }
        }

        /// <summary>
        /// RigidBody Physics component
        /// </summary>
        public RigidBody RigidBody
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

        /// <summary>
        /// Create a new sphere object
        /// </summary>
        /// <param name="radius">Radius of the sphere</param>
        /// <param name="game"></param>
        /// <param name="color">Color of the object</param>
        public Sphere(Game game, float radius, Color color)
        {            
            this.radius = radius;//scale            
            this.color = color.ToVector3();
            this.RigidBody =null;
            this.Model = game.Content.Load<Model>("Models/Sfera");
        }

        /// <summary>
        /// Draw the object on the screen
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <param name="cameraView"></param>
        /// <param name="cameraProjection"></param>  
        public void Draw(GraphicsDevice graphicsDevice, Matrix cameraView, Matrix cameraProjection)
        {
            //Copy any parent transforms.
            Matrix[] transforms = new Matrix[this.Model.Bones.Count];
            this.Model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in this.Model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.DiffuseColor = this.color;
                    effect.World = transforms[mesh.ParentBone.Index] //*
                        //Matrix.CreateFromYawPitchRoll(gameobject.rotation.Y, gameobject.rotation.X, gameobject.rotation.Z)
                        * Matrix.CreateScale(this.radius)
                        * this.RigidBody.WorldTrasformation;
                    //effect.World = world* mesh.ParentBone.Transform;
                    effect.View = cameraView;
                    effect.Projection = cameraProjection;
                }
                mesh.Draw();
            }
        }
    }
}
