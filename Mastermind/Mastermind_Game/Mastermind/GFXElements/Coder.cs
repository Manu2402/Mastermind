using OpenTK;

namespace Mastermind
{
    internal class Coder : GameObject
    {
        public Coder() : base("Coder", DrawLayer.Playground)
        {
            sprite.pivot = Vector2.Zero;
        }
    }
}
