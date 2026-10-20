using UnityEngine;
using ZooWorld.Animals.Core;
using ZooWorld.Utils;

namespace ZooWorld.Animals.Interactions
{
    public class PredatorInteractionBehavior : AnimalInteractionBehavior
    {
        [SerializeField]
        private Animal _animal;

        public override FoodChainRole Role => FoodChainRole.Predator;

        protected override void HandleContact(Animal other)
        {
            switch (other.Role)
            {
                case FoodChainRole.Predator:
                    var self = transform;
                    var opponent = other.transform;

                    var isSelfWinner = AttackLogic.DoesFirstWin(
                        self.position.ToXZ(), self.forward.ToXZ(),
                        opponent.position.ToXZ(), opponent.forward.ToXZ());

                    var winner = isSelfWinner ? _animal : other;
                    var loser = isSelfWinner ? other : _animal;

                    winner.TryEat(loser);
                    break;
                case FoodChainRole.Prey:
                    _animal.TryEat(other);
                    break;
            }
        }
    }
}
