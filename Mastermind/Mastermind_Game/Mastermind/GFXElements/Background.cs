using OpenTK;

namespace Mastermind
{
    internal class Background : GameObject
    {
        public Background(string texturePath) : base(texturePath, DrawLayer.Background)
        {
            sprite.pivot = Vector2.Zero;
        }

    }
}
