namespace Paul.PhysicsSimulator.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.Xna.Framework;
    using Paul.PhysicsSimulator.Engine.Graphics.Shapes;
    using Paul.PhysicsSimulator.Engine.Physics.RigidBodies;

    [TestClass]
    public class RigidBodyTest
    {
        [TestMethod]
        public void Cube_GetPointInWorldSpace_Centered_OK()
        {
            //Setup
            Vector3 cubePosition = new Vector3(4, 4, 4);
            Vector3 localPoint = new Vector3(0, 0, 0);

            RigidBody rigidBody = new RigidBody();
            rigidBody.Position = cubePosition;            

            //Action
            Vector3 worldPoint = rigidBody.GetPointInWorldSpace(localPoint);

            //Assert
            Assert.AreEqual(worldPoint, cubePosition);
        }

        [TestMethod]
        public void Cube_GetPointInWorldSpace_NotCentered_OK()
        {
            //Setup
            Vector3 cubePosition = new Vector3(4, 4, 4);
            Vector3 localPoint = new Vector3(1, 1, 0);
            Vector3 worldPoint_expeted = new Vector3(5, 5, 4);

            RigidBody rigidBody = new RigidBody();
            rigidBody.Position = cubePosition;

            //Action
            Vector3 worldPoint = rigidBody.GetPointInWorldSpace(localPoint);

            //Assert
            Assert.AreEqual(worldPoint, worldPoint_expeted);
        }
    }
}
