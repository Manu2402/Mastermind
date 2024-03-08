using OpenTK;

namespace Mastermind
{
    class BoxCollider : Collider
    {
        protected float halfWidth;
        protected float halfHeight;

        public float Width { get { return halfWidth * 2; } }
        public float Height { get { return halfHeight * 2; } }

        public BoxCollider(RigidBody owner, float halfWidth, float halfHeight, Vector2 offset) : base(owner, offset)
        {
            this.halfWidth = halfWidth;
            this.halfHeight = halfHeight;
        }

        public override bool Contains(Vector2 point)
        {
            return
                point.X >= Position.X - halfWidth &&
                point.X <= Position.X + halfWidth &&
                point.Y >= Position.Y - halfHeight &&
                point.Y <= Position.Y + halfHeight;
        }

    }
}
