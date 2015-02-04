namespace Paul.PhysicsSimulator.Game.UpdateBehaviour
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Define a generic Update behaviour
    /// </summary>
    public interface IUpdateBehaviour
    {
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="gameTime"></param>
        void Update(GameTime gameTime);
    }
}
