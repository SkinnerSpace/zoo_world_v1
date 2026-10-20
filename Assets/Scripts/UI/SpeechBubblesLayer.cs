using UnityEngine;
using Zenject;
using ZooWorld.Infrastructure;

namespace ZooWorld.UI
{
    public class SpeechBubblesLayer : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _rectTransform;

        [SerializeField]
        private Canvas _canvas;

        private CameraService _cameraService;

        public RectTransform Root => _rectTransform;

        [Inject]
        private void Construct(CameraService cameraService)
        {
            _cameraService = cameraService;
        }

        public bool TryGetCanvasPosition(Vector3 worldPoint, Vector2 canvasOffset, out Vector3 canvasPosition)
        {
            if (!_cameraService.TryProjectToCanvas(worldPoint, _canvas, _rectTransform, out canvasPosition))
            {
                return false;
            }

            canvasPosition += _rectTransform.TransformVector(canvasOffset);

            return true;
        }
    }
}
