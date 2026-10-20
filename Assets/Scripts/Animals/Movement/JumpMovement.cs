using UnityEngine;
using ZooWorld.Utils;

namespace ZooWorld.Animals.Movement
{
    public class JumpMovement : MonoBehaviour
    {
        private const float GroundCheckSkin = 0.1f;

        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private WanderSteering _steering;

        [SerializeField]
        private float _jumpIntervalSeconds = 2.5f;

        [SerializeField]
        private float _jumpDistance = 4f;

        [SerializeField]
        private float _jumpHeight = 5f;

        [SerializeField]
        private float _rotationSpeed = 360f;

        [SerializeField]
        private Collider _bodyCollider;

        private float _timeUntilJumpSeconds;

        private void FixedUpdate()
        {
            var direction = _steering.Tick(Time.fixedDeltaTime);

            _timeUntilJumpSeconds -= Time.fixedDeltaTime;

            if (_timeUntilJumpSeconds <= 0f && IsGrounded())
            {
                _timeUntilJumpSeconds = _jumpIntervalSeconds;

                Jump(direction);
            }

            _rigidbody.RotateTowards(direction, _rotationSpeed * Time.fixedDeltaTime);
        }

        private bool IsGrounded()
        {
            var bounds = _bodyCollider.bounds;

            return Physics.Raycast(
                bounds.center, Vector3.down, bounds.extents.y + GroundCheckSkin,
                Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore
            );
        }

        private void Jump(Vector2 direction)
        {
            var targetVelocity = JumpLogic.CalculateLaunchVelocity(
                direction, _jumpDistance, _jumpHeight, Mathf.Abs(Physics.gravity.y)
            );

            _rigidbody.AddForce(
                targetVelocity - _rigidbody.linearVelocity,
                ForceMode.VelocityChange
            );
        }

        private void OnEnable()
        {
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            _timeUntilJumpSeconds = 0f;
        }
    }
}
