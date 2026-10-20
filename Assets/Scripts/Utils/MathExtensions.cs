using UnityEngine;

namespace ZooWorld.Utils
{
    public static class MathExtensions
    {
        public static Vector2 ToXZ(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.z);
        }

        public static Vector3 ToXZVector3(this Vector2 vector2)
        {
            return new Vector3(vector2.x, 0f, vector2.y);
        }
    }
}
