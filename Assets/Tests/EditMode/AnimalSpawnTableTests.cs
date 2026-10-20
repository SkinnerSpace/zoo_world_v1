using System;
using NUnit.Framework;
using UnityEngine;
using ZooWorld.Animals;
using ZooWorld.Animals.Spawn;
using Object = UnityEngine.Object;

namespace ZooWorld.Tests
{
    public class AnimalSpawnTableTests
    {
        private AnimalDefinitionSO _frog;
        private AnimalDefinitionSO _snake;

        [SetUp]
        public void SetUp()
        {
            _frog = ScriptableObject.CreateInstance<AnimalDefinitionSO>();
            _snake = ScriptableObject.CreateInstance<AnimalDefinitionSO>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_frog);
            Object.DestroyImmediate(_snake);
        }

        [Test]
        public void Select_PicksEntryByWeight()
        {
            var table = new AnimalSpawnTable(new[]
            {
                new AnimalSpawnEntry { AnimalDefinition = _frog, Weight = 2 },
                new AnimalSpawnEntry { AnimalDefinition = _snake, Weight = 3 }
            });

            Assert.AreEqual(_frog, table.Select(0));
            Assert.AreEqual(_frog, table.Select(1));
            Assert.AreEqual(_snake, table.Select(2));
            Assert.AreEqual(_snake, table.Select(4));
        }

        [Test]
        public void NoPositiveWeight_Throws()
        {
            var entries = new[] { new AnimalSpawnEntry { AnimalDefinition = _frog, Weight = 0 } };

            Assert.Throws<InvalidOperationException>(() => new AnimalSpawnTable(entries));
        }
    }
}
