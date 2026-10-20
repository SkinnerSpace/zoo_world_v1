using R3;
using ZooWorld.Animals.Core;

// ReSharper disable once ClassNeverInstantiated.Global
namespace ZooWorld.Application
{
    public class StatisticsService
    {
        private readonly ReactiveProperty<int> _deadPreyCount = new (0);
        private readonly ReactiveProperty<int> _deadPredatorsCount = new (0);

        public ReadOnlyReactiveProperty<int> DeadPreyCount => _deadPreyCount;
        public ReadOnlyReactiveProperty<int> DeadPredatorsCount => _deadPredatorsCount;

        public void RecordAnimalDied(FoodChainRole role)
        {
            switch (role)
            {
                case FoodChainRole.Prey:
                    _deadPreyCount.Value++;
                    break;
                case FoodChainRole.Predator:
                    _deadPredatorsCount.Value++;
                    break;
            }
        }
    }
}
