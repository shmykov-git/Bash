using System.Diagnostics.CodeAnalysis;
using BashModel.Model;

namespace BashModel.Extensions;

public static class ShapeExtensions
{
    public static (Vector2 a, Vector2 b)[] ToSections(this Shape shape)
    {
        return shape.Polygons.SelectMany(p => p.Points.SelectPair((a, b) => (a, b))).ToArray();
    }

    public static Shape Mult(this Shape shape, double mult)
    {
        return new Shape()
        {
            Polygons = shape.Polygons.Select(pl => new Polygon()
            {
                Points = pl.Points.Select(p => new Vector2(p.X * mult, p.Y * mult)).ToArray()
            }).ToArray()
        };
    }
}