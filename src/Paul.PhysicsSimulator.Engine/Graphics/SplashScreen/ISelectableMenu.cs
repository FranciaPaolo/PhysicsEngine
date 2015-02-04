namespace Paul.PhysicsSimulator.Engine.Graphics.SplashScreen
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Generic menu that enable to select items.
    /// </summary>
    public interface ISelectableMenu : Paul.PhysicsSimulator.Engine.Gaming.IDrawable,
                                    Paul.PhysicsSimulator.Engine.Gaming.IUpdateable,
                                    Paul.PhysicsSimulator.Engine.Gaming.IGameComponent
    {
        /// <summary>
        /// Get or Set the index of the current selected item.
        /// </summary>
        int SelectedIndex { get; set; }

        /// <summary>
        /// Get the list of the menu items
        /// </summary>
        IEnumerable<string> MenuItems { get; }

        /// <summary>
        /// Notify when the seleted index is changed
        /// </summary>
        event EventHandler OnSelectedIndexChanged;
    }
}
