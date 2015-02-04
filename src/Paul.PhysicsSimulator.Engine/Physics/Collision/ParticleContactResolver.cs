using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Paul.PhysicsSimulator.Engine.Physics.Collision
{
    public class ParticleContactResolver
    {
        public ParticleContactResolver(uint iterations)
        {
            iteration = iterations;
        }
        public void SetIterations(uint iterations)
        {
            iteration = iterations;
        }

        public void ResolveContacts(List<ParticleContact> contactArray, uint numContacts,float duration)
        {
            if (contactArray.Count == 0)
                return;

            iterationUsed = 0;
            uint limit = iteration;
            if (iteration > numContacts)
                limit = numContacts;
            while (iterationUsed < limit)
            {
                //Find the contact with the larger closing velocity
                float max = 0;
                uint maxIndex = numContacts-1;
                for (uint i = 0; i < contactArray.Count; i++)
                {
                    float sepVel = contactArray[(int)i].CalculateSeparatingVelocity();
                    if (sepVel < max)
                    {
                        max = sepVel;
                        maxIndex = i;
                    }
                }

                //Resolve this contact
                contactArray[(int)maxIndex].Resolve(duration);
                iterationUsed++;
            }
        }


        protected uint iteration;
        protected uint iterationUsed;
    }
}
