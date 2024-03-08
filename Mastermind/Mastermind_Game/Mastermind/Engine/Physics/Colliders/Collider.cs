using OpenTK;

namespace Mastermind
{
    abstract class Collider
    {
        public Vector2 Offset;
        public RigidBody RigidBody;

        public Vector2 Position { get => RigidBody.Position + Offset; }

        public Collider(RigidBody owner, Vector2 offset)
        {
            RigidBody = owner;
            Offset = offset;
        }

        public abstract bool Contains(Vector2 point);
    }
}
