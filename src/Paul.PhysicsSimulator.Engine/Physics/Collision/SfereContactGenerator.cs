using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Paul.PhysicsSimulator.Engine.Physics.Particles;

namespace Paul.PhysicsSimulator.Engine.Physics.Collision
{
    public class SfereContactGenerator:ParticleContactGenerator
    {        
        ParticleWorld pworld;
        public SfereContactGenerator(ParticleWorld particleWorld)
        {
            pworld = particleWorld;
        }        

        public uint addContact(List<ParticleContact> contact, uint limit)
        {           
            int i,j;
            uint ris = 0;
            ////List<Sfera> sfere = new List<Sfera>();
            ////Sfera sfTmp=null;            
            //for (i = 0; i < pworld.particle.Count; i++)
            //{
            //    if (pworld.particle[i].GetType().Name=="Sfera")
            //    {
            //        sfTmp = (Sfera)pworld.particle[i];
            //        if (sfTmp != null)
            //        {
            //            sfere.Add(sfTmp);
            //        }
            //        sfTmp = null;
            //    }                
            //}            
            
            Vector3 normale;
            //Sfera sfera1,sfera2;
            float intersec = 0.0f;
            for (i = 0; i < pworld.Particles.Count; i++)
            {
                if ((pworld.Particles[i].Position.Y - 0.5f) <= 0.0f)                
                {
                    intersec = 0.0f;
                    intersec = pworld.Particles[i].Position.Y - 0.5f;
                    contact.Add(new ParticleContact(new Particle[] { pworld.Particles[i] }, Vector3.Up, 0.6f, intersec));
                    ris++;
                }
                //for (j = i + 1; j < pworld.particle.Count; j++)
                //{
                //    //Distanza dei centri < somma dei raggi
                //    intersec = (sfere[i].raggio + sfere[j].raggio)-(sfere[i].Position - sfere[j].Position).Length();
                //    if (intersec>0.0f)
                //    {
                //        sfera1 = sfere[i];
                //        sfera2 = sfere[j];                               
                //        normale = sfera1.Position - sfera2.Position;
                //        normale.Normalize();                        
                //        contact.Add(new ParticleContact(new Particle[] { sfera1,sfera2 }, normale, 1.0f, intersec));
                //        ris++;
                //    }
                //}
            }
            return ris;
        }
    }
}
