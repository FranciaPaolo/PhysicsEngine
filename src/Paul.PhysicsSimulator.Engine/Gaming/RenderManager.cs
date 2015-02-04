namespace Paul.PhysicsSimulator.Engine.Gaming
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.GamerServices;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Paul.PhysicsSimulator.Engine.Physics.Particles;
    using Paul.PhysicsSimulator.Engine.Physics.Collision;
    using Paul.PhysicsSimulator.Engine.Physics.Explosion;
    using Paul.PhysicsSimulator.Engine.Graphics.Camera;
    using Paul.PhysicsSimulator.Engine.Graphics.Shapes;
    using Paul.PhysicsSimulator.Engine.Physics.Force;
    using Paul.PhysicsSimulator.Engine.Physics.RigidBodies;        
    
    /// <summary>
    /// Draw the objects on the screen
    /// </summary>
    public class RenderManager
    {
        private Game xnaGame;
        private SpriteBatch spriteBatch;

        /// <summary>
        /// All registered drawable objects
        /// </summary>
        private List<IDrawable> models;        

        /// <summary>
        /// Get all registered drawable object
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IDrawable> DrawableModels
        {
            get
            {
                return models.AsEnumerable();
            }
        }

        /// <summary>
        /// Get or set the camera used to calculate the projection and view matrix to draw the objects
        /// </summary>
        public ICamera Camera
        {
            get;
            set;
        }

        /// <summary>
        /// Get the SpriteBacth used to draw the objects. It's used for istance by the Text Objects to write 2d text on the screen.
        /// </summary>
        public SpriteBatch SpriteBatch { get { return spriteBatch; } }

        /// <summary>
        /// Istantiate the Render Manage. It need the XnaGame object to get the spritebacth
        /// </summary>
        /// <param name="xnaGame">Xna game object</param>
        public RenderManager(Game xnaGame)
        {
            models = new List<IDrawable>();
            this.xnaGame=xnaGame;
            this.spriteBatch = new SpriteBatch(xnaGame.GraphicsDevice);
        }

        /// <summary>
        /// Add a drawable model
        /// </summary>
        /// <param name="model"></param>
        public void AddRender(IDrawable model)
        {
            models.Add(model);
            //if(model.PhysicsComponent is Particle)
            //    ParticleWorld.Particles.Add((Particle)model.PhysicsComponent);
            //else if (model.PhysicsComponent is RigidBody)
            //    RigidBodyWorld.AddRigidBody((RigidBody)model.PhysicsComponent);            
        }

        /// <summary>
        /// Remove the object from the components
        /// </summary>
        /// <param name="model"></param>
        public void RemoveRender(IDrawable model)
        {
            models.Remove(model);
        }
        
        //public void AddExplosion(Vector3 position,GraphicsDevice graphicsDevice)
        //{
        //    explosionManager.AddExplosion(position, graphicsDevice);
        //}

        /// <summary>
        /// Draw all registered objects
        /// </summary>
        public void Draw()
        {
            spriteBatch.Begin();
            for (int i = 0; i < models.Count; i++)
            {
                if(models[i].IsEnabled)
                    models[i].Draw(this.xnaGame.GraphicsDevice, Camera.CameraView, Camera.CameraProjection);
            }
            spriteBatch.End();
            //explosionManager.Draw(camera);
        }
    }
}
