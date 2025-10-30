using System;

namespace SnakeGame;

public static class Program
{
    public static void Main()
    {
        Console.CursorVisible = false;
        var game = new Game(width: 30, height: 18);
        game.Run();
    }
}