using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;
using ZooWorld.Animals;
using ZooWorld.Animals.Core;

// ReSharper disable once ClassNeverInstantiated.Global
namespace ZooWorld.Infrastructure
{
    public class AnimalPoolsRegistry
    {
        private Dictionary<AnimalDefinitionSO, Animal.Pool> _pools;

        [Inject]
        private void Construct(List<Animal.Pool> pools)
        {
            _pools = pools.ToDictionary(pool => pool.Definition);
        }

        public Animal.Pool GetPool(AnimalDefinitionSO definition)
        {
            EnsureRegistered(definition);

            return _pools[definition];
        }

        public void EnsureRegistered(AnimalDefinitionSO definition)
        {
            if (!_pools.ContainsKey(definition))
            {
                throw new InvalidOperationException(
                    $"Animal definition {definition.name} has no pool. Add it to the animals list of GameInstaller."
                );
            }
        }
    }
}
