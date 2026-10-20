using UnityEngine;
using UnityEngine.Localization.Components;
using Zenject;
using ZooWorld.Animals;

namespace ZooWorld.UI
{
    public class SpeechBubble : MonoBehaviour, IPoolable<Transform, SpeechCues>
    {
        // ReSharper disable once ClassNeverInstantiated.Global
        public class Pool : MonoPoolableMemoryPool<Transform, SpeechCues, SpeechBubble>
        {
        }

        [SerializeField]
        private RectTransform _rectTransform;

        [SerializeField]
        private LocalizeStringEvent _localizeStringEvent;

        [SerializeField]
        private Vector2 _canvasOffset;

        private SpeechBubblesLayer _layer;
        private Transform _anchorPoint;

        [Inject]
        private void Construct(SpeechBubblesLayer layer)
        {
            _layer = layer;
        }

        private void LateUpdate()
        {
            if (_anchorPoint == null)
            {
                return;
            }

            if (_layer.TryGetCanvasPosition(_anchorPoint.position, _canvasOffset, out var canvasPosition))
            {
                _rectTransform.position = canvasPosition;
            }
        }

        void IPoolable<Transform, SpeechCues>.OnSpawned(Transform anchorPoint, SpeechCues speechCue)
        {
            _anchorPoint = anchorPoint;

            _localizeStringEvent.StringReference.TableEntryReference = speechCue.ToString();
            _localizeStringEvent.RefreshString();
        }

        void IPoolable<Transform, SpeechCues>.OnDespawned()
        {
            _anchorPoint = null;
        }
    }
}
