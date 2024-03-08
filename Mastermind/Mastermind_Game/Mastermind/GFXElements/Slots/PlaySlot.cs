using Aiv.Fast2D;
using OpenTK;

namespace Mastermind
{
    internal class PlaySlot : Slot, IInputable, IHoverable //Playable Slots
    {
        private Texture selectorTexture;
        private Sprite selectorSprite;
        private bool isSelected;

        public Sprite SelectorSprite { get => selectorSprite; }
        public bool IsHover { get; set; }
        public bool IsSelected { get => isSelected; set => isSelected = value; }
        public bool IsClicked { get; set; }

        public PlaySlot(string texturePath) : base(texturePath)
        {
            isSelected = false;

            selectorTexture = GfxMgr.GetTexture("Selector");
            selectorSprite = new Sprite(Game.PixelsToUnits(selectorTexture.Width), Game.PixelsToUnits(selectorTexture.Height));
            selectorSprite.pivot = new Vector2(0.036f, selectorSprite.Height * 0.5f);

            RigidBody = new RigidBody(this);
            RigidBody.Collider = ColliderFactory.CreateCircleFor(this, new Vector2(HalfWidth, 0));

            HoverMgr.AddItem(this);
        }

        public override void Draw()
        {
            base.Draw();
            if (isSelected || IsHover)
            {
                selectorSprite.DrawTexture(selectorTexture);
            }
        }

        public void Input()
        {
            if (Game.Window.MouseLeft)
            {
                if (!IsClicked)
                {
                    if (!isSelected)
                    {
                        if (RigidBody.Collider.Contains(Game.Window.MousePosition) && !Game.RulesControls.IsActive)
                        {
                            SlotMgr.DisableAllSlots();
                            isSelected = true;
                            IsClicked = true;
                        }
                    }
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
            selectorTexture = null;
            selectorSprite = null;

            InputMgr.RemoveItem(this);
            HoverMgr.RemoveItem(this);
        }

        public void Hover()
        {
            IsHover = RigidBody.Collider.Contains(Game.Window.MousePosition);
        }

    }
}
