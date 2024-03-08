namespace Mastermind
{
    internal class Color : GameObject
    {
        public byte IndexColor;

        public Color(string texturePath, Colors color) : base(texturePath, DrawLayer.Middleground)
        {
            IndexColor = PinMgr.GetIndex(color);
        }
    }
}
