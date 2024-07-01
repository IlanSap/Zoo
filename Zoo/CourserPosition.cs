using System;

namespace ZooProject;

public struct CourserPosition
{
    public int Row { get; set; }
    public int Col { get; set; }

    // Operator overloading for the == operator
    public static bool operator ==(CourserPosition a, CourserPosition b)
    {
        return a.Row == b.Row && a.Col == b.Col;
    }

    // Operator overloading for the != operator
    public static bool operator !=(CourserPosition a, CourserPosition b)
    {
        return a.Row != b.Row || a.Col != b.Col;
    }
}
