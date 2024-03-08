using OpenTK;

namespace Mastermind
{
    internal class Pin : GameObject, IInputable
    {
        private Colors colorName;

        public bool IsClicked { get; set; }

        public Pin(string texturePath, Colors color) : base(texturePath, DrawLayer.Playground)
        {
            sprite.pivot = Vector2.Zero;
            colorName = color;

            RigidBody = new RigidBody(this);
            RigidBody.Collider = ColliderFactory.CreateBoxFor(this, new Vector2(HalfWidth, HalfHeight));

            InputMgr.AddItem(this);
        }

        public override void Destroy()
        {
            base.Destroy();
            InputMgr.RemoveItem(this);
        }

        public void Input()
        {
            if (Game.Window.MouseLeft)
            {
                if (!IsClicked)
                {
                    if (Game.PlayState)
                    {
                        if (RigidBody.Collider.Contains(Game.Window.MousePosition) && !Game.RulesControls.IsActive)
                        {
                            IsClicked = true;
                            SlotMgr.SetColor(colorName);
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
