using UnityEngine;
using ZooWorld.Infrastructure;

namespace ZooWorld.Animals.Movement
{
    public class SteeringLogic
    {
        private readonly float _minIntervalSeconds;
        private readonly float _maxIntervalSeconds;
        private readonly System.Random _random;

        private float _timeUntilDirectionChangeSeconds;

        public Vector2 Direction { get; private set; }

        public SteeringLogic(System.Random random, float minIntervalSeconds, float maxIntervalSeconds)
        {
            _random = random;

            _minIntervalSeconds = minIntervalSeconds;
            _maxIntervalSeconds = maxIntervalSeconds;
        }

        public void Reset()
        {
            _timeUntilDirectionChangeSeconds = 0f;
            Direction = Vector2.zero;
        }

        public void Tick(float deltaTime, Vector2 position, CameraBorders cameraBorders)
        {
            if (position.x > cameraBorders.Right ||
                position.x < cameraBorders.Left ||
                position.y > cameraBorders.Top ||
                position.y < cameraBorders.Bottom)
            {
                var closestPoint = new Vector2(
                    Mathf.Clamp(position.x, cameraBorders.Left, cameraBorders.Right),
                    Mathf.Clamp(position.y, cameraBorders.Bottom, cameraBorders.Top)
                );

                Direction = (closestPoint - position).normalized;
                _timeUntilDirectionChangeSeconds = NextInterval();

                return;
            }

            _timeUntilDirectionChangeSeconds -= deltaTime;

            if (_timeUntilDirectionChangeSeconds > 0f)
            {
                return;
            }

            var angle = (float)_random.NextDouble() * Mathf.PI * 2f;

            Direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            _timeUntilDirectionChangeSeconds = NextInterval();
        }

        private float NextInterval()
        {
            return Mathf.Lerp(_minIntervalSeconds, _maxIntervalSeconds, (float)_random.NextDouble());
        }
    }
}
