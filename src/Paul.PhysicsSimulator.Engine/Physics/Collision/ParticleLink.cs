using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using Microsoft.Xna.Framework;
using Paul.PhysicsSimulator.Engine.Physics.Particles;

namespace Paul.PhysicsSimulator.Engine.Physics.Collision
{
    /// <summary>
    /// Rappresenta un collegamento tra Particles. 
    /// E' una classe base usata per Cavi (ParticleCable) e Aste (ParticleRod).
    /// </summary>
    public class ParticleLink : ParticleContactGenerator
    {
        protected Particle p1;
        protected Particle p2;

        public ParticleLink()
        {            
        }
        
        protected float currentLenght()
        {
            Vector3 relativePos = p1.Position - p2.Position;
            return relativePos.Length();
        }

        public virtual uint addContact(List<ParticleContact> contact, uint limit)
        {
            return 0;
        }
    }

    /// <summary>
    /// Rappresenta un cavo che collega 2 particelle
    /// </summary>
    public class ParticleCable:ParticleLink
    {
        protected float maxLenght;
        protected float restitution;
        
        public ParticleCable(Particle p1, Particle p2, float cableMaxLenght, float coeffRestitution)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.maxLenght = cableMaxLenght;
            this.restitution = coeffRestitution;
        }

        public override uint addContact(List<ParticleContact> contact, uint limit)
        {
            float length = currentLenght();
            if (length < maxLenght)
            {
                return 0;
            }

            Vector3 normal = p2.Position - p1.Position;
            normal.Normalize();

            contact.Add(new ParticleContact(new Particle[] { p1, p2 }, normal, restitution, length - maxLenght));

            return 1;
        }
    }
}
