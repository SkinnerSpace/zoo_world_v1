using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Zenject;
using ZooWorld.Animals;
using ZooWorld.Animals.Core;
using ZooWorld.Animals.Spawn;

namespace ZooWorld.Tests
{
    public class AnimalTests
    {
        private readonly List<Animal> _deaths = new ();
        private readonly List<GameObject> _gameObjects = new ();

        [TearDown]
        public void TearDown()
        {
            _gameObjects.ForEach(Object.DestroyImmediate);
            _gameObjects.Clear();
            _deaths.Clear();
        }

        private Animal SpawnAnimal()
        {
            var gameObject = new GameObject();
            _gameObjects.Add(gameObject);

            var animal = gameObject.AddComponent<Animal>();
            ((IPoolable<AnimalDefinitionSO, AnimalSpawnData>)animal).OnSpawned(
                null, new AnimalSpawnData(Vector3.zero, Quaternion.identity, _deaths.Add)
            );

            return animal;
        }

        [Test]
        public void TryEat_KillsVictimOnlyOnce()
        {
            var snake = SpawnAnimal();
            var otherSnake = SpawnAnimal();
            var frog = SpawnAnimal();

            Assert.IsTrue(snake.TryEat(frog));
            Assert.IsFalse(otherSnake.TryEat(frog));
            Assert.AreEqual(1, _deaths.Count);
        }

        [Test]
        public void DeadAnimal_CannotEat()
        {
            var snake = SpawnAnimal();
            var otherSnake = SpawnAnimal();
            var frog = SpawnAnimal();

            otherSnake.TryEat(snake);

            Assert.IsFalse(snake.TryEat(frog));
            Assert.AreEqual(1, _deaths.Count);
        }
    }
}
