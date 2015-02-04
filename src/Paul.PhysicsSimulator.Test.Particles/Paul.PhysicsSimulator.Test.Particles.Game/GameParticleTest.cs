namespace Paul.PhysicsSimulator
{
    using System;
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
    

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameParticleTest : Microsoft.Xna.Framework.Game
    {
        #region PrivateFields
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont fontInfo1;
        SpriteFont fontInfo2;

        PhysicsGame physicsGame;

        
        MouseState prevMouseState;
        MouseState currMouseState;

        KeyboardState prevKeyState;
        KeyboardState currentKeyState;

        GameState prevGameState;
        GameState gameState=GameState.Start;

        enum GameState { Start, Play, Pause, Leave, End };
        #endregion
        CartesianAxis asse;

        CameraMoveable camera;

        public GameParticleTest()
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

            //asse = new CartesianAxis(this, 1000, Color.Red, Color.Black, Color.Green, physicsGame.Camera);
            //this.Components.Add(asse);

            //Basic
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            fontInfo1 = Content.Load<SpriteFont>("Fonts\\info1");
            fontInfo2 = Content.Load<SpriteFont>("Fonts\\info2");
            //splashManager.AddSplash(physicsGame.RenderManager.SpriteBatch, "introInfo", new SplashScreenDoubleText(fontInfo1, fontInfo2, new Vector2(this.Window.ClientBounds.Width, this.Window.ClientBounds.Height), Color.Gold, Color.Gold));

            //My content            

            //Terreno
            //Rettangle rettangoloTmp = new Rettangle(this, 20, 20, Color.Blue);
            //rettangoloTmp.PhysicsComponent.Position = new Vector3(-10, 0, -10);
            //physicsGame.AddModelObject(rettangoloTmp);

            //LoadContent_DragForce();
            
            //LoadContent_Spring();

            //LoadContent_Cable();

            //LoadContent_Rod();
            
            Mouse.SetPosition(this.Window.ClientBounds.Width / 2, this.Window.ClientBounds.Height / 2);
            prevMouseState = Mouse.GetState();

            ChangeGameState(GameState.Start, true);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //currMouseState = Mouse.GetState();
            //currentKeyState = Keyboard.GetState();

            //if (currentKeyState.IsKeyDown(Keys.Enter) && prevKeyState.IsKeyUp(Keys.Enter))
            //{
            //    if (gameState == GameState.Start)
            //        ChangeGameState(GameState.Play, false);
            //}
            //if (currentKeyState.IsKeyDown(Keys.P) && prevKeyState.IsKeyUp(Keys.P))
            //{
            //    if (gameState == GameState.Play)
            //        ChangeGameState(GameState.Pause, false);
            //    else if (gameState == GameState.Pause)
            //        ChangeGameState(GameState.Play, false);
            //}
            //if (currentKeyState.IsKeyDown(Keys.Escape) && prevKeyState.IsKeyUp(Keys.Escape))
            //{
            //    if (gameState == GameState.Play || gameState == GameState.Start)
            //    {
            //        ChangeGameState(GameState.Leave, false);
            //    }
            //    else
            //    {
            //        ChangeGameState(prevGameState, false);
            //    }
            //}
            //if (physicsGame.Enabled || gameState == GameState.Start)
            //{
            //    if (prevMouseState.X != currMouseState.X)
            //    {
            //        if (gameState == GameState.Play)
            //            physicsGame.ActiveCamera.RotateYaw(((float)prevMouseState.X - (float)currMouseState.X) * 0.005f);

            //        Mouse.SetPosition(this.Window.ClientBounds.Width / 2, currMouseState.Y);
            //        currMouseState = Mouse.GetState();
            //    }
            //    if (prevMouseState.Y != currMouseState.Y)
            //    {
            //        if (gameState == GameState.Play)
            //            physicsGame.ActiveCamera.RotatePitch(((float)prevMouseState.Y - (float)currMouseState.Y) * 0.005f);

            //        Mouse.SetPosition(this.Window.ClientBounds.Width / 2, this.Window.ClientBounds.Height / 2);
            //        currMouseState = Mouse.GetState();
            //        prevMouseState = currMouseState;
            //    }
            //    if (gameState == GameState.Play)
            //    {
            //        if (prevKeyState.IsKeyDown(Keys.Left))
            //            physicsGame.ActiveCamera.MoveLeftDirection(0.05f);
            //        if (prevKeyState.IsKeyDown(Keys.Right))
            //            physicsGame.ActiveCamera.MoveRightDirection(0.05f);
            //        if (prevKeyState.IsKeyDown(Keys.Down))
            //            physicsGame.ActiveCamera.MoveBackwardDirection(0.05f);
            //        if (prevKeyState.IsKeyDown(Keys.Up))
            //            physicsGame.ActiveCamera.MoveForwardDirection(0.05f);
            //    }

            //    if (prevKeyState.IsKeyUp(Keys.E) && currentKeyState.IsKeyDown(Keys.E))
            //    {
            //        //physicsGame.AddExplosion(new Vector3(1, 1, 1), GraphicsDevice);
            //    }
            //}
            //prevMouseState = currMouseState;
            //prevKeyState = currentKeyState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            // Clear dello screen
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.CornflowerBlue, 1.0f, 0);
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;


            base.Draw(gameTime);
        }

        private void ChangeGameState(GameState stato, bool force)
        {
            //if (stato != gameState || force)
            //{
            //    prevGameState = gameState;
            //    gameState = stato;
            //    switch (gameState)
            //    {
            //        case GameState.Start:
            //            ((SplashScreenDoubleText)splashManager.GetSplashByName("introInfo")).SetTexts("Benvenuto in PHYSICS ENGINE Creato da Paolo Francia", "Premi ENTER per iniziare");
            //            splashManager.RefreshCanDraw();
            //            splashManager.Enabled = true;
            //            physicsGame.Enabled = false;
            //            this.IsMouseVisible = false;
            //            break;
            //        case GameState.Play:
            //            splashManager.Enabled = false;
            //            physicsGame.Enabled = true;
            //            this.IsMouseVisible = false;
            //            break;
            //        case GameState.Pause:
            //            //splashManager.SplashScreenList[0].SetData("PASUSA", "Premi P per continuare");
            //            splashManager.Enabled = true;
            //            physicsGame.Enabled = false;
            //            this.IsMouseVisible = true;
            //            break;
            //        case GameState.Leave:
            //            splashManager.Enabled = false;
            //            physicsGame.Enabled = false;
            //            this.IsMouseVisible = true;
            //            break;
            //        case GameState.End:
            //            //splashManager.SplashScreenList[0].SetData("END", "");
            //            splashManager.Enabled = true;
            //            physicsGame.Enabled = false;
            //            this.IsMouseVisible = false;
            //            break;
            //    }
            //}
        }



        #region Examples_Scenario

        //private void LoadContent_DragForce()
        //{
        //    //Add sfera            
        //    Sphere sferaTmp = new Sphere(this, 0.5f, Color.Yellow);
        //    sferaTmp.PhysicsComponent.Position = new Vector3(-3, 2, 0);
        //    physicsGame.AddModelObject(sferaTmp);

        //    sferaTmp.PhysicsComponent.Velocity = new Vector3(10.0f, 0, 0);

        //    physicsGame.ParticleWorld.forceRegistry.Add((Particle)sferaTmp.PhysicsComponent, new ParticleDrag(0.5f, 0.5f));

        //}

        //private void LoadContent_Spring()
        //{
        //    //Add sfera            
        //    Sphere sferaTmp = new Sphere(this, 1.5f, Color.Red);
        //    sferaTmp.PhysicsComponent.Position = new Vector3(5, 2, 0);
        //    physicsGame.AddModelObject(sferaTmp);

        //    Particle fixedPoint=new Particle(new Vector3(0,2,0),1f);

        //    //Spring setup
        //    physicsGame.ParticleWorld.forceRegistry.Add((Particle)sferaTmp.PhysicsComponent,
        //        new ParticleSpring(fixedPoint, 10f,
        //            (float)Math.Abs((fixedPoint.Position - sferaTmp.PhysicsComponent.Position).Length()) * 1.5f
        //            ));
 
        //}

        //private void LoadContent_Cable()
        //{
        //    Sphere sferaTmp1 = new Sphere(this, 1f, Color.Yellow);
        //    sferaTmp1.PhysicsComponent.Position = new Vector3(-2, 6, 0);
        //    physicsGame.AddModelObject(sferaTmp1);

        //    Sphere sferaTmp2 = new Sphere(this, 1f, Color.Yellow);
        //    sferaTmp2.PhysicsComponent.Position = new Vector3(2, 6, 0);
        //    physicsGame.AddModelObject(sferaTmp2);

        //    //sferaTmp1.PhysicsComponent.Velocity = new Vector3(-5.0f, 0, 0);
        //    //sferaTmp2.PhysicsComponent.Velocity = new Vector3(5.0f, 0, 0);

        //    physicsGame.ParticleWorld.forceRegistry.Add((Particle)sferaTmp1.RigidBody, new GravityForce(GravityForce.gravityConstant));

        //    physicsGame.ParticleWorld.contactGenRegistration.Add(new ParticleCable((Particle)sferaTmp1.PhysicsComponent, (Particle)sferaTmp2.PhysicsComponent, 5f, 1f));
        //}

        //private void LoadContent_Rod()
        //{
        //    Sphere sferaTmp1 = new Sphere(this, 1f, Color.Yellow);
        //    sferaTmp1.PhysicsComponent.Position = new Vector3(-2, 6, 0);
        //    physicsGame.AddModelObject(sferaTmp1);

        //    Sphere sferaTmp2 = new Sphere(this, 1f, Color.Yellow);
        //    sferaTmp2.PhysicsComponent.Position = new Vector3(2, 6, 0);
        //    physicsGame.AddModelObject(sferaTmp2);

        //    //sferaTmp1.PhysicsComponent.Velocity = new Vector3(5.0f, 0, 0);
        //    physicsGame.ParticleWorld.forceRegistry.Add((Particle)sferaTmp1.PhysicsComponent, new GravityForce(GravityForce.gravityConstant));
            
        //    physicsGame.ParticleWorld.contactGenRegistration.Add(new ParticleRod((Particle)sferaTmp1.PhysicsComponent, (Particle)sferaTmp2.PhysicsComponent, 5f));
        //}
        #endregion
    }
}