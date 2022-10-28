using UnityEngine;

namespace GamaPlatform {
    public class GameManager : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Stage m_stage;
        [SerializeField] private Player m_player;
        [SerializeField] private Spawner m_spawner;
        [SerializeField] private HeightDisplayController m_heightDisplay;

        private void Awake()
        {
            Application.targetFrameRate = 60;

            var cameraHandler = new CameraHandler();
            Bounds stageBounds = cameraHandler.GetCameraBounds();
            this.m_stage.CreateBorders(stageBounds, cameraHandler.CameraSize);
            this.m_player.SetInitialPosition(stageBounds);
            this.m_heightDisplay.SetPlayerLocation(this.m_player);
        }
    }
}