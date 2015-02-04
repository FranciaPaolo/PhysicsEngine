using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Paul.PhysicsSimulator.Engine.Physics.Particles;

namespace Paul.PhysicsSimulator.Engine.Physics.Collision
{
    /// <summary>
    /// Rappresenta un asta tra che collega le particelle
    /// </summary>
    public class ParticleRod:ParticleLink
    {
        public float length;

        public ParticleRod(Particle p1, Particle p2, float rodLenght)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.length = rodLenght;
        }

        public override uint addContact(List<ParticleContact> contact, uint limit)
        {            
            float currentLen=currentLenght();
            if (currentLen== length)
                return 0;

            Vector3 normal = p2.Position - p1.Position;
            normal.Normalize();

            float penetration=currentLen - length;

            if (currentLen <= length)
            {
                normal = normal * -1;
                penetration = length - currentLen;
            }

            //no bounciness -> coeffRestitution=0            
            contact.Add(new ParticleContact(new Particle[] { p1, p2 }, normal, 0, penetration));

            return 1;
        }
    }
}
