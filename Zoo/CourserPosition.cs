using System;

public struct CourserPosition
{
    public int row { get; set; }
    public int col { get; set; }

    // Operator overloading for the == operator
    public static bool operator ==(CourserPosition a, CourserPosition b)
    {
        return a.row == b.row && a.col == b.col;
    }

    // Operator overloading for the != operator
    public static bool operator !=(CourserPosition a, CourserPosition b)
    {
        return a.row != b.row || a.col != b.col;
    }
}
