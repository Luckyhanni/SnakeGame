using System;
using System.Collections.Generic;

namespace SnakeGame;

public class Food
{
    private readonly Random _rng = new Random();
    public Position Position { get; private set; }

    public Food(Position start) => Position = start;

    public void Respawn(int width, int height, List<Position> occupied)
    {
        Position p;
        int safety = 0;
        do
        {
            p = new Position(_rng.Next(1, width - 1), _rng.Next(1, height - 1));
            safety++;
            if (safety > 1000) break;
        } while (occupied.Contains(p));
        Position = p;
    }
}