using TMPro;
using UnityEngine;

namespace GamaPlatform
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class HeightDisplayController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_text;

        private IPlayerLocation m_playerLocation;

        private void OnValidate()
        {
            if (this.m_text == null)
                this.m_text = GetComponent<TextMeshProUGUI>();
        }

        public void SetPlayerLocation(IPlayerLocation playerLocation)
        {
            this.m_playerLocation = playerLocation;
        }

        private void Update()
        {
            this.m_text.text = this.m_playerLocation.GetDistance().ToMeters();
        }
    }
}