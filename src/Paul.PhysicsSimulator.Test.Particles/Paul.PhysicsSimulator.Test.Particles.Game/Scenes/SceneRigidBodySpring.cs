namespace Paul.PhysicsSimulator.Game.Scenes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Paul.PhysicsSimulator.Engine.Gaming;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SceneRigidBodySpring: BasicGameScene
    {
        public static string SceneDescription = "RigidBody physics with Spring";

        public SceneRigidBodySpring(PhysicsGame physicsGame, Game xnaGame)
            : base(physicsGame, xnaGame)
        { 
        }

        public override void InitializeScene()
        {
            base.InitializeScene();
        }
    }
}
