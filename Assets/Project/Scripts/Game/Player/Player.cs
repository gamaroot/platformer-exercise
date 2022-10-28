using UnityEngine;

namespace GamaPlatform
{
    public class Player : PlayerController, IPlayerLocation
    {
        private float m_initialPositionY;

        public void SetInitialPosition(Bounds bounds)
        {
            this.m_initialPositionY = bounds.Bottom + (base.transform.localScale.y / 2f);
            base.transform.localPosition = new Vector2(0, this.m_initialPositionY);
        }

        public int GetDistance()
        {
            return (int)(base.transform.localPosition.y - this.m_initialPositionY);
        }
    }
}