using UnityEngine;
using ZooWorld.Animals.Core;

namespace ZooWorld.Animals
{
    [CreateAssetMenu(fileName = nameof(AnimalDefinitionSO), menuName = "Configuration/" + nameof(AnimalDefinitionSO))]
    public class AnimalDefinitionSO : ScriptableObject
    {
        [SerializeField]
        private Animal _animalPrefab;

        [SerializeField]
        private int _initialPoolSize;

        public Animal AnimalPrefab => _animalPrefab;
        public int InitialPoolSize => _initialPoolSize;
    }
}
