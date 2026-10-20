using UnityEngine;

namespace ZooWorld.Utils
{
    public static class RigidbodyExtensions
    {
        public static void RotateTowards(this Rigidbody rigidbody, Vector2 direction, float maxDegreesDelta)
        {
            if (direction == Vector2.zero)
            {
                return;
            }

            rigidbody.MoveRotation(Quaternion.RotateTowards(
                rigidbody.rotation, Quaternion.LookRotation(direction.ToXZVector3()), maxDegreesDelta
            ));
        }
    }
}
