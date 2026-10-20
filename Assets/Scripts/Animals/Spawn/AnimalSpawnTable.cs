using System;
using System.Collections.Generic;

namespace ZooWorld.Animals.Spawn
{
    public class AnimalSpawnTable
    {
        public int TotalWeight { get; }

        private readonly (AnimalDefinitionSO Definition, int Weight)[] _entries;

        public AnimalSpawnTable(IReadOnlyList<AnimalSpawnEntry> entries)
        {
            _entries = new (AnimalDefinitionSO, int)[entries.Count];

            for (var i = 0; i < entries.Count; i++)
            {
                var definition = entries[i].AnimalDefinition;
                var weight = entries[i].Weight;

                if (definition == null)
                {
                    throw new InvalidOperationException($"Spawn entry {i} has no animal definition");
                }

                if (weight < 0)
                {
                    throw new InvalidOperationException($"Spawn entry {i} has negative weight {weight}");
                }

                _entries[i] = (definition, weight);
                TotalWeight += weight;
            }

            if (TotalWeight <= 0)
            {
                throw new InvalidOperationException("Spawn table has no entry with positive weight");
            }
        }

        public AnimalDefinitionSO Select(int draw)
        {
            if (draw < 0 || draw >= TotalWeight)
            {
                throw new ArgumentOutOfRangeException(nameof(draw), draw, $"Draw must be in range from 0 to {TotalWeight}");
            }

            foreach (var entry in _entries)
            {
                if (draw < entry.Weight)
                {
                    return entry.Definition;
                }

                draw -= entry.Weight;
            }

            throw new InvalidOperationException("Unreachable: draw is below the total weight");
        }
    }
}
