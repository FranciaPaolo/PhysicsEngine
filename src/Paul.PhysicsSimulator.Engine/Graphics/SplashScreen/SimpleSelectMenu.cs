namespace Paul.PhysicsSimulator.Engine.Graphics.SplashScreen
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Menu that display a selectable list.
    /// </summary>
    public class SimpleSelectMenu : ISelectableMenu
    {
        /// <summary>
        /// Get or Set the items of the menu
        /// </summary>
        private List<string> menuItems;

        private IList<Text2d> textLists;
        private IList<Text2d> checktLists;
        private SpriteBatch spriteBatch;
        private SpriteFont font;
        private Color fontColor;
        private Vector2 upperLeftCorner;
        private float marginY = 3; //TODO: create the setter and getter methods
        private string textUnChecked = "[ ]";
        private string textChecked = "[X]";
        private int selectedIndex;
        private KeyboardState prevKeyState;
        private KeyboardState currentKeyState;

        /// <summary>
        /// Notify when the seleted index is changed
        /// </summary>
        public event EventHandler OnSelectedIndexChanged;

        /// <summary>
        /// Get or Set the index of the current selected item.
        /// </summary>
        public int SelectedIndex { get { return this.selectedIndex; } set { this.SetSelectedIndex(value); } }

        /// <summary>
        /// Get the list of the menu items
        /// </summary>
        public IEnumerable<string> MenuItems { get { return menuItems; } }

        /// <summary>
        /// Create the Selectable Menu
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch used to draw the menu</param>
        /// <param name="font">Font used for the text</param>
        /// <param name="fontColor">Color used for the text</param>
        /// <param name="upperLeftCorner">Upper Left Corner where the menu start to draw</param>
        /// <param name="menuItems">List of items of the menu</param>
        public SimpleSelectMenu(SpriteBatch spriteBatch, SpriteFont font, Color fontColor, Vector2 upperLeftCorner, string[] menuItems)
        {
            this.spriteBatch = spriteBatch;
            this.font = font;
            this.upperLeftCorner = upperLeftCorner;
            this.fontColor = fontColor;
            this.IsEnabled = true;
            this.selectedIndex = 0;

            CreateMenuFromItemList(menuItems);
            SetSelectedIndex(0);
        }

        /// <summary>
        /// Get or Set if the object have to be drawn or it's disabled
        /// </summary>
        public bool IsEnabled
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
        public void Draw(GraphicsDevice graphicsDevice, Matrix cameraView, Matrix cameraProjection)
        { 
            foreach(Text2d text2d in this.textLists)
                text2d.Draw(graphicsDevice,cameraView,cameraProjection);
            foreach (Text2d check in this.checktLists)
                check.Draw(graphicsDevice, cameraView, cameraProjection);
        }

        /// <summary>
        /// Update the status of the component
        /// </summary>
        /// <param name="secondElapsed">Time elapsed since last update (0 for the first time)</param>
        public void Update(float secondElapsed)
        {
            currentKeyState = Keyboard.GetState();

            if (currentKeyState.IsKeyDown(Keys.Down) && prevKeyState.IsKeyUp(Keys.Down))
                this.SetSelectedIndex((this.selectedIndex + 1) % this.menuItems.Count);

            if (currentKeyState.IsKeyDown(Keys.Up) && prevKeyState.IsKeyUp(Keys.Up))
                this.SetSelectedIndex((this.selectedIndex + this.menuItems.Count - 1) % this.menuItems.Count);

            if (currentKeyState.IsKeyDown(Keys.Enter) && prevKeyState.IsKeyUp(Keys.Enter))
            {
                if (this.OnSelectedIndexChanged != null)
                    OnSelectedIndexChanged(this, new EventArgs());
            }
            
            prevKeyState = currentKeyState;
        }

        //TODO: add the methods to add, remove, clear the menu items

        private void CreateMenuFromItemList(string []items)
        {
            this.textLists = new List<Text2d>();
            this.checktLists = new List<Text2d>();
            this.menuItems = new List<string>();

            if (items == null)
                return;

            Vector2 itemPosition=new Vector2(upperLeftCorner.X,upperLeftCorner.Y);
            float checkWidth = font.MeasureString(textChecked).X;
            Vector2 currentItemPosition = new Vector2(itemPosition.X, itemPosition.Y);
            for (int i=0; i < items.Length; i++)
            {
                this.menuItems.Add(items[i]);
                this.textLists.Add(new Text2d(this.spriteBatch, font, new Vector2(currentItemPosition.X+checkWidth, currentItemPosition.Y), fontColor, Text2d.TextAlignment.Left, items[i]));
                this.checktLists.Add(new Text2d(this.spriteBatch, font, new Vector2(currentItemPosition.X, currentItemPosition.Y), fontColor, Text2d.TextAlignment.Left, textUnChecked));

                Vector2 textSize = font.MeasureString(items[i]);
                currentItemPosition = new Vector2(currentItemPosition.X, currentItemPosition.Y + textSize.Y + marginY);
            }
        }

        private void SetSelectedIndex(int index)
        {
            if (index >= 0 && index < this.textLists.Count)
            {
                checktLists[selectedIndex].SetTexts(textUnChecked);
                checktLists[index].SetTexts(textChecked);
                this.selectedIndex = index;
            }
        }
    }
}
