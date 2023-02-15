using System.IO;
using System.Text.Json;
using System.Windows.Input;
using BashModel.Model;
using BashModel.Tools;
using BashView.Commands;

namespace BashView
{
    internal class MainViewModel
    {
        private readonly PolygonReader polygonReader;
        private readonly ShapeProcessor shapeProcessor;

        public MainViewModel(PolygonReader polygonReader, ShapeProcessor shapeProcessor)
        {
            this.polygonReader = polygonReader;
            this.shapeProcessor = shapeProcessor;
        }

        public ICommand One => new ActionCommand(OneAction);
        public ICommand Two => new ActionCommand(TwoAction);

        

        public void OneAction()
        {
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

            File.WriteAllText("D:\\Projects\\C#\\Bash\\BashView\\Shapes\\Shape1.json", JsonSerializer.Serialize(shape, new JsonSerializerOptions()
            {
                WriteIndented = true
            }));
        }
        public void TwoAction()
        {
            var shape = polygonReader.ReadAsJson("Shapes\\Shape1.json");
            var rect = new Rect2()
            {
                A = new Vector2(2, 2),
                B = new Vector2(4, 4),
            };
            var cutShape = shapeProcessor.CutInternal(shape, rect);
        }
    }
}
