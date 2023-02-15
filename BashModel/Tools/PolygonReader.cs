using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BashModel.Model;

namespace BashModel.Tools
{
    public class PolygonReader
    {
        public Shape ReadAsJson(string path)
        {
            var dataStr = File.ReadAllText(path);
            var shape = JsonSerializer.Deserialize<Shape>(dataStr);

            return shape;
        }
    }
}
