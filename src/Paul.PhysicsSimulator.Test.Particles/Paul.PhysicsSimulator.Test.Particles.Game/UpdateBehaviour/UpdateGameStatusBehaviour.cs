namespace Paul.PhysicsSimulator.Game.UpdateBehaviour
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Paul.PhysicsSimulator.Engine.Gaming;

    /// <summary>
    /// Manage the game status:
    /// - when pressed the P key the game is paused
    /// - when pressed the ESC key exit the game
    /// </summary>
    public class UpdateGameStatusBehaviour:IUpdateBehaviour
    {
        KeyboardState savedKeyState;        

        public event EventHandler OnExit;
        public event EventHandler OnPause;

        public UpdateGameStatusBehaviour()
        {
        }

        public void Update(GameTime gameTime)        /// <summary>
        /// Update
        /// </summary>
        /// <param name="gameTime"></param>

        {
            if (savedKeyState.IsKeyUp(Keys.Escape) && Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                if (OnExit != null)
                    OnExit(this, new EventArgs());
            }

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.P) && savedKeyState.IsKeyUp(Keys.P))
            {
                if (OnPause != null)
                    OnPause(this, new EventArgs());
            }
            
            savedKeyState = keyState;
        }
    }
}
