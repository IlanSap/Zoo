using System;

public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public (int, int) ToTuple() => (X, Y);

    public override string ToString()
    {
        return $"({X}, {Y})";
    }

    public void Deconstruct(out int row, out int col)
    {
        row = X;
        col = Y;
    }

    public static implicit operator Point((int row, int col) tuple)
    {
        return new Point(tuple.row, tuple.col);
    }
}