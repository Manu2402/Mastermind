using OpenTK;

namespace Mastermind
{
    internal class Indicator : GameObject
    {
        private short posX;
        private short[] posY;
        public sbyte playIndex = 0;

        public Indicator(string texturePath) : base(texturePath, DrawLayer.Playground)
        {
            sprite.pivot = new Vector2(Game.PixelsToUnits(45), Game.PixelsToUnits(18));

            posX = 89;
            posY = new short[] { 627, 556, 485, 414, 344, 274, 205, 134, 65, 65 };

            SetPosition(playIndex);
        }

        public void SetPosition(sbyte index)
        {
            Position = new Vector2(Game.PixelsToUnits(posX), Game.PixelsToUnits(posY[index = (index > 0) ? index : (sbyte)0])); //Assign index only in case of positive index
            if (index >= 9) //Max Index Turn
            {
                Game.PlayState = false;
                SlotMgr.ShowHiddenColors(false);
            }
        }

        public override void Update()
        {
            if (Game.PlayState) IsActive = true;
            else IsActive = false;
        }

        public void ResetPlayIndex()
        {
            playIndex = -1; //Negative number for skip a cycle
        }

    }
}
