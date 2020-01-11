namespace TheMatiaz0_MonobeamTheMercenary.Movement
{
    public struct Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return this == (Point)obj;
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Point a, Point b)
        {
            if (a == null)
            {
                if (b == null)
                {
                    return true;
                }

                else
                {
                    return false;
                }

            }

            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        public static Point operator *(Point a, float b)
        {
            return new Point((int)(a.X * b), (int)(a.Y * b));
        }

        public static Point operator /(Point a, float b)
        {
            return new Point((int)(a.X / b), (int)(a.Y / b));
        }
    }
}
