using System;
using UnityEngine;

namespace ZooWorld.Infrastructure
{
    public class CameraService : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;

        private Ray _bottomLeftRay;
        private Ray _bottomRightRay;
        private Ray _topLeftRay;
        private Ray _topRightRay;

        private void Awake()
        {
            RefreshCornerRays();
        }

        private void Update()
        {
            RefreshCornerRays();
        }

        public CameraBorders GetBordersAtHeight(float height)
        {
            var plane = new Plane(Vector3.up, new Vector3(0f, height, 0f));

            var bounds = new Bounds(GetPlanePoint(_bottomLeftRay), Vector3.zero);
            bounds.Encapsulate(GetPlanePoint(_bottomRightRay));
            bounds.Encapsulate(GetPlanePoint(_topLeftRay));
            bounds.Encapsulate(GetPlanePoint(_topRightRay));

            return new CameraBorders(bounds.min.x, bounds.max.x, bounds.min.z, bounds.max.z);

            Vector3 GetPlanePoint(Ray cornerRay)
            {
                if (!plane.Raycast(cornerRay, out var distance))
                {
                    throw new InvalidOperationException(
                        $"Corner ray of camera {_camera.name} does not hit the plane at height {height}, borders are unbounded."
                    );
                }

                return cornerRay.GetPoint(distance);
            }
        }

        private void RefreshCornerRays()
        {
            _bottomLeftRay = _camera.ViewportPointToRay(new Vector2(0, 0));
            _bottomRightRay = _camera.ViewportPointToRay(new Vector2(1, 0));
            _topLeftRay = _camera.ViewportPointToRay(new Vector2(0, 1));
            _topRightRay = _camera.ViewportPointToRay(new Vector2(1, 1));
        }

        public bool TryProjectToCanvas(Vector3 worldPoint, Canvas canvas, RectTransform plane, out Vector3 canvasPosition)
        {
            var screenPoint = _camera.WorldToScreenPoint(worldPoint);
            var canvasCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

            return RectTransformUtility.ScreenPointToWorldPointInRectangle(
                plane, screenPoint, canvasCamera, out canvasPosition
            );
        }
    }
}
