using OpenTK;

namespace Mastermind
{
    static class CoderMgr
    {
        private static Coder[] coders;
        private static byte numCoders = 9;

        public static void Init()
        {
            short posX = 412;
            short[] posY = new short[] { 35, 104, 175, 244, 314, 384, 455, 526, 597 }; //FindTuning

            coders = new Coder[numCoders];

            for (byte i = 0; i < numCoders; i++)
            {
                coders[i] = new Coder();
                coders[i].Position = new Vector2(Game.PixelsToUnits(posX), Game.PixelsToUnits(posY[i]));
            }
        }

    }
}
