using OpenTK;

namespace Mastermind
{
    internal class Block : GameObject
    {
        private short[] posX;
        private short[] posY;

        private byte xIndex;
        private static byte index = 0;
        private static byte yIndex = 0;

        public BlockColors BlockColor;

        public Block(string texturePath, bool decreaseIndex = false, byte i = 0) : base(texturePath, DrawLayer.Middleground)
        {
            //Positions
            posX = new short[] { 415, 444 };
            posY = new short[] { 600, 628, 528, 558, 458, 486, 386, 416, 317, 345, 247, 276, 177, 207, 107, 135, 38, 67 };

            Pivot = Vector2.Zero;

            SetBlockColor(texturePath); //Set color name for indexes

            if (decreaseIndex) index -= i; //Reset current index when RemoveWhite() is called

            xIndex = (byte)(index % posX.Length); //Calculate x,y
            yIndex = (byte)(index * 0.5f);

            Position = new Vector2(Game.PixelsToUnits(posX[xIndex]), Game.PixelsToUnits(posY[yIndex]));

            if (decreaseIndex) index += i; //Callback
            else index++;

            BlockMgr.Add(this);
        }

        private void SetBlockColor(string texturePath)
        {
            switch (texturePath)
            {
                case "Black":
                    BlockColor = BlockColors.Black;
                    break;

                case "White":
                    BlockColor = BlockColors.White;
                    break;

                case "x":
                    BlockColor = BlockColors.x;
                    break;
            }
        }

        public static void ResetIndex()
        {
            index = 0;
        }

    }
}
