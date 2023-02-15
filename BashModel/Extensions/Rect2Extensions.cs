using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
