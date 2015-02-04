namespace Paul.PhysicsSimulator.Engine.Graphics.SplashScreen
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Draw 2 texts in 2dimension. Each row can have a specific font.
    /// It's possible to use \n character to wrap text in multiple lines.
    /// </summary>
    public class Text2dDoubleRows: Paul.PhysicsSimulator.Engine.Gaming.IGameComponent,Paul.PhysicsSimulator.Engine.Gaming.IDrawable
    {
        #region PrivateFields
        private string textToDraw;
        private string secondaryTextToDraw;        
        private SpriteFont spriteFont;
        private SpriteFont secondarySpriteFont;
        private Color mainColor;
        private Color secondaryColor;
        private Vector2 textPosition;
        private Vector2 drawPosition;
        private Vector2 secondaryDrawPosition;
        private SpriteBatch spriteBatch;
        #endregion

        /// <summary>
        /// First row to be rendered
        /// </summary>
        public string TextToDraw
        {
            get
            { return textToDraw; }
        }
        /// <summary>
        /// Second row to be rendered
        /// </summary>
        public string SecondaryTextToDraw
        {
            get
            { return secondaryTextToDraw; }
        }        
        /// <summary>
        /// Font of the first row
        /// </summary>
        public SpriteFont MainSpriteFont
        {
            get { return spriteFont; }
            set { spriteFont = value; }
        }
        /// <summary>
        /// Font of the second row
        /// </summary>
        public SpriteFont SecondarySpriteFont
        {
            get { return secondarySpriteFont; }
            set { secondarySpriteFont = value; }
        }

        /// <summary>
        /// Create the splash screen text
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="MainFont">First row font</param>
        /// <param name="SecondaryFont">Second row font</param>
        /// <param name="textPosition">Position of the text. Relative to the center of the text</param>
        /// <param name="mainColor">Color of the first row</param>
        /// <param name="secondaryColor">Color of the second row</param>
        public Text2dDoubleRows(SpriteBatch spriteBatch,SpriteFont MainFont, SpriteFont SecondaryFont, Vector2 textPosition, Color mainColor, Color secondaryColor)
        {
            textToDraw = "";
            secondaryTextToDraw = "";

            spriteFont = MainFont;
            secondarySpriteFont = SecondaryFont;
            this.textPosition = textPosition;
            this.drawPosition = Vector2.Zero;
            this.secondaryDrawPosition = Vector2.Zero;
            this.mainColor = mainColor;
            this.secondaryColor = secondaryColor;
            this.spriteBatch = spriteBatch;
            this.IsEnabled = true;
        }      
        
        /// <summary>
        /// Draw the object
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <param name="cameraView"></param>
        /// <param name="cameraProjection"></param>
        public void Draw(GraphicsDevice graphicsDevice, Matrix cameraView, Matrix cameraProjection)
        {
            if (!String.IsNullOrEmpty(textToDraw))
                spriteBatch.DrawString(spriteFont, textToDraw, drawPosition, mainColor);
            if (!String.IsNullOrEmpty(secondaryTextToDraw))
                spriteBatch.DrawString(secondarySpriteFont, secondaryTextToDraw, secondaryDrawPosition, secondaryColor);
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
        /// Set the text to be drawn
        /// </summary>
        /// <param name="text">First row of text</param>
        /// <param name="secondaryText">Second row of text</param>
        public void SetTexts(string text,string secondaryText)
        {
            Vector2 textSize = spriteFont.MeasureString(text);
            Vector2 secondaryTextSize = spriteFont.MeasureString(secondaryText);

            drawPosition = new Vector2(textPosition.X - textSize.X / 2, textPosition.Y - textSize.Y / 2);
            secondaryDrawPosition = new Vector2(textPosition.X - secondaryTextSize.X / 2, textPosition.Y + textSize.Y / 2);

            textToDraw = text;
            secondaryTextToDraw = secondaryText;
        }
    }
}
