namespace Paul.PhysicsSimulator.Engine.Gaming
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Component that need to updated
    /// </summary>
    public interface IUpdateable
    {
        /// <summary>
        /// Update the status of the component
        /// </summary>
        /// <param name="secondElapsed">Time elapsed since last update (0 for the first time)</param>
        void Update(float secondElapsed);
    }
}
