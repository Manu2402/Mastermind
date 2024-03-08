using Aiv.Fast2D;
using OpenTK;

namespace Mastermind
{
    internal class HiddenSlot : Slot //Slots "READONLY"
    {
        private Texture questionTexture;
        private Sprite questionSprite;

        private bool hiddenColor;

        public HiddenSlot(string texturePath) : base(texturePath)
        {
            questionTexture = GfxMgr.GetTexture("Question");
            questionSprite = new Sprite(Game.PixelsToUnits(questionTexture.Width), Game.PixelsToUnits(questionTexture.Height));
            questionSprite.pivot = new Vector2(-0.2f, 0.15f);
            questionSprite.position = Vector2.Zero;
        }

        public void SetQuestionPos()
        {
            questionSprite.position = Position;
        }

        public override void Draw()
        {
            base.Draw();
            if (hiddenColor)
            {
                questionSprite.DrawTexture(questionTexture);
            }
        }

        public override void Update()
        {
            Color.IsActive = !hiddenColor;
        }

        public override void Destroy()
        {
            base.Destroy();
            questionTexture = null;
            questionSprite = null;
        }

        public void ShowColors(bool show)
        {
            hiddenColor = show;
        }

    }
}
