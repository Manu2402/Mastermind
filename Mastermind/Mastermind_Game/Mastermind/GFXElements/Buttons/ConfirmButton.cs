using Aiv.Fast2D;
using OpenTK;

namespace Mastermind
{
    internal class ConfirmButton : Button, IInputable, IHoverable
    {
        private Texture hoverTexture;
        private Sprite hoverSprite;

        public bool IsHover { get; set; }
        public bool IsClicked { get; set; }

        public ConfirmButton(string texturePath) : base(texturePath)
        {
            Position = new Vector2(Game.PixelsToUnits(561), Game.PixelsToUnits(687));

            RigidBody = new RigidBody(this);
            RigidBody.Collider = ColliderFactory.CreateBoxFor(this, new Vector2(HalfWidth, HalfHeight));

            hoverTexture = GfxMgr.GetTexture("Hover_Confirm");
            hoverSprite = new Sprite(Game.PixelsToUnits(hoverTexture.Width), Game.PixelsToUnits(hoverTexture.Height));
            hoverSprite.pivot = sprite.pivot;
            hoverSprite.position = Position;

            InputMgr.AddItem(this);
            HoverMgr.AddItem(this);
        }

        public override void Draw()
        {
            base.Draw();
            if (IsHover)
            {
                hoverSprite.DrawTexture(hoverTexture);
            }
        }

        public void Hover()
        {
            IsHover = RigidBody.Collider.Contains(Game.Window.MousePosition);
        }

        public override void Destroy()
        {
            base.Destroy();
            InputMgr.RemoveItem(this);
            HoverMgr.RemoveItem(this);
        }

        public void Input()
        {
            if (Game.Window.MouseLeft)
            {
                if (!IsClicked)
                {
                    if (RigidBody.Collider.Contains(Game.Window.MousePosition) && !Game.RulesControls.IsActive)
                    {
                        if (Game.PlayState)
                        {
                            IsClicked = true;
                            SlotMgr.CalculateRow(); //Coders calculus
                            SlotMgr.FollowingRow(); //Increment row
                        }
                        else
                        {
                            Game.ResumeGame(); //Reset assets to resume the game
                        }
                    }
                }
            }
            else
            {
                IsClicked = false;
            }
        }

    }
}
