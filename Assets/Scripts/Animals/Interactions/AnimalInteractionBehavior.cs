using System;
using R3;
using UnityEngine;
using ZooWorld.Animals.Core;

namespace ZooWorld.Animals.Interactions
{
    public abstract class AnimalInteractionBehavior : MonoBehaviour
    {
        [SerializeField]
        private InteractionCollider _interactionCollider;

        private IDisposable _subscription;

        public abstract FoodChainRole Role { get; }

        protected abstract void HandleContact(Animal other);

        private void OnEnable()
        {
            _subscription = _interactionCollider.OnTriggered.Subscribe(HandleContact);
        }

        private void OnDisable()
        {
            _subscription?.Dispose();
            _subscription = null;
        }
    }
}
