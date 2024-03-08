using Aiv.Fast2D;
using OpenTK;

namespace Mastermind
{
    internal class RulesButton : Button, IInputable, IHoverable
    {
        private Texture hoverTexture;
        private Sprite hoverSprite;

        public bool IsClicked { get; set; }
        public bool IsHover { get; set; }

        public RulesButton(string texturePath) : base(texturePath)
        {
            Position = new Vector2(Game.PixelsToUnits(561), Game.PixelsToUnits(195));

            RigidBody = new RigidBody(this);
            RigidBody.Collider = ColliderFactory.CreateBoxFor(this, new Vector2(HalfWidth, HalfHeight));

            IsClicked = false;

            hoverTexture = GfxMgr.GetTexture("Hover_Rules");
            hoverSprite = new Sprite(Game.PixelsToUnits(hoverTexture.Width), Game.PixelsToUnits(hoverTexture.Height));
            hoverSprite.pivot = sprite.pivot;
            hoverSprite.position = Position;

            InputMgr.AddItem(this);
            HoverMgr.AddItem(this);
        }

        public void Input()
        {
            if (Game.Window.MouseLeft && !CheckRulesWindowIsOpen())
            {
                if (!IsClicked)
                {
                    if (RigidBody.Collider.Contains(Game.Window.MousePosition) && !Game.RulesControls.IsActive)
                    {
                        EnableRulesWindow(true);
                    }

                    IsClicked = true;
                }
            }
            else
            {
                IsClicked = false;
            }
        }

        public override void Destroy()
        {
            base.Destroy();
            InputMgr.RemoveItem(this);
            HoverMgr.RemoveItem(this);
        }

        public override void Draw()
        {
            base.Draw();
            if (IsHover)
            {
                hoverSprite.DrawTexture(hoverTexture);
            }
        }

        public bool CheckRulesWindowIsOpen()
        {
            return Game.RulesControls.IsActive;
        }

        public void EnableRulesWindow(bool enabled)
        {
            Game.RulesControls.IsActive = enabled;
        }

        public void Hover()
        {
            IsHover = RigidBody.Collider.Contains(Game.Window.MousePosition);
        }

    }
}
