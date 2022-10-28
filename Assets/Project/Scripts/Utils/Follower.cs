using Unity.VisualScripting;
using UnityEngine;

namespace GamaPlatform
{
    public class Follower : MonoBehaviour
    {
        [Header("Properties")]
        [Range(1f, 20f)]
        [SerializeField] private float m_speedWhenMovingUp = 1f;
        [Range(1f, 20f)]
        [SerializeField] private float m_speedWhenMovingDown = 15f;

        [Header("Components")]
        [SerializeField] private Transform m_target;

        private float m_offset;

        private void Start()
        {
            this.m_offset = base.transform.position.y - this.m_target.localPosition.y;
        }

        private void Update()
        {
            Vector3 cameraPosition = base.transform.localPosition;
            Vector3 targetPosition = cameraPosition;
            targetPosition.y = this.m_target.localPosition.y + this.m_offset;

            bool isGoingUp = targetPosition.y - cameraPosition.y > 0;
            float speed = isGoingUp ? this.m_speedWhenMovingUp : this.m_speedWhenMovingDown;

            base.transform.localPosition = Vector3.Lerp(cameraPosition, targetPosition, speed * Time.deltaTime);
        }
    }
}