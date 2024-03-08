using Aiv.Fast2D;
using OpenTK;

namespace Mastermind
{
    internal class RulesControlsConfig : GameObject, IInputable
    {
        public bool IsClicked { get; set; }

        public RulesControlsConfig(string texturePath) : base(texturePath, DrawLayer.GUI)
        {
            sprite.pivot = Vector2.Zero;

            InputMgr.AddItem(this);
        }

        public void Input()
        {
            if (IsActive)
            {
                if (Game.Window.GetKey(KeyCode.Return))
                {
                    if (!IsClicked)
                    {
                        IsClicked = true;
                        IsActive = false;
                    }
                }
                else
                {
                    IsClicked = false;
                }
            }
        }

        public override void Destroy()
        {
            base.Destroy();
            InputMgr.RemoveItem(this);
        }

    }
}
