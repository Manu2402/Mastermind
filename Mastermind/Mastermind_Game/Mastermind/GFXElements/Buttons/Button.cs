using OpenTK;

namespace Mastermind
{
    internal class Button : GameObject
    {
        public Button(string texturePath) : base(texturePath, DrawLayer.Playground)
        {
            sprite.pivot = Vector2.Zero;
        }

    }
}
