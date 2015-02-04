namespace Paul.PhysicsSimulator.Engine.Physics.Particles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;    
    using Paul.PhysicsSimulator.Engine.Physics.Collision;
    using Paul.PhysicsSimulator.Engine.Physics.Force;

    /// <summary>
    /// Tiene traccia di un set di particles, e gestisce l'aggiornamento per frame delle stesse.
    /// </summary>
    public class ParticleWorld
    {
        /// <summary>
        /// Crea un nuovo particle simulator che gestisce il dato numero di contatti per frame.
        /// Si può specificare il numero di iterazioni massime da usare.
        /// </summary>
        /// <param name="maxContacts">Massimo numero di contacts gestiti</param>
        /// <param name="iteration">Massimo numero di iterazioni nella risoluzione dei contacts</param>
        public ParticleWorld(uint maxContacts,uint iteration)
        {
            Particles = new List<Particle>();
            forceRegistry = new ForceRegistry();
            resolver=new ParticleContactResolver(iteration);
            contactGenRegistration = new List<ParticleContactGenerator>();
            contacts=new List<ParticleContact>();
        }

        #region Fields

        //List of particle
        public List<Particle> Particles
        { get; set; }

        //Hold the force generators for the particle in this world
        public ForceRegistry forceRegistry;

        //Hold the resolver for contacts
        public ParticleContactResolver resolver;

        //Hold the list of contact generator
        public List<ParticleContactGenerator> contactGenRegistration;

        //Hold the list of contacts
        List<ParticleContact> contacts;

        //Maximum number of contacts allowed (i.e., the size of the contacts array)
        uint maxContacts;

        #endregion


        /// <summary>
        /// Processes all the physics for the particle world.
        /// </summary>
        /// <param name="duration">Secondi</param>
        public void RunPhysics(float duration)
        {            
            //Applica tutti i force generator
            forceRegistry.Update(duration);

            //Integra le forze per aggiornarne la posizione
            Integrate(duration);

            //Genera i contacts tra le particles
            uint usedContacts = GenerateContacts();

            //Processa i contacts
            //if (calculateIterations) resolver.SetIterations(usedContacts * 2);   
            if(usedContacts>0)             
                resolver.ResolveContacts(contacts, usedContacts, duration);

            // Svuota gli accumularori di forze all'interno delle particelle
            //for (int i = 0; i < Particles.Count; i++)
            //    Particles[i].ClearAccumulator();
        }


        /// <summary>
        /// Calls each of the registered contact generators to report their contacts. Return the number of contact
        /// </summary>
        /// <returns></returns>
        private uint GenerateContacts()
        {
            //Manca il limite sul numero di contatti generati
            //int limit = maxContacts;
            //List<ParticleContact> pContacts = contacts;
            contacts.Clear();

            List<ParticleContactGenerator> pReg = contactGenRegistration;
            int i;
            uint used = 0;
            for (i = 0; i < pReg.Count; i++)
            {
                used += pReg[i].addContact(contacts, 0);//limit);
                //limit -= used;

                //if (limit <= 0) break;
            }
            //return the number of contacts used
            return used;//maxContacts-limit;
        }

        /// <summary>
        /// Integrates all the particles in this world forward in time.
        /// </summary>
        /// <param name="duration">Secondi</param>
        private void Integrate(float duration)
        {
            //List<Particle> elencoParticle = particle;
            int i;
            for (i = 0; i < Particles.Count; i++)
            {
                if (Particles[i].InverseMass != 0)
                    Particles[i].Update(duration);
            }
        }
    }
}
