using System;

public struct AnimalPosition(int x, int y)
{
    public int Row { get; set; } = x;
    public int Col { get; set; } = y;

    public (int, int) ToTuple() => (Row, Col);

    public override string ToString()
    {
        return $"({Row}, {Col})";
    }

    public void Deconstruct(out int row, out int col)
    {
        row = Row;
        col = Col;
    }

    public static implicit operator AnimalPosition((int row, int col) tuple)
    {
        return new AnimalPosition(tuple.row, tuple.col);
    }
}