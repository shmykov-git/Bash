using BashModel.Model;

namespace BashModel.Extensions
{
    public static class Rect2Extensions
    {
        public static bool IsInside(this Rect2 rect, Vector2 a)
        {
            var min = rect.Min;
            var max = rect.Max;

            return min.X <= a.X && a.X <= max.X &&
                   min.Y <= a.Y && a.Y <= max.Y;
        }

        public static Rect2 Mult(this Rect2 rect, double mult)
        {
            return new Rect2()
            {
                A = new Vector2(rect.A.X * mult, rect.A.Y * mult),
                B = new Vector2(rect.B.X * mult, rect.B.Y * mult)
            };
        }
    }
}
