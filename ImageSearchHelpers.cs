using Point = OpenCvSharp.Point;

namespace ImageSearchSharp
{
    internal static class ImageSearchHelpers
    {

        public static bool ContainsCloseEnoughPoint(List<Point> list, Point point, int xDistance, int yDistance)
        {
            foreach (Point cPoint in list)
            {
                if (Math.Abs(cPoint.X - point.X) < xDistance && Math.Abs(cPoint.Y - point.Y) < yDistance) { return true; }
            }

            return false;
        }
    }
}