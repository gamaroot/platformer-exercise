using UnityEngine;

namespace GamaPlatform
{
    public class CameraHandler
    {
        private readonly Camera m_camera;

        public Vector2 CameraSize => 2f * Camera.main.orthographicSize * new Vector2(Camera.main.aspect, 1f);

        public CameraHandler()
        {
            this.m_camera = Camera.main;
        }

        public Bounds GetCameraBounds()
        {
            var bounds = new Vector3(this.m_camera.pixelWidth, this.m_camera.pixelHeight, this.m_camera.transform.position.z);
            Vector3 worldBasedBounds = this.m_camera.ScreenToWorldPoint(bounds);

            float verticalSize = worldBasedBounds.y;
            float horizontalSize = worldBasedBounds.x;

            return new Bounds(-horizontalSize, horizontalSize, verticalSize, -verticalSize);
        }
    }
}