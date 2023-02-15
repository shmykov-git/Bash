using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using BashModel.Extensions;
using BashModel.Model;
using BashModel.Tools;
using BashView.Commands;

namespace BashView
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private readonly ShapeReader shapeReader;
        private readonly ShapeProcessor shapeProcessor;
        private int sliderValue;

        public List<Item> Items { get; set; }
        public ICommand One => new ActionCommand(OneAction);
        public ICommand Two => new ActionCommand(TwoAction);

        public int SliderValue
        {
            get => sliderValue;
            set
            {
                sliderValue = value;
                Task.Run(RefreshItems);
            }
        }

        public MainViewModel(ShapeReader shapeReader, ShapeProcessor shapeProcessor)
        {
            this.shapeReader = shapeReader;
            this.shapeProcessor = shapeProcessor;

            Task.Run(RefreshItems);
        }

        private Item GetItem(Vector2 a, Vector2 b, Brush brush, int width) => new Item()
        {
            X1 = (int) a.X,
            Y1 = (int) a.Y,
            X2 = (int) b.X,
            Y2 = (int) b.Y,
            Brush = brush,
            Width = width
        };

        private void RefreshItems()
        {
            var shape = shapeReader.ReadAsJson("Shapes\\Shape1.json");

            var offset = (SliderValue + 1) * 0.21;

            var rect = new Rect2()
            {
                A = new Vector2(3 - offset, 3 - offset),
                B = new Vector2(3 + offset, 3 + offset),
            };

            var cutShape = shapeProcessor.CutInternal(shape, rect);

            var mult = 50;

            var items = new List<Item>();

            AddShape(items, shape.Mult(mult), Brushes.Green, 2);
            AddShape(items, cutShape.Mult(mult), Brushes.Red, 4);
            AddRect(items, rect.Mult(mult), Brushes.Blue, 4);

            Items = items;
            OnPropertyChanged(nameof(Items));
        }

        private void AddRect(List<Item> items, Rect2 rect, Brush brush, int width)
        {
            var a = rect.Min;
            var b = rect.Max;

            items.Add(GetItem(a, new Vector2(a.X, b.Y), brush, width));
            items.Add(GetItem(new Vector2(a.X, b.Y), b, brush, width));
            items.Add(GetItem(b, new Vector2(b.X, a.Y), brush, width));
            items.Add(GetItem(new Vector2(b.X, a.Y), a, brush, width));
        }

        private void AddShape(List<Item> items, Shape shape, Brush brush, int width)
        {
            var sections = shape.ToSections();

            items.AddRange(sections.Select(s=> GetItem(s.a, s.b, brush, width)));
        }

        private void OneAction()
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

            Debug.WriteLine(JsonSerializer.Serialize(shape, new JsonSerializerOptions()
            {
                WriteIndented = true
            }));
        }

        private void TwoAction()
        {
            var shape = shapeReader.ReadAsJson("Shapes\\Shape1.json");
            var rect = new Rect2()
            {
                A = new Vector2(2, 2),
                B = new Vector2(4, 4),
            };
            var cutShape = shapeProcessor.CutInternal(shape, rect);

            Debug.WriteLine(JsonSerializer.Serialize(cutShape, new JsonSerializerOptions()
            {
                WriteIndented = true
            }));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
