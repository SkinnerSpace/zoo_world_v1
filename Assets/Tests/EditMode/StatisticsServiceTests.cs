using NUnit.Framework;
using ZooWorld.Animals.Core;
using ZooWorld.Application;

namespace ZooWorld.Tests
{
    public class StatisticsServiceTests
    {
        [Test]
        public void RecordAnimalDied_CountsByRole()
        {
            var statistics = new StatisticsService();

            statistics.RecordAnimalDied(FoodChainRole.Prey);
            statistics.RecordAnimalDied(FoodChainRole.Prey);
            statistics.RecordAnimalDied(FoodChainRole.Predator);

            Assert.AreEqual(2, statistics.DeadPreyCount.CurrentValue);
            Assert.AreEqual(1, statistics.DeadPredatorsCount.CurrentValue);
        }
    }
}
