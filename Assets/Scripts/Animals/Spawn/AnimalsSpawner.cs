using System.Collections.Generic;
using ZooWorld.Application;
using UnityEngine;
using Zenject;
using ZooWorld.Infrastructure;
using Random = UnityEngine.Random;

namespace ZooWorld.Animals.Spawn
{
    public class AnimalsSpawner : MonoBehaviour
    {
        [SerializeField]
        private List<AnimalSpawnEntry> _entries;

        [SerializeField]
        private float _minSpawnIntervalSeconds = 1f;

        [SerializeField]
        private float _maxSpawnIntervalSeconds = 2f;

        private AnimalPoolsRegistry _registry;
        private StatisticsService _statisticsService;

        private AnimalSpawnTable _spawnTable;

        private float _timeUntilSpawnSeconds;

        [Inject]
        private void Construct(AnimalPoolsRegistry registry, StatisticsService statisticsService)
        {
            _registry = registry;
            _statisticsService = statisticsService;
        }

        private void Awake()
        {
            _spawnTable = new AnimalSpawnTable(_entries);

            foreach (var entry in _entries)
            {
                _registry.EnsureRegistered(entry.AnimalDefinition);
            }
        }

        private void Update()
        {
            _timeUntilSpawnSeconds -= Time.deltaTime;

            if (_timeUntilSpawnSeconds > 0f)
            {
                return;
            }

            Spawn();

            _timeUntilSpawnSeconds = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);
        }

        private void Spawn()
        {
            var randomAngle = Random.Range(0f, 360f);
            var randomRotation = Quaternion.Euler(0f, randomAngle, 0f);

            var animalDefinition = _spawnTable.Select(Random.Range(0, _spawnTable.TotalWeight));

            var pool = _registry.GetPool(animalDefinition);

            pool.Spawn(animalDefinition, new AnimalSpawnData(transform.position, randomRotation,
                diedAnimal =>
                {
                    pool.Despawn(diedAnimal);

                    _statisticsService.RecordAnimalDied(diedAnimal.Role);
                }));
        }
    }
}
