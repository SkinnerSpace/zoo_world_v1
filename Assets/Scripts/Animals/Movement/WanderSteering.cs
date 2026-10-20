using UnityEngine;
using Zenject;
using ZooWorld.Infrastructure;
using ZooWorld.Utils;

namespace ZooWorld.Animals.Movement
{
    public class WanderSteering : MonoBehaviour
    {
        [SerializeField]
        private float _minIntervalSeconds = 1f;

        [SerializeField]
        private float _maxIntervalSeconds = 3f;

        private CameraService _cameraService;
        private SteeringLogic _steeringLogic;

        [Inject]
        private void Construct(CameraService cameraService)
        {
            _cameraService = cameraService;

            var random = new System.Random(Random.Range(0, int.MaxValue));

            _steeringLogic = new SteeringLogic(random, _minIntervalSeconds, _maxIntervalSeconds);
        }

        public Vector2 Tick(float deltaTime)
        {
            var position = transform.position;

            _steeringLogic.Tick(deltaTime, position.ToXZ(), _cameraService.GetBordersAtHeight(position.y));

            return _steeringLogic.Direction;
        }

        private void OnEnable()
        {
            _steeringLogic.Reset();
        }
    }
}
