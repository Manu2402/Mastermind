using OpenTK;

namespace Mastermind
{
    class CircleCollider : Collider
    {
        public float Radius;

        public CircleCollider(RigidBody owner, float radius, Vector2 offset) : base(owner, offset)
        {
            Radius = radius;
        }

        public override bool Contains(Vector2 point)
        {
            Vector2 distFromCenter = point - Position;
            return distFromCenter.LengthSquared <= (Radius * Radius);
        }

    }
}
