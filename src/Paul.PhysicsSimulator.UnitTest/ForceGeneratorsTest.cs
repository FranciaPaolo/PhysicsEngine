using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paul.PhysicsSimulator.Engine.Physics.Particles;
using System.Data;
using Paul.PhysicsSimulator.Engine.Gaming;
using Paul.PhysicsSimulator.Engine.Physics.Force;
using Paul.PhysicsSimulator.UnitTest.Utilities;

namespace Paul.PhysicsSimulator.UnitTest
{
    /// <summary>
    /// Test the forces updated by Physiscs Engine
    /// </summary>
    [TestClass]
    public class ForceGeneratorsTest
    {
        Scenario scenario;
        ExpectedData expectedData;

        [TestInitialize]
        public void Initialize()
        {
            scenario = new Scenario();
            expectedData = new ExpectedData();
        }

        [TestMethod]
        public void Test_NearEquals()
        {
            float a = 1.456789f;
            float b = 1.456780f;

            Assert.IsTrue(a.NearlyEquals(b, 0.00001f));
        }

        /// <summary>
        /// Test on the particle the updates of position and velocity updated by the Spring Force generator
        /// </summary>
        [TestMethod]
        [DeploymentItem("ExpectedData\\Spring simulation.xlsx")]
        public void Test_ParticleSpring_Update()
        {
            Particle alfa = scenario.Particle_alfa;
            Particle zero = scenario.Particle_zero;
            alfa.Position = new Microsoft.Xna.Framework.Vector3(5, 2, 0);
            alfa.Mass = 1;
            zero.Position = new Microsoft.Xna.Framework.Vector3(0, 2, 0);
            zero.Mass = 1;

            ParticleWorld particleWorld = scenario.Particle_World;
            DataTable data = expectedData.GetParticleSpring_Data();

            particleWorld.Particles.Add(alfa);
            particleWorld.forceRegistry.Add(alfa,new SpringForce(zero,1f,(float)Math.Abs((zero.Position-alfa.Position).Length())*1.5f));

            float precision = 0.0001f;
            float duration = 0;
            float expectedPosition = 0;
            float expectedVelocity = 0;
            float expectedAcceleration = 0;

            for (int i = 0; i < data.Rows.Count; i++)
            {
                duration = float.Parse(data.Rows[i]["duration (s)"].ToString());
                expectedPosition = float.Parse(data.Rows[i]["position (m)"].ToString());
                expectedVelocity = float.Parse(data.Rows[i]["velocity (m/s)"].ToString());
                expectedAcceleration = float.Parse(data.Rows[i]["acceleration (m/s^2)"].ToString());
                
                Assert.IsTrue(alfa.Position.X.NearlyEquals(expectedPosition, precision));
                Assert.IsTrue(alfa.Velocity.X.NearlyEquals(expectedVelocity, precision));
                Assert.IsTrue(alfa.LinearAcceleration.X.NearlyEquals(expectedAcceleration, precision));

                particleWorld.RunPhysics(duration);
                
            }

        }
    }
}
