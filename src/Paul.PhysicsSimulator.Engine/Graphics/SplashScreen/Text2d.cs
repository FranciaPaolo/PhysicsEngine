namespace Paul.PhysicsSimulator.Engine.Graphics.SplashScreen
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Draw a simple 2d text. It's possible to use \n to wrap text on multiple lines
    /// </summary>
    public class Text2d :   Paul.PhysicsSimulator.Engine.Gaming.IGameComponent, 
                            Paul.PhysicsSimulator.Engine.Gaming.IDrawable
    {
        public enum TextAlignment { Left, Right, Center };

        #region PrivateFields
        private string textToDraw;
        private SpriteFont spriteFont;
        private Color mainColor;
        private Vector2 textPosition;
        private Vector2 drawPosition;
        private SpriteBatch spriteBatch;
        private TextAlignment textAlignment;
        #endregion

        /// <summary>
        /// Get the text to be drawn
        /// </summary>
        public string TextToDraw
        {
            get
            { return textToDraw; }
        }
        /// <summary>
        /// Get or Set the used font
        /// </summary>
        public SpriteFont Font
        {
            get { return spriteFont; }
            set { spriteFont = value; }
        }

        /// <summary>
        /// Create the text object
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="font">Font of the text</param>
        /// <param name="textPosition">Position of the text. Relative to the center of the text</param>
        /// <param name="mainColor">Color of the text</param>        
        /// <param name="textToDraw">Text to draw</param>        
        public Text2d(SpriteBatch spriteBatch, SpriteFont font, Vector2 textPosition, Color mainColor, TextAlignment textAlignment, string textToDraw)
        {
            this.textToDraw = textToDraw;
            this.spriteFont = font;
            this.textPosition = textPosition;
            this.drawPosition = Vector2.Zero;
            this.mainColor = mainColor;
            this.spriteBatch = spriteBatch;
            this.textAlignment = textAlignment;

            this.SetTexts(textToDraw);
            this.IsEnabled = true;
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
        /// Draw the object
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <param name="cameraView"></param>
        /// <param name="cameraProjection"></param>
        public void Draw(GraphicsDevice graphicsDevice, Matrix cameraView, Matrix cameraProjection)
        {
            if(!String.IsNullOrEmpty(textToDraw))
                spriteBatch.DrawString(spriteFont, textToDraw, drawPosition, mainColor);
        }

        /// <summary>
        /// Set the text to draw
        /// </summary>
        /// <param name="text">Text</param>        
        public void SetTexts(string text)
        {
            Vector2 textSize = spriteFont.MeasureString(text);
            switch (this.textAlignment)
            {
                case TextAlignment.Center:
                    drawPosition = new Vector2(textPosition.X - textSize.X / 2, textPosition.Y - textSize.Y / 2);
                    break;
                case TextAlignment.Left:
                    drawPosition = new Vector2(textPosition.X, textPosition.Y);
                    break;
                case TextAlignment.Right:
                    drawPosition = new Vector2(textPosition.X+textSize.X, textPosition.Y);
                    break;
            }
            textToDraw = text;
        }
    }
}
