using System;

namespace Mastermind
{
    //Class for generate pseudo-random numbers (byte, float)

    static class RandomGenerator
    {
        private static Random random;

        static RandomGenerator()
        {
            random = new Random();
        }

        public static byte GetRandomByte(byte min, byte max)
        {
            return (byte)random.Next(min, max);
        }

        public static float GetRandomFloat()
        {
            return (float)random.NextDouble();
        }
    }
}
