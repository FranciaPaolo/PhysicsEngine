namespace Paul.PhysicsSimulator.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Paul.PhysicsSimulator.Engine.Physics.Particles;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Contiene una serie di oggetti utili per i test.
    /// </summary>
    public class Scenario
    {
        #region Private
        public Scenario()
        {
            Initialize();
        }

        private void Initialize()
        {
            particles = new List<Particle>();

            //alfa
            Particle alfa = new Particle();
            alfa.Position = new Vector3(5, 0, 0);
            alfa.Mass = 1;
            particles.Add(alfa);

            //alfa
            Particle zero = new Particle();
            zero.Position = new Vector3(0, 0, 0);
            zero.Mass = 1;
            particles.Add(zero);

            //particleWorld
            particleWorld = new ParticleWorld(50, 50);
        }

        private List<Particle> particles;
        private ParticleWorld particleWorld;
        #endregion

        public Particle Particle_alfa
        {
            get
            {
                return particles.Where(p => (p.Position.X == 5 && p.Position.Y == 0 && p.Position.Z == 0)).FirstOrDefault();
            }
        }
        public Particle Particle_zero
        {
            get
            {
                return particles.Where(p => (p.Position.X == 0 && p.Position.Y == 0 && p.Position.Z == 0)).FirstOrDefault();
            }
        }
        public ParticleWorld Particle_World
        {
            get { return particleWorld; }
        }
    }
}
