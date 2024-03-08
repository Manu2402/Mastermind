using OpenTK;

namespace Mastermind
{
    internal class Slot : GameObject
    {
        public Color Color;

        public Slot(string texturePath) : base(texturePath, DrawLayer.Playground)
        {
            sprite.pivot = new Vector2(0, HalfHeight);
        }

        public override void Destroy()
        {
            base.Destroy();
            Color?.Destroy();
        }

    }
}
