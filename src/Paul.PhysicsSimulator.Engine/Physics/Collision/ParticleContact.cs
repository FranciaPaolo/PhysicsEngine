using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Paul.PhysicsSimulator.Engine.Physics.Particles;

namespace Paul.PhysicsSimulator.Engine.Physics.Collision
{
    /// <summary>
    /// Contatto tra particelle
    /// </summary>
    public class ParticleContact
    {
        public ParticleContact(Particle[] twoParticles,Vector3 vContactNormal,float coeffRestitution,float DepthPenetration)
        {
            //particle = new Particle[2];
            particle = twoParticles;
            contactNormal = vContactNormal;
            restitution = coeffRestitution;
            DepthPenetration = penetration;
        }

        public Particle[] particle;//Una o Due particelle
        public Vector3 contactNormal;//Normale di contatto
        public float restitution;//Coefficiente di restituzione
        public float penetration;//intersezione tra le due particelle

        public void Resolve(float duration)
        {
            ResolveVelocity(duration);
            ResolveInterpenetration(duration);            
        }

        /// <summary>
        /// Calcola la velocità di separazione (Vs) tra le 2 particelle. Vs>0 le particelle si allontanano.
        /// </summary>
        /// <returns></returns>
        public float CalculateSeparatingVelocity()
        {
            Vector3 relativeVelocity = particle[0].Velocity;
            if (particle.Length > 1)
                relativeVelocity -= particle[1].Velocity;
            return Vector3.Dot(relativeVelocity,contactNormal);
        }

        /// <summary>
        /// Applica alle particelle implicate nella collisione l'impulso necessario
        /// </summary>
        /// <param name="duration"></param>
        private void ResolveVelocity(float duration)
        {           
            float separatingVelocity = CalculateSeparatingVelocity();

            //Console.WriteLine(separatingVelocity);
            if (separatingVelocity>0.0f)
            {
                //The contact is either stationary or separating
                //no impulse requied
                return;
            }

            float newSeparatingVelocity = -separatingVelocity * restitution;

            ////Check the velocity build-up due to acceleration only
            //Vector3 accCausedVelocity = particle[0].Acceleration;
            //if (particle.Length > 1)
            //    accCausedVelocity -= particle[1].Acceleration;
            //float accCausedSepVelocity = Vector3.Dot(accCausedVelocity, contactNormal) * duration;

            ////if we've got a closing velocity due to acceleration build-up
            ////remove it from separating velocity
            //if (accCausedSepVelocity < 0)
            //{
            //    newSeparatingVelocity += restitution * accCausedSepVelocity;
            //    if (newSeparatingVelocity < 0)
            //        newSeparatingVelocity = 0;
            //}
            ////END Check the velocity build-up due to acceleration only

            float deltaVelocity = newSeparatingVelocity - separatingVelocity;
            float totalInversMass = particle[0].InverseMass;
            if (particle.Length > 1)
                totalInversMass += particle[1].InverseMass;
            if (totalInversMass <= 0)
                return;

            //Calcola l'impulso da applicare
            float impulse = deltaVelocity / totalInversMass;
            Vector3 impulsePerIMass = contactNormal * impulse;

            //Apply impulses
            particle[0].Velocity+=impulsePerIMass * particle[0].InverseMass;
            if(particle.Length>1)
                particle[1].Velocity += impulsePerIMass *(- particle[1].InverseMass);           
        }

        private void ResolveInterpenetration(float duration)
        {
            //non c'è interpenetration
            if (penetration <= 0)   return;

            float totalInversMass = particle[0].InverseMass;
            if (particle.Length > 1)
                totalInversMass += particle[1].InverseMass;

            if (totalInversMass <= 0) return;

            Vector3 movePerIMass = contactNormal * (-penetration / totalInversMass);

            particle[0].Position += movePerIMass * particle[0].InverseMass;
            if(particle.Length>1)
                particle[1].Position += movePerIMass * particle[1].InverseMass;
        }
    }

    /// <summary>
    /// Generatore di Contact tra particelle, individua i contatti in modo da svincolare la generazione di contatti dalla risoluzione.
    /// </summary>
    public interface ParticleContactGenerator
    {
        /// <summary>
        /// Fills the given contact structure with the generated contact.
        /// </summary>
        /// <returns></returns>
        uint addContact(List<ParticleContact> contact, uint limit);        
    }
}
