using System.Text.Json;
using BashModel.Model;

namespace BashModel.Tools
{
    public class ShapeReader
    {
        public Shape ReadAsJson(string path)
        {
            var dataStr = File.ReadAllText(path);
            var shape = JsonSerializer.Deserialize<Shape>(dataStr);

            return shape;
        }
    }
}
