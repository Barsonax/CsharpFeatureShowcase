﻿using System;

namespace MorePatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayCsharp7(null);
            DisplayCsharp8(null);
        }

        static string DisplayCsharp7(object o)
        {
            switch (o)
            {
                case Point p when p.X == 0 && p.Y == 0:
                    return "origin";
                case Point p:
                    return $"({p.X}, {p.Y})";
                default:
                    return "unknown";
            }
        }

        static string DisplayCsharp8(object o) => o switch
        {
            Point (0, 0) => "origin",
            Point (var x, var y) => $"({x}, {y})",
            _ => "unknown"
        };
    }

    class Point
    {
        public int X { get; }
        public int Y { get; }
        public Point(int x, int y) => (X, Y) = (x, y);
        public void Deconstruct(out int x, out int y) => (x, y) = (X, Y);
    }
}
