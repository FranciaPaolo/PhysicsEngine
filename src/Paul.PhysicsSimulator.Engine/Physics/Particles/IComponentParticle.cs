namespace Paul.PhysicsSimulator.Engine.Physics.Particles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Paul.PhysicsSimulator.Engine.Gaming;

    /// <summary>
    /// Interface that define a game objcet with Particle physics behaviour.    
    /// </summary>
    public interface IComponentParticle
    {
        /// <summary>
        /// Particle Physics component
        /// </summary>
        Particle Particle{get; set;}
    }
}
