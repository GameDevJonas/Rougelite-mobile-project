using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Point
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public Point()
    {
        X = 0;
        Y = 0;
    }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point Add(Point b)
    {
        return new Point(X + b.X, Y + b.Y);
    }

    public Point Substract(Point b)
    {
        return new Point(X - b.X, Y - b.Y);
    }

    public override bool Equals(object obj)
    {
        var point = obj as Point;
        return X == point.X && Y == point.Y;
    }
    public override int GetHashCode()
    {
        unchecked
        {
            return X * 1023 + Y;
        }
    }

    public static Point operator -(Point p)
    {
        return new Point(-p.X, -p.Y);
    }
}