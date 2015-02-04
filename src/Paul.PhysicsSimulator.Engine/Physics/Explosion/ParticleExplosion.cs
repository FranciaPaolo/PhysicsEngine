using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Paul.PhysicsSimulator.Engine.Physics.Particles;
using Paul.PhysicsSimulator.Engine.Graphics.Camera;

namespace Paul.PhysicsSimulator.Engine.Physics.Explosion
{
    struct ParticleExp
    {
        public Vector3 position;
        public Vector3 velocity;
        //public float velocityModule;
        public Vector2 textureCoordinate;
        //public float distance = 0;//distanza percorsa
        public float pointSize;

        public static readonly VertexElement[] vertexElements =
        {
            //new VertexElement(0,0,VertexElementFormat.Vector3,VertexElementMethod.Default,VertexElementUsage.Position,0),
            //new VertexElement(0,24,VertexElementFormat.Single,VertexElementMethod.Default,VertexElementUsage.TextureCoordinate,0),
            //new VertexElement(0,32,VertexElementFormat.Single,VertexElementMethod.Default,VertexElementUsage.PointSize,0),
        };
        
    }


    public class ParticleExplosion
    {
        #region declaration
        //Particles
        ParticleExp[] particles;

        //Position
        Vector3 position;

        //Max Raius of Explosion
        float maxRadius;

        //Velocity m/s
        float maxVelocity;

        //Life
        int lifeLeft;

        //Round and particles counts
        int numParticlesPerRound;
        int maxParticles;
        static Random rnd = new Random();
        int roundTime;
        int timeSinceLastRound = 0;

        //Vertex and graphics info
        VertexDeclaration vertexDeclaration;
        GraphicsDevice graphicsDevice;

        //Texture
        Vector2 textureSize;

        //Settings
        ExplosionParticleSettings particleSettings;

        //Array Indicies
        int endOfLiveParticlesIndex = 0;
        int endOfDeadParticlesIndex = 0;
        #endregion
        public bool IsDead
        {
            get { return endOfDeadParticlesIndex == maxParticles; }
        }
        public ParticleExplosion(GraphicsDevice graphicsDevice, Vector3 position,
            int lifeLeft, int roundTime, int numParticlesPerRound, int maxParticles,
            Vector2 textureSize, ExplosionParticleSettings particleSettings, float maxRadius,float maxVelocity)
        {
            this.position = position;
            this.graphicsDevice = graphicsDevice;
            this.lifeLeft = lifeLeft;
            this.numParticlesPerRound = numParticlesPerRound;
            this.maxParticles = maxParticles;
            this.roundTime = roundTime;
            this.textureSize = textureSize;
            this.particleSettings = particleSettings;
            this.maxRadius = maxRadius;
            this.maxVelocity = maxVelocity;

            vertexDeclaration = new VertexDeclaration(new VertexElement[]{});
            particles = new ParticleExp[maxParticles];
            InitializeParticles();
        }

        public void InitializeParticles()
        {
            //Loop untile max particles
            for (int i = 0; i < maxParticles; i++)
            {
                //Assign a random texture coordinate for color
                particles[i].textureCoordinate = new Vector2(
                    rnd.Next(0, (int)textureSize.X) / textureSize.X,
                    rnd.Next(0, (int)textureSize.Y) / textureSize.Y);

                //All particles start where the explosion began
                particles[i].position = position;

                //Create random velocity
                Vector3 direction = new Vector3(
                    (float)rnd.NextDouble() * 2 - 1,
                    (float)rnd.NextDouble() * 2 - 1,
                    (float)rnd.NextDouble() * 2 - 1);
                direction.Normalize();

                direction *= (float)rnd.NextDouble()*maxVelocity;
                particles[i].velocity = direction;
                //particles[i].velocityModule = particles[i].velocity.Length();
                //particles[i].distance = 0;

                particles[i].pointSize = (float)rnd.NextDouble() * particleSettings.maxSize;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (lifeLeft > 0)
                lifeLeft -= gameTime.ElapsedGameTime.Milliseconds;

            timeSinceLastRound += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastRound > roundTime)
            {
                //new round add and remove particles
                timeSinceLastRound -= roundTime;

                if (endOfLiveParticlesIndex < maxParticles)
                {
                    endOfLiveParticlesIndex += numParticlesPerRound;
                    if (endOfLiveParticlesIndex > maxParticles)
                        endOfLiveParticlesIndex = maxParticles;
                }

                if (lifeLeft <= 0)
                {
                    if (endOfDeadParticlesIndex < maxParticles)
                    {
                        endOfDeadParticlesIndex += numParticlesPerRound;
                        if (endOfDeadParticlesIndex > maxParticles)
                            endOfDeadParticlesIndex = maxParticles;
                    }

                }

                float duration=gameTime.ElapsedGameTime.Milliseconds/1000.0f;
                //Update position
                for (int i = endOfDeadParticlesIndex; i < endOfLiveParticlesIndex; i++)
                {
                    particles[i].position += particles[i].velocity*duration;
                    particles[i].textureCoordinate = new Vector2(
                        rnd.Next(0, (int)textureSize.X) / textureSize.X,
                        rnd.Next(0, (int)textureSize.Y) / textureSize.Y);
                    //particles[i].distance += particles[i].velocityModule * duration;                    
                }
            }
        }

        public void Draw(CameraMoveable camera,Effect effect)
        {
            if (endOfLiveParticlesIndex - endOfDeadParticlesIndex > 0)
            {
                //graphicsDevice.VertexDeclaration = vertexDeclaration;

                effect.Parameters["WorldViewProjection"].SetValue(camera.CameraView * camera.CameraProjection);

                //effect.Begin();
                foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                {
                    //pass.Begin();
                    //graphicsDevice.DrawUserPrimitives<ParticleExp>(PrimitiveType.PointList, particles, endOfDeadParticlesIndex, endOfLiveParticlesIndex - endOfDeadParticlesIndex);
                    //pass.End();
                }
                //effect.End();
            }
        }
    }
}