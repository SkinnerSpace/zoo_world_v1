using System;
using R3;
using UnityEngine;
using Zenject;
using ZooWorld.Animals.Interactions;
using ZooWorld.Animals.Spawn;

namespace ZooWorld.Animals.Core
{
    public sealed class Animal : MonoBehaviour, IPoolable<AnimalDefinitionSO, AnimalSpawnData>
    {
        // ReSharper disable once ClassNeverInstantiated.Global
        public class Pool : MonoPoolableMemoryPool<AnimalDefinitionSO, AnimalSpawnData, Animal>
        {
            public AnimalDefinitionSO Definition { get; private set; }

            [Inject]
            public void Construct(AnimalDefinitionSO definition)
            {
                Definition = definition;
            }
        }

        [SerializeField]
        private AnimalInteractionBehavior _interactionBehavior;

        private bool _isAlive;
        private Action<Animal> _onDeath;
        private readonly Subject<Animal> _onAte = new ();

        public Observable<Animal> OnAte => _onAte;

        public AnimalDefinitionSO Definition { get; private set; }

        public FoodChainRole Role => _interactionBehavior.Role;

        public bool TryEat(Animal victim)
        {
            if (!_isAlive || !victim.TryDie())
            {
                return false;
            }

            _onAte.OnNext(victim);

            return true;
        }

        private bool TryDie()
        {
            if (!_isAlive)
            {
                return false;
            }

            _isAlive = false;

            _onDeath.Invoke(this);

            return true;
        }

        void IPoolable<AnimalDefinitionSO, AnimalSpawnData>.OnSpawned(
            AnimalDefinitionSO definition, AnimalSpawnData spawnData
        )
        {
            Definition = definition;

            transform.position = spawnData.Position;
            transform.rotation = spawnData.Rotation;
            _onDeath = spawnData.OnDeath;

            _isAlive = true;
        }

        void IPoolable<AnimalDefinitionSO, AnimalSpawnData>.OnDespawned()
        {
            _isAlive = false;
            _onDeath = null;
        }
    }
}
