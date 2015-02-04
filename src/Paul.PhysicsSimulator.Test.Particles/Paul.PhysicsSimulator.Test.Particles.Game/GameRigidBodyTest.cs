namespace Paul.PhysicsSimulator
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework.Storage;
    using Microsoft.Xna.Framework.GamerServices;    
    using Paul.PhysicsSimulator.Engine.Gaming;
    using Paul.PhysicsSimulator.Engine.Graphics.Utility;
    using Paul.PhysicsSimulator.Engine.Graphics.Camera;
    using Paul.PhysicsSimulator.Engine.Graphics.Shapes;
    using Paul.PhysicsSimulator.Engine.Physics.Force;
    using Paul.PhysicsSimulator.Engine.Physics.Particles;
    using Paul.PhysicsSimulator.Engine.Physics.Collision;
    using Paul.PhysicsSimulator.Engine.Physics.RigidBodies;
    using Paul.PhysicsSimulator.Engine.Graphics.SplashScreen;
    using Paul.PhysicsSimulator.Game.UpdateBehaviour;
    using Paul.PhysicsSimulator.Game.Scenes;
    using Paul.PhysicsSimulator.Game;

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameRigidBodyTest : Microsoft.Xna.Framework.Game
    {
        #region PrivateFields
        GraphicsDeviceManager graphics;
        PhysicsGame physicsGame;

        IList<IUpdateBehaviour> updateBehaviours;

        IList<IGameScene> gameScenes;
        SceneGameSelection sceneGameSelection;

        IGameScene activeScene;

        string[] sceneNames = new string[] { typeof(SceneRigidBodyGravity).Name, typeof(SceneRigidBodyGravity).Name };
        Type[] sceneTypes = new Type[] { typeof(SceneRigidBodyGravity), typeof(SceneRigidBodySpring) };

        MouseState mouseState;
        GameState gameState = GameState.SceneSelection;

        ShowCase showCase = ShowCase.CubeTorque;

        enum GameState { SceneSelection, Play, Pause, End };
        enum ShowCase { CubeTorque };
        
        float boxRadius;
        #endregion

        public GameRigidBodyTest()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            physicsGame = new PhysicsGame(this);
            physicsGame.UpdateEnabled = true;


            boxRadius = 100f;

            //Basic
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //Updates behaviour
            updateBehaviours = new List<IUpdateBehaviour>();

            // Manage the pause and exit of the game
            UpdateGameStatusBehaviour updateGameStatus = new UpdateGameStatusBehaviour();
            updateGameStatus.OnExit += new EventHandler(updateGameStatus_OnExit);
            updateGameStatus.OnPause += new EventHandler(updateGameStatus_OnPause);
            updateBehaviours.Add(updateGameStatus);


            //Scene
            sceneGameSelection = new SceneGameSelection(this.physicsGame, this, sceneTypes);
            sceneGameSelection.OnGameSelected += new EventHandler(sceneGameSelection_OnGameSelected);
            SetScene(sceneGameSelection);

            //// Moveable Camera
            //CameraMoveable camera = new CameraMoveable(new Vector3(0, 8, 12), new Vector3(0, 0, 0), Vector3.Up, 0.01f, 4000.0f, (float)this.Window.ClientBounds.Width / (float)this.Window.ClientBounds.Height);
            //FPSCameraMover fpsCameraMover = new FPSCameraMover(this, camera);
            //physicsGame.AddComponent("fpsCameraMover", fpsCameraMover);
            //physicsGame.ActiveCamera = camera;

            //// Cartesian Axis
            //CartesianAxis asse = new CartesianAxis(this.GraphicsDevice, 1000, Color.Red, Color.Black, Color.Green, physicsGame.ActiveCamera);
            //physicsGame.AddComponent("axis",asse);

            //SpriteFont fontInfo1 = Content.Load<SpriteFont>("Fonts\\info1");
            //SpriteFont fontInfo2 = Content.Load<SpriteFont>("Fonts\\info2");
            //physicsGame.AddComponent("introInfo", new Text2dDoubleRows(physicsGame.RenderManager.SpriteBatch ,fontInfo1, fontInfo2, new Vector2(this.Window.ClientBounds.Width / 2, this.Window.ClientBounds.Height / 2), Color.Gold, Color.Gold));

            //My content            

            ////Terreno
            //Rettangle rettangoloTmp = new Rettangle(this, 20, 20, Color.Blue);
            //rettangoloTmp.PhysicsComponent.Position = new Vector3(-10, 0, -10);
            //modelMng.AddModelObject(rettangoloTmp);

            //switch (showCase)
            //{
            //    case ShowCase.CubeTorque:
            //        LoadContent_CubeRotation();
            //        break;
            //}

            //SimpleSelectMenu menu=new SimpleSelectMenu(physicsGame.RenderManager.SpriteBatch ,fontInfo1,Color.Red,new Vector2(50,50),new string[]{"primaVoce","secondaVoce","terzaVoce"});
            //physicsGame.AddComponent("menu", menu);
            //menu.OnSelectedIndexChanged += new EventHandler(menu_OnSelectedIndexChanged);


            //Cube cube = new Cube(this, new Vector3(1, 1, 1), Color.Red);
            //cube.RigidBody.Position = new Vector3(0, 10, 0);
            

            //physicsGame.AddComponent("cube",cube);
            //physicsGame.RigidBodyWorld.ForceRegistry.Add(cube.RigidBody, new GravityForce(GravityForce.gravityConstant));
            
            //Mouse.SetPosition(this.Window.ClientBounds.Width / 2, this.Window.ClientBounds.Height / 2);
            //mouseState = Mouse.GetState();

            //ChangeGameState(GameState.Start, true);
        }

        void SetScene(IGameScene scene)
        {
            //Remove all components from the engine
            this.physicsGame.RemoveAllComponents();

            scene.InitializeScene();
            activeScene = scene;
            physicsGame.ActiveCamera = activeScene.DefaultCamera;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            foreach(IUpdateBehaviour updateBehaviour in updateBehaviours)
                updateBehaviour.Update(gameTime);

            //activeScene.Update(gameTime);
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            //// TODO: Add your update logic here
            //KeyboardState keyState = Keyboard.GetState();

            //if (keyState.IsKeyDown(Keys.Enter) && savedKeyState.IsKeyUp(Keys.Enter))
            //{
            //    //if (gameState == GameState.Start)
            //        //ChangeGameState(GameState.Play, false);
            //}
            //if (keyState.IsKeyDown(Keys.P) && savedKeyState.IsKeyUp(Keys.P))
            //{
            //    if (gameState == GameState.Play)
            //        ChangeGameState(GameState.Pause, false);
            //    else if (gameState == GameState.Pause)
            //        ChangeGameState(GameState.Play, false);
            //}
            //if (keyState.IsKeyDown(Keys.Escape) && savedKeyState.IsKeyUp(Keys.Escape))
            //{
            //    //if (gameState == GameState.Play || gameState == GameState.Start)
            //    //{
            //    //    //ChangeGameState(GameState.Leave, false);
            //    //}
            //}
            
            //savedKeyState = keyState;

            //DeleteObjectsOutOfBox();

            base.Update(gameTime);
        }

        /// <summary>
        /// Elimina gli oggetti che escono dal box degli oggetti
        /// </summary>
        private void DeleteObjectsOutOfBox()
        {
            IList<string> modelsToDelete = physicsGame.Components.Where(w => w.Value is IRigidBodyComponent && ((IRigidBodyComponent)w.Value).RigidBody.Position.Length() > boxRadius).Select(w=>w.Key).ToList();

            for (int i = 0; i < modelsToDelete.Count; i++)
                physicsGame.RemoveComponent(modelsToDelete[i]);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            // Clear dello screen
            //GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.CornflowerBlue, 1.0f, 0);
            //GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            activeScene.Draw(gameTime);

            base.Draw(gameTime);
        }

        private void ChangeGameState(GameState stato, bool force)
        {
            if (stato != gameState || force)
            {
                gameState = stato;
                switch (gameState)
                {
                    //case GameState.Start:
                    //    ((Text2dDoubleRows)physicsGame.Components["introInfo"]).SetTexts("Welcome in PHYSICS ENGINE By Paolo Francia, example: "+showCase.ToString(), "Premi ENTER per iniziare");
                    //    physicsGame.PhysicsEnabled = false;
                    //    this.IsMouseVisible = false;
                    //    break;
                    case GameState.Play:
                        ((Text2dDoubleRows)physicsGame.Components["introInfo"]).SetTexts("", "");
                        physicsGame.UpdateEnabled = true;
                        this.IsMouseVisible = false;
                        Mouse.SetPosition(mouseState.X, mouseState.Y);
                        break;
                    case GameState.Pause:
                        ((Text2dDoubleRows)physicsGame.Components["introInfo"]).SetTexts("PAUSA", "Premi P per continuare");
                        physicsGame.UpdateEnabled = false;
                        this.IsMouseVisible = true;
                        mouseState = Mouse.GetState();
                        break;
                    //case GameState.Leave:
                    //    physicsGame.PhysicsEnabled = false;
                    //    this.IsMouseVisible = true;
                    //    break;
                    case GameState.End:
                        physicsGame.UpdateEnabled = false;
                        this.IsMouseVisible = false;
                        break;
                }
            }
        }


        void updateGameStatus_OnPause(object sender, EventArgs e)
        {
            this.physicsGame.UpdateEnabled = !this.physicsGame.UpdateEnabled;
        }

        void updateGameStatus_OnExit(object sender, EventArgs e)
        {
            if (activeScene == sceneGameSelection)
                Exit();
            else
                SetScene(sceneGameSelection);
        }

        void sceneGameSelection_OnGameSelected(object sender, EventArgs e)
        {
            IGameScene scene = (IGameScene)Activator.CreateInstance(sceneTypes[sceneGameSelection.SelectedGameIndex], this.physicsGame, this);
            SetScene(scene);
        }

        #region Examples_Scenario
        
        //private void LoadContent_CubeRotation()
        //{
        //    //Add Cube
        //    // Il cubo ha il lato di 1
        //    Cube cubeTmp = new Cube(this, new Vector3(1,1,1), Color.Yellow);
        //    cubeTmp.PhysicsComponent.Position = new Vector3(-3, 2, 0);
        //    modelMng.AddModelObject(cubeTmp);

        //    ((RigidBody)cubeTmp.PhysicsComponent).AddForceAtBodyPoint(new Vector3(0,0,-5),new Vector3(0.5f,0,0));

        //    ((SplashScreenSingleText)splashManager.GetSplashByName("howTo")).SetTexts("Premi X per sparare un cubo verso l'alto");
        //}
        //private void Update_CubeRotation()
        //{
        //    if (prevKeyState.IsKeyUp(Keys.X) && currentKeyState.IsKeyDown(Keys.X))
        //    {
        //        Cube cube = (Cube)modelMng.GetModels().Where(w => w is Cube && w.PhysicsComponent.Velocity.Length() == 0).FirstOrDefault();
                
        //        if (modelMng.GetModels().Count() == 0 || cube==null)
        //        {
        //            //Add Cube
        //            cube = new Cube(this, new Vector3(1, 1, 1), Color.Yellow);
        //            cube.PhysicsComponent.Position = new Vector3(-3, 2, 0);
        //            modelMng.AddModelObject(cube);
        //        }
        //        else
        //        {
        //            //Aggiunge una velocità iniziale
        //            cube.PhysicsComponent.Velocity = new Vector3(3, 20, 0);
        //            ((RigidBody)(cube.PhysicsComponent)).AddTorque(new Vector3(0, 15, 0));
        //            modelMng.RigidBodyWorld.ForceRegistry.Add(cube.PhysicsComponent, new GravityForce(GravityForce.gravityConstant));                                        
        //        }
        //    }
        //}

        //private void LoadContent_Spring()
        //{
        //    //Add sfera            
        //    Sphere sferaTmp = new Sphere(this, 1.5f, Color.Red);
        //    sferaTmp.PhysicsComponent.Position = new Vector3(5, 2, 0);
        //    modelMng.AddModelObject(sferaTmp);

        //    Particle fixedPoint=new Particle(new Vector3(0,2,0),1f);

        //    //Spring setup
        //    modelMng.ParticleWorld.forceRegistry.Add((Particle)sferaTmp.PhysicsComponent,
        //        new ParticleSpring(fixedPoint, 10f,
        //            (float)Math.Abs((fixedPoint.Position - sferaTmp.PhysicsComponent.Position).Length()) * 1.5f
        //            ));
 
        //}

        //private void LoadContent_Cable()
        //{
        //    Sphere sferaTmp1 = new Sphere(this, 1f, Color.Yellow);
        //    sferaTmp1.PhysicsComponent.Position = new Vector3(-2, 6, 0);
        //    modelMng.AddModelObject(sferaTmp1);

        //    Sphere sferaTmp2 = new Sphere(this, 1f, Color.Yellow);
        //    sferaTmp2.PhysicsComponent.Position = new Vector3(2, 6, 0);
        //    modelMng.AddModelObject(sferaTmp2);

        //    //sferaTmp1.PhysicsParticle.Velocity = new Vector3(-5.0f, 0, 0);
        //    //sferaTmp2.PhysicsParticle.Velocity = new Vector3(5.0f, 0, 0);

        //    modelMng.ParticleWorld.forceRegistry.Add((Particle)sferaTmp1.PhysicsComponent, new GravityForce(GravityForce.gravityConstant));

        //    modelMng.ParticleWorld.contactGenRegistration.Add(new ParticleCable((Particle)sferaTmp1.PhysicsComponent, (Particle)sferaTmp2.PhysicsComponent, 5f, 1f));
        //}

        //private void LoadContent_Rod()
        //{
        //    Sphere sferaTmp1 = new Sphere(this, 1f, Color.Yellow);
        //    sferaTmp1.PhysicsComponent.Position = new Vector3(-2, 6, 0);
        //    modelMng.AddModelObject(sferaTmp1);

        //    Sphere sferaTmp2 = new Sphere(this, 1f, Color.Yellow);
        //    sferaTmp2.PhysicsComponent.Position = new Vector3(2, 6, 0);
        //    modelMng.AddModelObject(sferaTmp2);

        //    //sferaTmp1.PhysicsParticle.Velocity = new Vector3(5.0f, 0, 0);
        //    modelMng.ParticleWorld.forceRegistry.Add((Particle)sferaTmp1.PhysicsComponent, new GravityForce(GravityForce.gravityConstant));

        //    modelMng.ParticleWorld.contactGenRegistration.Add(new ParticleRod((Particle)sferaTmp1.PhysicsComponent, (Particle)sferaTmp2.PhysicsComponent, 5f));
        //}
        #endregion
    }
}