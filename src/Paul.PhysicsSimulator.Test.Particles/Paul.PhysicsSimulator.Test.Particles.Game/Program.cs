using System;

namespace Paul.PhysicsSimulator.Game
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (GameRigidBodyTest game = new GameRigidBodyTest())
            {
                game.Run();
            }
        }

        //static void Main(string[] args)
        //{
        //    using (GameParticleTest game = new GameParticleTest())
        //    {
        //        game.Run();
        //    }
        //}
    }
#endif
}

