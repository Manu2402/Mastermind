using OpenTK;
using System;

namespace Mastermind
{
    static class ColliderFactory
    {
        //Get circle or box collider

        public static CircleCollider CreateCircleFor(GameObject obj, Vector2 offset, bool innerCircle = true)
        {
            float radius;

            if (innerCircle)
            {
                if (obj.HalfWidth < obj.HalfHeight)
                {
                    radius = obj.HalfWidth;
                }
                else
                {
                    radius = obj.HalfHeight;
                }
            }
            else
            {
                radius = (float)Math.Sqrt(obj.HalfWidth * obj.HalfWidth + obj.HalfHeight * obj.HalfHeight);
            }

            return new CircleCollider(obj.RigidBody, radius, offset);
        }

        public static BoxCollider CreateBoxFor(GameObject obj, Vector2 offset)
        {
            return new BoxCollider(obj.RigidBody, (float)obj.HalfWidth, (float)obj.HalfHeight, offset);
        }

    }
}
