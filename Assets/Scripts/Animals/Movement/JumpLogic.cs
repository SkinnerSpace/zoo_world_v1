using UnityEngine;
using ZooWorld.Utils;

namespace ZooWorld.Animals.Movement
{
    public static class JumpLogic
    {
        public static Vector3 CalculateLaunchVelocity(Vector2 direction, float distance, float height, float gravity)
        {
            var flightDurationSeconds = 2f * Mathf.Sqrt(2f * height / gravity);

            var horizontalSpeed = distance / flightDurationSeconds;
            var verticalSpeed = Mathf.Sqrt(2f * gravity * height);

            return direction.ToXZVector3() * horizontalSpeed + Vector3.up * verticalSpeed;
        }
    }
}
