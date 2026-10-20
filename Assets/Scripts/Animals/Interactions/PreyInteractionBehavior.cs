using UnityEngine;
using ZooWorld.Animals.Core;

namespace ZooWorld.Animals.Interactions
{
    public class PreyInteractionBehavior : AnimalInteractionBehavior
    {
        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private float _collisionPushBackPower = 5f;

        public override FoodChainRole Role => FoodChainRole.Prey;

        protected override void HandleContact(Animal other)
        {
            if (other.Role != FoodChainRole.Prey)
            {
                return;
            }

            var direction = (transform.position - other.transform.position).normalized;

            _rigidbody.AddForce(direction * _collisionPushBackPower, ForceMode.Impulse);
        }
    }
}
