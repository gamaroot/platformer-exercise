namespace GamaPlatform
{
    public struct Bounds
    {
        public float Left;
        public float Right;
        public float Top;
        public float Bottom;

        public Bounds(float left, float right, float top, float bottom)
        {
            this.Left = left;
            this.Right = right;
            this.Top = top;
            this.Bottom = bottom;
        }
    }
}