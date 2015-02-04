namespace Paul.PhysicsSimulator.Game.Scenes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Paul.PhysicsSimulator.Engine.Gaming;
    using Paul.PhysicsSimulator.Engine.Graphics.SplashScreen;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework;
    using Paul.PhysicsSimulator.Game.Scenes;
    using Paul.PhysicsSimulator.Engine.Graphics.Camera;
    using System.Reflection;
    using Paul.PhysicsSimulator.Engine.Graphics.Shapes;

    /// <summary>
    /// Select the game to play
    /// </summary>
    public class SceneGameSelection : IGameScene
    {
        private PhysicsGame physicsGame;
        private CameraMoveable camera;
        private Text2d titleText;
        private Game xnaGame;
        private int selectedGameIndex = 0;

        Cube cube;
        Cube cube2;
        public SimpleSelectMenu Menu;

        public ICamera DefaultCamera { get { return camera; } }
        public int SelectedGameIndex { get { return selectedGameIndex; } }
        private string cubeModel = "Models\\CuboTextures";

        public event EventHandler OnGameSelected;

        public SceneGameSelection(PhysicsGame physicsGame,Game xnaGame, Type[] sceneClassTypes)
        {
            this.physicsGame = physicsGame;
            this.xnaGame = xnaGame;
            camera=new CameraMoveable(new Vector3(0, 4, 3), new Vector3(0, 0, 0), Vector3.Up, 0.01f, 4000.0f, (float)xnaGame.Window.ClientBounds.Width / (float)xnaGame.Window.ClientBounds.Height);

            //Build the menu
            SpriteFont menuFont = xnaGame.Content.Load<SpriteFont>("Fonts\\info1");
            SpriteFont titleFont = xnaGame.Content.Load<SpriteFont>("Fonts\\info2");

            //Get from the types of the scene the description
            string[] sceneDescriptions = new string[sceneClassTypes.Length];
            for (int i = 0; i < sceneClassTypes.Length; i++)
            {                
                FieldInfo fieldInfo = sceneClassTypes[i].GetField("SceneDescription", BindingFlags.Public | BindingFlags.Static); 
                sceneDescriptions[i] = (string)fieldInfo.GetValue(null);
            }

            this.Menu = new SimpleSelectMenu(physicsGame.RenderManager.SpriteBatch, menuFont, Color.Red, new Vector2(50, 100), sceneDescriptions);
            this.Menu.OnSelectedIndexChanged += new EventHandler(Menu_OnSelectedIndexChanged);

            //Add components
            cube = new Cube(xnaGame, new Vector3(1, 1, 1), cubeModel, null, false);
            cube.RigidBody.Position = new Vector3(1, 2, 0);
            cube.RigidBody.AddTorque(new Vector3(50,50,0));
            

            cube2 = new Cube(xnaGame, new Vector3(1, 1, 1), cubeModel, null, false);
            cube2.RigidBody.Position = new Vector3(2, 0, 0);
            cube2.RigidBody.AddTorque(new Vector3(-30, 0, -30));

            //Title
            titleText = new Text2d(physicsGame.RenderManager.SpriteBatch, titleFont, new Vector2(10, 10), Color.Red, Text2d.TextAlignment.Left, "wellcome! \nthis a test application\nthat use my simple Physics Engine\nplease select an example scene:");
        }

        /// <summary>
        /// Create the scene objects
        /// </summary>
       public  void InitializeScene()
        {
            this.physicsGame.AddComponent("menu", this.Menu);
            this.physicsGame.AddComponent("cube", cube);
            this.physicsGame.AddComponent("cube2", cube2);
            this.physicsGame.AddComponent("titleText", titleText);
        }

        public string GetSceneDescription()
        {
            return "";
        }

        /// <summary>
        /// Update of the gamePlay
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// Draw custom objects
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime)
        {
            xnaGame.GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);
            xnaGame.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

        }

        void Menu_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            selectedGameIndex = Menu.SelectedIndex;
            if (OnGameSelected != null)
                OnGameSelected(this, e);
        }
    }
}
