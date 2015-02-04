namespace Paul.PhysicsSimulator.UnitTest.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Estende la classe float con extension method di utility, ad esempio il confronto fino alla n-esima cifra decimale
    /// </summary>
    public static class FloatExtension
    {
        /// <summary>
        /// Confronta i due numeri float alla precisione specificata
        /// </summary>
        /// <param name="value1">primo numero float da confrontare</param>
        /// <param name="value2">secondo numero float da confrontare</param>
        /// <param name="unimportantDifference">quantità sotto la quale i 2 numeri vengono considerati uguali</param>
        /// <returns>Ritorna true se i due numeri float sono uguali alla precisione specificata</returns>
        public static bool NearlyEquals(this float value1, float value2, float unimportantDifference = 0.0001f)
        {
            return Math.Abs(value1 - value2) < unimportantDifference;
        }
    }
}
