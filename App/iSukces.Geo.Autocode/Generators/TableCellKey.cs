using System;

namespace iSukces.Geo.Autocode.Generators;

public class TableCellKey : IEquatable<TableCellKey>
{
    public TableCellKey(int row, int col)
    {
        Row = row;
        Col = col;
    }

    public static bool operator ==(TableCellKey left, TableCellKey right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(TableCellKey left, TableCellKey right)
    {
        return !Equals(left, right);
    }

    public bool Equals(TableCellKey? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Row == other.Row && Col == other.Col;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((TableCellKey)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (Row * 397) ^ Col;
        }
    }

    public int Row { get; }
    public int Col { get; }

    public TableCellKey WithColumn(int col)
    {
        return new TableCellKey(Row, col);
    }
}