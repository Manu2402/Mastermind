using OpenTK;

namespace Mastermind
{
    class RigidBody
    {
        public GameObject GameObject;
        public Collider Collider;

        public bool IsActive { get { return GameObject.IsActive; } }

        public Vector2 Position { get { return GameObject.Position; } }

        public RigidBody(GameObject owner)
        {
            GameObject = owner;
        }

        public void Destroy()
        {
            GameObject = null;
            if (Collider != null)
            {
                Collider.RigidBody = null;
                Collider = null;
            }
        }

    }
}
