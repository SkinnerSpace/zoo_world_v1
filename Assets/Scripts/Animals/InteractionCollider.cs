using R3;
using UnityEngine;
using ZooWorld.Animals.Core;

namespace ZooWorld.Animals
{
    public class InteractionCollider : MonoBehaviour
    {
        private readonly Subject<Animal> _onTriggered = new ();

        public Observable<Animal> OnTriggered => _onTriggered;

        private void OnTriggerEnter(Collider other)
        {
            var body = other.attachedRigidbody;

            if (other.isTrigger || body == null || !body.TryGetComponent<Animal>(out var animal))
            {
                return;
            }

            _onTriggered.OnNext(animal);
        }
    }
}
