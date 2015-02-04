using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paul.PhysicsSimulator.Engine.Physics.Explosion
{
    public class ExplosionParticleSettings
    {
        public int maxSize=2;
    }
    public class ExplosionSettings
    {
        public int minLife = 1000;
        public int maxLife = 2000;

        public int minParticlePerRound = 100;
        public int maxParticlePerRound = 600;

        public int minRoundTime = 16;
        public int maxRoundTime = 50;

        public int minParticles = 2000;
        public int maxParticles = 3000;

        public float maxRaius = 1;

        public float maxVelocity = 27;//m/s
    }

    /// <summary>
    /// Classe inutile, contiene dei settaggi predefiniti
    /// </summary>
    public class ExplosionSetting
    {
        public int lifeLeft = 1000;        

        public int particlePerRound = 100;        

        public int roundTime = 16;        

        public int numParticles = 2000;        

        public float raius = 1;

        public float velocity = 27;//m/s


    }
}
