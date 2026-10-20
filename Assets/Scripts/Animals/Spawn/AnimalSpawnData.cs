using System;
using UnityEngine;
using ZooWorld.Animals.Core;

namespace ZooWorld.Animals.Spawn
{
    public readonly struct AnimalSpawnData
    {
        public readonly Vector3 Position;
        public readonly Quaternion Rotation;
        public readonly Action<Animal> OnDeath;

        public AnimalSpawnData(Vector3 position, Quaternion rotation, Action<Animal> onDeath)
        {
            Position = position;
            Rotation = rotation;
            OnDeath = onDeath;
        }
    }
}
