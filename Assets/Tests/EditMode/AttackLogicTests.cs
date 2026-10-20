using NUnit.Framework;
using UnityEngine;
using ZooWorld.Utils;

namespace ZooWorld.Tests
{
    public class AttackLogicTests
    {
        [Test]
        public void AttackFromBehind_Wins()
        {
            Assert.IsTrue(AttackLogic.DoesFirstWin(Vector2.zero, Vector2.right, Vector2.right, Vector2.right));
        }

        [Test]
        public void FacingAway_Loses()
        {
            Assert.IsFalse(AttackLogic.DoesFirstWin(Vector2.zero, Vector2.left, Vector2.right, Vector2.left));
        }
    }
}
