using DG.Tweening;
using UnityEngine;

namespace GamaPlatform
{
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D), typeof(Rigidbody2D))]
    public class Spawnable : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] private Color m_contactColor;

        [Header("Components")]
        [SerializeField] protected SpriteRenderer m_spriteRenderer;
        [SerializeField] protected Rigidbody2D m_rigidbody;
        [SerializeField] protected BoxCollider2D m_boxCollider;

        private const float COLOR_TRANSITION_DURATION = 0.5f;

        private Color m_originalColor;

        private void OnValidate()
        {
            if (this.m_rigidbody == null)
                this.m_rigidbody = base.GetComponent<Rigidbody2D>();
        }

        private void Awake()
        {
            this.m_originalColor = this.m_spriteRenderer.color;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<SpawnLimitBorder>() != null)
                base.gameObject.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            this.m_spriteRenderer.DOKill();
            this.m_spriteRenderer.DOColor(this.m_contactColor, COLOR_TRANSITION_DURATION);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            this.m_spriteRenderer.DOKill();
            this.m_spriteRenderer.DOColor(this.m_originalColor, COLOR_TRANSITION_DURATION);
        }
    }
}