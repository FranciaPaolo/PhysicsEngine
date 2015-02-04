namespace Paul.PhysicsSimulator.Engine.Physics.RigidBodies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Paul.PhysicsSimulator.Engine.Gaming;

    /// <summary>
    /// Interface that define a game objcet with RigidBody physics behaviour.    
    /// </summary>
    public interface IRigidBodyComponent
    {
        /// <summary>
        /// RigidBody Physics component
        /// </summary>
        RigidBody RigidBody { get; set; }
    }
}
