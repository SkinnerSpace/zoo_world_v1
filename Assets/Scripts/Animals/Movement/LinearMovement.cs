using UnityEngine;
using ZooWorld.Utils;

namespace ZooWorld.Animals.Movement
{
    public class LinearMovement : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private WanderSteering _steering;

        [SerializeField]
        private float _speed = 3f;

        [SerializeField]
        private float _rotationSpeed = 180f;

        private void FixedUpdate()
        {
            var direction = _steering.Tick(Time.fixedDeltaTime);

            var horizontalVelocity = transform.forward * _speed;

            _rigidbody.linearVelocity = new Vector3(
                horizontalVelocity.x, _rigidbody.linearVelocity.y, horizontalVelocity.z
            );
            _rigidbody.RotateTowards(direction, _rotationSpeed * Time.fixedDeltaTime);
        }

        private void OnEnable()
        {
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}
