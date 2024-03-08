using Aiv.Fast2D;
using OpenTK;

namespace Mastermind
{
    internal class WinLoseText : GameObject
    {
        //Both the texts are managed into this class

        private Texture loseTexture;
        private Sprite loseSprite;

        public bool LoseSpriteIsActive;

        public WinLoseText(string texturePath) : base(texturePath)
        {
            loseTexture = GfxMgr.GetTexture("YouLose");
            loseSprite = new Sprite(Game.PixelsToUnits(loseTexture.Width), Game.PixelsToUnits(loseTexture.Height));

            sprite.pivot = Vector2.Zero;

            LoseSpriteIsActive = true;

            Position = new Vector2(Game.PixelsToUnits(543), Game.PixelsToUnits(577));
            loseSprite.position = new Vector2(Game.PixelsToUnits(535), Game.PixelsToUnits(577));
        }

        public override void Draw()
        {
            if (IsActive)
            {
                if (LoseSpriteIsActive) sprite.DrawTexture(loseTexture);
                else sprite.DrawTexture(texture);
            }
        }

        public override void Update()
        {
            IsActive = !Game.PlayState;
        }

        public override void Destroy()
        {
            base.Destroy();
            loseTexture = null;
            loseSprite = null;
        }

    }
}
