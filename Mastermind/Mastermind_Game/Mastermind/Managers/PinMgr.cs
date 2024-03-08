using OpenTK;

namespace Mastermind
{
    enum Colors
    {
        Pink, Green, Yellow, Blue, Orange, Brown, X
    }

    enum BlockColors
    {
        Black, White, x
    }

    static class PinMgr
    {
        private static Pin[] pins;
        private static byte numPins = 6;

        public static void Init()
        {
            short posX = 531;
            short posY = 465;

            pins = new Pin[numPins];

            for (byte i = 0; i < numPins; i++)
            {
                pins[i] = new Pin($"Pin_{i}", (Colors)i);
                pins[i].Position = new Vector2(Game.PixelsToUnits(posX + (40 * i)), Game.PixelsToUnits(posY));
            }
        }

        public static byte GetIndex(Colors color)
        {
            return (byte)color;
        }

    }
}
