namespace TheMatiaz0_MonobeamTheMercenary.Movement
{
    /// <summary>
    /// Contains methods for changing position of Point structure.
    /// </summary>
    public static class PointExtension
    {
        public static void SetX(this ref Point point, int to)
        {
            point = new Point(to, point.Y);
        }

        public static void SetY(this ref Point point, int to)
        {
            point = new Point(point.X, to);
        }

        public static void AddToX(this ref Point point, int to, int add)
        {
            point = new Point(to + add, point.Y);
        }

        public static void RemoveFromX(this ref Point point, int to, int remove)
        {
            point = new Point(to - remove, point.Y);
        }

        public static void AddToY(this ref Point point, int to, int add)
        {
            point = new Point(point.X, to + add);
        }

        public static void RemoveFromY(this ref Point point, int to, int remove)
        {
            point = new Point(point.X, to - remove);
        }
    }
}
