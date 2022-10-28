using UnityEngine;

namespace GamaPlatform
{
    [RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] private string m_axis = "Horizontal";
        [Range(400f, 600f)]
        [SerializeField] private float m_movementSpeed = 400f;
        [Range(0, 0.5f)]
        [SerializeField] private float m_movementSmoothing = 0.05f;
        [Range(20f, 50f)]
        [SerializeField] private float m_jumpForce = 25f;

        [SerializeField] private LayerMask m_groundLayer;

        [Header("Components")]
        [SerializeField] private Rigidbody2D m_rigidbody;

        private Vector3 m_currentVelocity = Vector3.zero;

        private float m_horizontalAxis;
        private bool m_hasPressedJump;
        private bool m_isGrounded;

        private void OnValidate()
        {
            if (this.m_rigidbody == null)
                this.m_rigidbody = base.GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            this.m_horizontalAxis = Input.GetAxisRaw(this.m_axis) * this.m_movementSpeed;

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                this.m_hasPressedJump = true;
            }
        }

        private void FixedUpdate()
        {
            Vector3 targetVelocity = new Vector2(this.m_horizontalAxis * Time.deltaTime, this.m_rigidbody.velocity.y);

            this.m_rigidbody.velocity = Vector3.SmoothDamp(this.m_rigidbody.velocity, targetVelocity, ref m_currentVelocity, this.m_movementSmoothing);

            if (this.m_isGrounded && this.m_hasPressedJump)
                this.m_rigidbody.AddForce(new Vector2(0, this.m_jumpForce), ForceMode2D.Impulse);

            this.m_hasPressedJump = false;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            this.m_isGrounded = true;
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            this.m_isGrounded = false;
        }

        private Vector2 GetFeetPosition()
        {
            Vector2 position = base.transform.position;
            position.y -= base.transform.localScale.y;

            return position;
        }
    }
}