namespace BashModel.Model;

public struct Rect2
{
    public Vector2 A { get; set; }
    public Vector2 B { get; set; }

    public Vector2 Min => new Vector2(Math.Min(A.X, B.X), Math.Min(A.Y, B.Y));
    public Vector2 Max => new Vector2(Math.Max(A.X, B.X), Math.Max(A.Y, B.Y));
    public Vector2 Size => new Vector2(Math.Abs(A.X - B.X), Math.Abs(A.Y - B.Y));
}