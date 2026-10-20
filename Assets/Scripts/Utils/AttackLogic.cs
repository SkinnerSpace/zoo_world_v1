using UnityEngine;

namespace ZooWorld.Utils
{
    public static class AttackLogic
    {
        public static bool DoesFirstWin(
            Vector2 positionA,
            Vector2 forwardA,
            Vector2 positionB,
            Vector2 forwardB)
        {
            var direction = positionB - positionA;

            direction.Normalize();

            var advantageA = Vector2.Dot(direction, forwardA);
            var advantageB = Vector2.Dot(-direction, forwardB);

            return advantageA >= advantageB;
        }
    }
}
