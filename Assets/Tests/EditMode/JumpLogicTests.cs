using NUnit.Framework;
using UnityEngine;
using ZooWorld.Animals.Movement;

namespace ZooWorld.Tests
{
    public class JumpLogicTests
    {
        [Test]
        public void Jump_ReachesHeightAndLandsAtDistance()
        {
            const float gravity = 9.81f;

            var velocity = JumpLogic.CalculateLaunchVelocity(Vector2.right, 4f, 3f, gravity);

            var apex = velocity.y * velocity.y / (2f * gravity);
            var distance = velocity.x * 2f * velocity.y / gravity;

            Assert.AreEqual(3f, apex, 0.001f);
            Assert.AreEqual(4f, distance, 0.001f);
        }
    }
}
