using Aiv.Fast2D;
using OpenTK;

namespace Mastermind
{
    class GameObject : IUpdatable, IDrawable
    {
        // Variables
        protected int frameW;
        protected int frameH;

        protected Sprite sprite;
        protected Texture texture;

        public RigidBody RigidBody;
        public bool IsActive;

        // Properties
        public virtual Vector2 Position { get { return sprite.position; } set { sprite.position = value; } }
        public Vector2 Pivot { get => sprite.pivot; set => sprite.pivot = value; }
        public float X { get { return sprite.position.X; } set { sprite.position.X = value; } }
        public float Y { get { return sprite.position.Y; } set { sprite.position.Y = value; } }
        public float HalfWidth { get { return sprite.Width * 0.5f; } protected set { } }
        public float HalfHeight { get { return sprite.Height * 0.5f; } protected set { } }

        protected int textOffsetX, textOffsetY;

        public DrawLayer Layer { get; protected set; }

        public GameObject(string texturePath, DrawLayer layer = DrawLayer.Playground, int textOffsetX = 0, int textOffsetY = 0, float spriteWidth = 0, float spriteHeight = 0)
        {
            texture = GfxMgr.GetTexture(texturePath);
            float spriteW = spriteWidth > 0 ? spriteWidth : Game.PixelsToUnits(texture.Width);
            float spriteH = spriteHeight > 0 ? spriteHeight : Game.PixelsToUnits(texture.Height);
            sprite = new Sprite(spriteW, spriteH);

            Layer = layer;

            frameW = texture.Width;
            frameH = texture.Height;

            this.textOffsetX = textOffsetX;
            this.textOffsetY = textOffsetY;

            IsActive = true;

            sprite.pivot = new Vector2(sprite.Width * 0.5f, sprite.Height * 0.5f);

            UpdateMgr.AddItem(this);
            DrawMgr.AddItem(this);
        }

        public virtual void Update() { }

        public virtual void Draw()
        {
            if (IsActive)
            {
                sprite.DrawTexture(texture, textOffsetX, textOffsetY, frameW, frameH);
            }
        }

        public virtual void Destroy()
        {
            sprite = null;
            texture = null;

            UpdateMgr.RemoveItem(this);
            DrawMgr.RemoveItem(this);

            if (RigidBody != null)
            {
                RigidBody.Destroy();
                RigidBody = null;
            }
        }

    }
}
