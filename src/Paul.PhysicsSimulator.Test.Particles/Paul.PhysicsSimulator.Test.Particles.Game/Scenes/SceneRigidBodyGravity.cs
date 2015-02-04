namespace Paul.PhysicsSimulator.Game.Scenes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Paul.PhysicsSimulator.Engine.Gaming;
    using Microsoft.Xna.Framework;
    using Paul.PhysicsSimulator.Engine.Graphics.Shapes;
    using Paul.PhysicsSimulator.Engine.Physics.Force;

    /// <summary>
    /// Gravity demostration.
    /// </summary>
    public class SceneRigidBodyGravity: BasicGameScene
    {
        public static string SceneDescription="RigidBody physics with Gravity";
        private string cubeModel = "Models\\Cubo";

        Cube cube;

        public SceneRigidBodyGravity(PhysicsGame physicsGame, Game xnaGame)
            : base(physicsGame, xnaGame)
        {
            cube = new Cube(xnaGame, new Vector3(1, 1, 1), cubeModel, Color.Red, true);
            cube.RigidBody.Position = new Vector3(0, 10, 0);            
            //this.physicsGame.RigidBodyWorld.ForceRegistry.Add(cube.RigidBody, new GravityForce(GravityForce.gravityConstant));

            //Cube cube2 = new Cube(xnaGame, new Vector3(1, 1, 1), "Models\\CuboTextures", null, false);
            //cube2.RigidBody.Position = new Vector3(0, 0, 0);
            //cube2.RigidBody.AddTorque(new Vector3(50, 50, 0));
            //this.physicsGame.AddComponent("cube2", cube2);
            
        }

        public override void InitializeScene()
        {
            base.InitializeScene();

            //Add the objects to the scene   
            this.physicsGame.AddComponent("cube", cube);
        }
    }
}
