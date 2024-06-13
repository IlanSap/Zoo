using System;

public struct AnimalPosition
{
    public int Row { get; set; }
    public int Col { get; set; }

    public AnimalPosition(int x, int y)
    {
        Row = x;
        Col = y;
    }

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