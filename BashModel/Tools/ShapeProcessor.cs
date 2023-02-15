using BashModel.Extensions;
using BashModel.Model;

namespace BashModel.Tools;

public class ShapeProcessor
{
    // TODO: нужно доработать алгоритм для случая, когда обе точки отрезка лежат вне прямоугольника,
    // TODO: но при этом отрезок пересекает прямоугольник
    public Shape CutInternal(Shape shape, Rect2 rect)
    {
        var polygons = new List<Polygon>();

        void AddPolygonIfNotEmpty(List<Vector2> points)
        {
            if (points.Count == 0)
                return;

            polygons.Add(new Polygon()
            {
                Points = points.ToArray()
            });
        }

        // TODO: check polygon contains 2 or more points
        foreach (var polygon in shape.Polygons)
        {
            var cutPoints = new List<Vector2>();
            Vector2 prevPoint = default;
            var hasPrevPoint = false;

            foreach (var point in polygon.Points)
            {
                var isInside = rect.IsInside(point);

                if (isInside)
                {
                    if (cutPoints.Count == 0 && hasPrevPoint)
                        cutPoints.Add(prevPoint);

                    cutPoints.Add(point);
                }
                else
                {
                    if (cutPoints.Count > 0)
                        cutPoints.Add(point);

                    AddPolygonIfNotEmpty(cutPoints);
                    cutPoints = new List<Vector2>();
                }

                prevPoint = point;
                hasPrevPoint = true;
            }

            AddPolygonIfNotEmpty(cutPoints);
        }

        return new Shape()
        {
            Polygons = polygons.ToArray()
        };
    }
}