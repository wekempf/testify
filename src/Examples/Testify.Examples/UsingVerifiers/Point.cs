using System;
using System.Collections.Generic;

namespace Examples.UsingVerifiers
{
    public class Point : IEquatable<Point>, IComparable<Point>, IComparable
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }

        public int Y { get; }

        public static bool operator !=(Point left, Point right) => !EqualityComparer<Point>.Default.Equals(left, right);

        public static bool operator <(Point left, Point right) => Comparer<Point>.Default.Compare(left, right) < 0;

        public static bool operator <=(Point left, Point right) => Comparer<Point>.Default.Compare(left, right) <= 0;

        public static bool operator ==(Point left, Point right) => EqualityComparer<Point>.Default.Equals(left, right);

        public static bool operator >(Point left, Point right) => Comparer<Point>.Default.Compare(left, right) > 0;

        public static bool operator >=(Point left, Point right) => Comparer<Point>.Default.Compare(left, right) >= 0;

        public int CompareTo(Point other) => other == null ? 1 : (X, Y).CompareTo((other.X, other.Y));

        public int CompareTo(object obj) => CompareTo(obj as Point);

        public bool Equals(Point other) => other == null ? false : (X, Y).Equals((other.X, other.Y));

        public override bool Equals(object obj) => Equals(obj as Point);

        public override int GetHashCode() => (X, Y).GetHashCode();
    }
}