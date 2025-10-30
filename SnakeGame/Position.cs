using System;

namespace SnakeGame;

public readonly struct Position : IEquatable<Position>
{
    public int X { get; }
    public int Y { get; }

    public Position(int x, int y) { X = x; Y = y; }

    public Position Translate(Direction d) => d switch
    {
        Direction.Up    => new Position(X, Y - 1),
        Direction.Down  => new Position(X, Y + 1),
        Direction.Left  => new Position(X - 1, Y),
        Direction.Right => new Position(X + 1, Y),
        _ => this
    };

    public bool Equals(Position other) => X == other.X && Y == other.Y;
    public override bool Equals(object? obj) => obj is Position p && Equals(p);
    public override int GetHashCode() => HashCode.Combine(X, Y);
    public override string ToString() => $"({X},{Y})";
}