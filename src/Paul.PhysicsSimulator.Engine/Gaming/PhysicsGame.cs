namespace Paul.PhysicsSimulator.Engine.Gaming
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Paul.PhysicsSimulator.Engine.Graphics.Camera;
    using Paul.PhysicsSimulator.Engine.Graphics.Shapes;
    using Paul.PhysicsSimulator.Engine.Physics.Commons;
    using Paul.PhysicsSimulator.Engine.Physics.Particles;
    using Paul.PhysicsSimulator.Engine.Physics.RigidBodies;
    using Paul.PhysicsSimulator.Engine.Utilities;

    /// <summary>
    /// Entry point that manage the physics engines (particleWorld, rigidBodyWorld) and model's rendering.
    /// This class extend Microsoft.Xna.DrawableGameComponent
    /// </summary>
    public class PhysicsGame: DrawableGameComponent
    {
        private float elapsedGameTime = 0f;
        private bool updateEnabled;
        private Dictionary<string, IGameComponent> components;
        private ReadOnlyDictionary<string, IGameComponent> readOnlyComponents;
        private IList<IUpdateable> componentsUpdatable;
     
        /// <summary>
        /// RenderManager of the game
        /// </summary>
        public RenderManager RenderManager { get; protected set; }
        /// <summary>
        /// PhysicsEngine that manage all particles of the game
        /// </summary>
        public ParticleWorld ParticleWorld { get; protected set; }
        /// <summary>
        /// PhysicsEngine that manage all rigidBody of the game
        /// </summary>
        public RigidBodyWorld RigidBodyWorld { get; protected set; }
        /// <summary>
        /// Return all game components
        /// </summary>
        public ReadOnlyDictionary<string, IGameComponent> Components { get { return readOnlyComponents; } }
        /// <summary>
        /// Disable the updates of the physics engine calculations
        /// </summary>
        public bool UpdateEnabled { get { return updateEnabled; } set {updateEnabled = value;} }

        /// <summary>
        /// Get od Set the Current Active Camera
        /// </summary>
        public ICamera ActiveCamera
        {
            get { return activeCamera; } 
            set 
            {
                activeCamera = value;
                RenderManager.Camera=activeCamera;
            }
        }
        private ICamera activeCamera;

        /// <summary>
        /// Create the PhysicsGame object. It doesn't need to xnaGame
        /// </summary>
        /// <param name="xnaGame"></param>
        public PhysicsGame(Game xnaGame)
            : base(xnaGame)
        {
            this.updateEnabled = false;
            this.components = new Dictionary<string,IGameComponent>();
            this.readOnlyComponents = new ReadOnlyDictionary<string, IGameComponent>(components);
            this.componentsUpdatable=new List<IUpdateable>();
            
            //this.ParticleWorld = new ParticleWorld(50, 50);
            this.RigidBodyWorld = new RigidBodyWorld(50, 50);
            this.AddComponent("rigidBody", this.RigidBodyWorld);//RigidBodyWorld is an IUpdatable Component
            this.RenderManager = new RenderManager(xnaGame);

            xnaGame.Components.Add(this);
        }

        /// <summary>
        /// Add the component to the game. The component can be a physics object or a non physics object (like simple texture, axis, ...)
        /// </summary>
        /// <param name="componentName">Unique name of the component</param>
        /// <param name="gameComponent">Component object</param>
        public void AddComponent(string componentName,IGameComponent gameComponent)
        {
            this.components.Add(componentName,gameComponent);

            if(gameComponent is IDrawable)
                this.RenderManager.AddRender((IDrawable)gameComponent);

            if (gameComponent is IUpdateable)
                this.componentsUpdatable.Add((IUpdateable)gameComponent);

            if (gameComponent is IRigidBodyComponent)
                this.RigidBodyWorld.AddRigidBody(((IRigidBodyComponent)gameComponent).RigidBody);

        }

        /// <summary>
        /// Remove the requested game component
        /// </summary>
        /// <param name="componentName">Game component to be removed</param>
        public void RemoveComponent(string componentName)
        {
            IGameComponent gameComponent = components[componentName];

            if (gameComponent is IDrawable)
                this.RenderManager.RemoveRender((IDrawable)gameComponent);

            if (gameComponent is IUpdateable)
                this.componentsUpdatable.Remove((IUpdateable)gameComponent);

            if (gameComponent is IRigidBodyComponent)
                this.RigidBodyWorld.RemoveRigidBody(((IRigidBodyComponent)gameComponent).RigidBody);

            components.Remove(componentName);
        }

        /// <summary>
        /// Remove all game components
        /// </summary>
        public void RemoveAllComponents()
        {
            int i;
            string[] componentNames= this.components.Keys.ToList().ToArray();
            for (i = 0; i < componentNames.Length; i++)
            {
                if(components[componentNames[i]]!=this.RigidBodyWorld)
                    RemoveComponent(componentNames[i]);
            }
        }

        /// <summary>
        /// Xna DrawableGameComponent methods used to run all the IUpdatable game component (includes the physics engines)
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (updateEnabled)
            {
                elapsedGameTime = gameTime.ElapsedGameTime.Milliseconds / 1000f;
                for(int i=0;i<this.componentsUpdatable.Count;i++)// (IUpdateable component in this.componentsUpdatable)
                    this.componentsUpdatable[i].Update(elapsedGameTime);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Xna DrawableGameComponent methods used to run the RendererManager
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            RenderManager.Draw();
            base.Draw(gameTime);
        }
    }
}