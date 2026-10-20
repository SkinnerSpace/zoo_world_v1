namespace ZooWorld.Infrastructure
{
    public readonly struct CameraBorders
    {
        public readonly float Left;
        public readonly float Right;
        public readonly float Bottom;
        public readonly float Top;

        public CameraBorders(float left, float right, float bottom, float top)
        {
            Left = left;
            Right = right;
            Bottom = bottom;
            Top = top;
        }
    }
}
