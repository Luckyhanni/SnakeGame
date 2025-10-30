using System.Collections.Generic;

namespace SnakeGame;

public class Snake
{
    public Direction Direction { get; private set; } = Direction.Right;
    public List<Position> Body { get; } = new List<Position>();

    public Snake(Position start, int length = 3)
    {
        for (int i = 0; i < length; i++)
            Body.Add(new Position(start.X - i, start.Y));
    }

    public Position Head() => Body[0];

    public void SetDirection(Direction d)
    {
        // kein sofortiges Umdrehen in den eigenen Körper
        if ((Direction == Direction.Up    && d == Direction.Down) ||
            (Direction == Direction.Down  && d == Direction.Up)   ||
            (Direction == Direction.Left  && d == Direction.Right)||
            (Direction == Direction.Right && d == Direction.Left)) return;

        Direction = d;
    }

    public void Move(bool grow = false)
    {
        var newHead = Head().Translate(Direction);
        Body.Insert(0, newHead);
        if (!grow) Body.RemoveAt(Body.Count - 1);
    }

    public void Grow() => Move(grow: true);

    public bool CollidesWithSelf()
    {
        var h = Head();
        for (int i = 1; i < Body.Count; i++)
            if (Body[i].Equals(h)) return true;
        return false;
    }
}