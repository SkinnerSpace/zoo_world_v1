using System;
using R3;
using UnityEngine;
using Zenject;
using ZooWorld.Animals;
using ZooWorld.UI;

namespace ZooWorld.Application
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class SpeechBubbleService
    {
        private SpeechBubble.Pool _pool;

        [Inject]
        private void Construct(SpeechBubble.Pool pool)
        {
            _pool = pool;
        }

        public IDisposable Show(Transform anchorPoint, SpeechCues speechCue, float durationSeconds)
        {
            var bubble = _pool.Spawn(anchorPoint, speechCue);
            var isReleased = false;
            IDisposable expiration = null;

            expiration = Observable.Timer(TimeSpan.FromSeconds(durationSeconds)).Subscribe(_ => Release());

            return Disposable.Create(Release);

            void Release()
            {
                if (isReleased)
                {
                    return;
                }

                isReleased = true;

                expiration?.Dispose();

                if (bubble != null)
                {
                    _pool.Despawn(bubble);
                }
            }
        }
    }
}
