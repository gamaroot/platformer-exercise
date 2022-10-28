using UnityEngine;

namespace GamaPlatform
{
    [RequireComponent(typeof(PlatformEffector2D))]
    public class Platform : Spawnable
    {
        [Header("Properties")]
        [SerializeField] private Vector2 m_widthRange;
        [SerializeField] private Vector2 m_heightRange;
        [SerializeField] private Vector2 m_speedRange;

        [Header("Components")]
        [SerializeField] private PlatformEffector2D m_platformEffector;

        private float m_speed;
        private Vector2 m_direction;
        private Alignment m_alignment;

        private float m_effectorDelay;

        private void OnValidate()
        {
            if (this.m_platformEffector == null)
                this.m_platformEffector = base.GetComponent<PlatformEffector2D>();
        }

        private void Start()
        {
            this.DefineName();
            this.ApplyRandomSize();
            this.ApplyRandomSpeed();
        }

        public void Setup(Bounds bounds)
        {
            this.ApplyRandomDirection();
            this.SetRandomPositionY(bounds);
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.DownArrow))
                this.m_effectorDelay = 0.5f;

            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (this.m_effectorDelay <= 0)
                {
                    this.m_platformEffector.rotationalOffset = 180f;
                    this.m_effectorDelay = 0.5f;
                }
                else
                {
                    this.m_effectorDelay -= Time.deltaTime;
                }
            }

            if (Input.GetKey(KeyCode.UpArrow))
                this.m_platformEffector.rotationalOffset = 0;
        }

        private void FixedUpdate()
        {
            base.m_rigidbody.velocity = this.m_direction * this.m_speed;
        }

        private void DefineName()
        {
            base.gameObject.name = $"Platform {base.transform.localPosition.y}";
        }

        private void ApplyRandomSize()
        {
            float randomWidth = Random.Range(this.m_widthRange.x, this.m_widthRange.y);
            float randomHeight = Random.Range(this.m_heightRange.x, this.m_heightRange.y);
            base.transform.localScale = new Vector2(randomWidth, randomHeight);
        }

        private void ApplyRandomSpeed()
        {
            this.m_speed = Random.Range(this.m_speedRange.x, this.m_speedRange.y);
        }

        private void ApplyRandomDirection()
        {
            if (Random.value > 0.5f)
            {
                this.m_alignment = Alignment.RIGHT;
                this.m_direction = Vector2.left;
            }
            else
            {
                this.m_alignment = Alignment.LEFT;
                this.m_direction = Vector2.right;
            }
        }
        private void SetRandomPositionY(Bounds bounds)
        {
            float positionX = 0;
            float positionY = Random.Range(bounds.Bottom, bounds.Top);

            switch (this.m_alignment)
            {
                case Alignment.LEFT:
                    positionX = bounds.Left - this.m_boxCollider.size.x;
                    break;
                case Alignment.RIGHT:
                    positionX = bounds.Right + this.m_boxCollider.size.x;
                    break;
            }
            base.transform.localPosition = new Vector2(positionX, positionY);
        }
    }
}