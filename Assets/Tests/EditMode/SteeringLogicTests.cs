using NUnit.Framework;
using UnityEngine;
using ZooWorld.Animals.Movement;
using ZooWorld.Infrastructure;

namespace ZooWorld.Tests
{
    public class SteeringLogicTests
    {
        private readonly CameraBorders _borders = new (-10f, 10f, -10f, 10f);

        [Test]
        public void KeepsDirectionUntilIntervalElapses()
        {
            var steering = new SteeringLogic(new System.Random(1), 1f, 1f);

            steering.Tick(0.1f, Vector2.zero, _borders);
            var direction = steering.Direction;

            steering.Tick(0.5f, Vector2.zero, _borders);
            Assert.AreEqual(direction, steering.Direction);

            steering.Tick(0.6f, Vector2.zero, _borders);
            Assert.AreNotEqual(direction, steering.Direction);
        }

        [Test]
        public void OutsideBorders_TurnsBackImmediately()
        {
            var steering = new SteeringLogic(new System.Random(1), 1f, 1f);
            steering.Tick(0.1f, Vector2.zero, _borders);

            steering.Tick(0.1f, new Vector2(15f, 0f), _borders);

            Assert.AreEqual(Vector2.left, steering.Direction);
        }
    }
}
