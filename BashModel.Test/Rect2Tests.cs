using BashModel.Extensions;
using BashModel.Model;
using FluentAssertions;

namespace BashModel.Test
{
    public class Rect2Tests
    {
        [Test]
        public void IsTrueIfPointIsInsideRect()
        {
            var rect = new Rect2
            {
                A = new Vector2(3, 3),
                B = new Vector2(1, 1)
            };

            var a = new Vector2(2, 2);
            var res = rect.IsInside(a);

            res.Should().BeTrue();
        }
    }
}