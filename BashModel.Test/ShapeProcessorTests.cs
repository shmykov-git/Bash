using BashModel.Model;
using BashModel.Tools;
using FluentAssertions;

namespace BashModel.Test;

public class ShapeProcessorTests
{
    [Test]
    public void NewShapeContainsOnlyInsideLinesWhenCut()
    {
        var processor = new ShapeProcessor();

        var shape = new Shape()
        {
            Polygons = new Polygon[]
            {
                new Polygon()
                {
                    Points = new Vector2[]
                    {
                        new(0, 0), new(1, 1), new(2, 2), new(3, 3), new(4, 4), new (5, 5), 
                        new(5, 6), new(4, 5), new(3, 4), new(2, 3), new(1, 2), new(0, 1)
                    }
                },
                new Polygon()
                {
                    Points = new Vector2[]
                    {
                        new(0, 2), new(1, 3), new(2, 4), new(3, 5), new(4, 6), new (5, 7)
                    }
                }
            }
        };

        var rect = new Rect2()
        {
            A = new Vector2(2, 2),
            B = new Vector2(4, 4),
        };

        var cutShape = processor.CutInternal(shape, rect);

        var expectedShape = new Shape()
        {
            Polygons = new Polygon[]
            {
                new Polygon()
                {
                    Points = new Vector2[]
                    {
                        new(1, 1), new(2, 2), new(3, 3), new(4, 4), new (5, 5),
                    }
                },
                new Polygon()
                {
                    Points = new Vector2[]
                    {
                        new(4, 5), new(3, 4), new(2, 3), new(1, 2), 
                    }
                },
                new Polygon()
                {
                    Points = new Vector2[]
                    {
                        new(1, 3), new(2, 4), new(3, 5), 
                    }
                }
            }
        };

        cutShape.Polygons.Length.Should().Be(expectedShape.Polygons.Length);

        for (var i = 0; i < expectedShape.Polygons.Length; i++)
        {
            cutShape.Polygons[i].Points.Length.Should().Be(expectedShape.Polygons[i].Points.Length);

            for (var j = 0; j < expectedShape.Polygons[i].Points.Length; j++)
                cutShape.Polygons[i].Points[j].Should().Be(expectedShape.Polygons[i].Points[j]);
        }
    }
}