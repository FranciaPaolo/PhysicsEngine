using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paul.PhysicsSimulator.Engine.Graphics.Camera;

namespace Paul.PhysicsSimulator.Engine.Physics.Explosion
{
    public class ExplosionManager
    {
        //private Texture2D texture;
        //private Effect effect;
        private List<ParticleExplosion> explosions;
        private ExplosionSetting explosionSetting;
        private ExplosionParticleSettings expParticleSetting;

        public ExplosionManager(Game game)
        {
            explosions = new List<ParticleExplosion>();
            explosionSetting = new ExplosionSetting();
            expParticleSetting = new ExplosionParticleSettings();

            //texture= game.Content.Load<Texture2D>("textures\\Particle");
            //effect = game.Content.Load<Effect>("Effect\\Explosion");
            //effect.CurrentTechnique = effect.Techniques["Textured"];
            //effect.Parameters["theTexture"].SetValue(texture);
        }

        public void AddExplosion(Vector3 position,GraphicsDevice graphicsDevice)
        { 
            //explosions.Add(new ParticleExplosion(graphicsDevice,position,explosionSetting.lifeLeft,explosionSetting.roundTime,explosionSetting.particlePerRound,explosionSetting.numParticles,new Vector2(texture.Width,texture.Height),expParticleSetting,explosionSetting.raius,explosionSetting.velocity));
        }

        //public void Draw(CameraMoveable camera)
        //{
        //    foreach (ParticleExplosion exp in explosions)
        //    {
        //        exp.Draw(camera,effect);
        //    }
        //}

        public void Update(GameTime gameTime)
        {
            int i;
            for (i = 0; i < explosions.Count; i++)
            {
                explosions[i].Update(gameTime);
                if (explosions[i].IsDead)
                {
                    explosions.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
