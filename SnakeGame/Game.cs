using System;
using System.Threading;

namespace SnakeGame;

public class Game
{
    public int Width { get; }
    public int Height { get; }
    public Snake Snake { get; }
    public Food Food { get; }
    public int Score { get; private set; }
    public bool IsGameOver { get; private set; }

    private const int TickMs = 120;

    public Game(int width = 30, int height = 18)
    {
        Width = width;
        Height = height;
        Snake = new Snake(new Position(width / 2, height / 2));
        Food = new Food(new Position(Width / 3, Height / 3));
    }

    public void Run()
    {
        PrepareConsole();

        while (!IsGameOver)
        {
            HandleInput();
            Tick();
            Render();
            Thread.Sleep(TickMs);
        }

        Console.WriteLine();
        Console.WriteLine($"Game Over! Score: {Score}");
        Console.CursorVisible = true;
    }

    public void Tick()
    {
        bool eats = Snake.Head().Translate(Snake.Direction).Equals(Food.Position);
        if (eats)
        {
            Snake.Grow();
            Score += 10;
            Food.Respawn(Width, Height, Snake.Body);
        }
        else
        {
            Snake.Move();
        }

        var h = Snake.Head();
        if (h.X <= 0 || h.X >= Width - 1 || h.Y <= 0 || h.Y >= Height - 1)
        {
            IsGameOver = true;
            return;
        }

        if (Snake.CollidesWithSelf()) IsGameOver = true;
    }

    private void HandleInput()
    {
        while (Console.KeyAvailable)
        {
            var key = Console.ReadKey(intercept: true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow: Snake.SetDirection(Direction.Up); break;
                case ConsoleKey.DownArrow: Snake.SetDirection(Direction.Down); break;
                case ConsoleKey.LeftArrow: Snake.SetDirection(Direction.Left); break;
                case ConsoleKey.RightArrow: Snake.SetDirection(Direction.Right); break;
                case ConsoleKey.Escape: IsGameOver = true; break;
            }
        }
    }

    private void PrepareConsole()
    {
        try
        {
            int minWidth = Math.Max(Width, 40);
            int minHeight = Math.Max(Height + 4, 20);
            if (Console.BufferWidth  < minWidth)  Console.BufferWidth  = minWidth;
            if (Console.BufferHeight < minHeight) Console.BufferHeight = minHeight;
            if (minWidth  <= Console.LargestWindowWidth  && Console.WindowWidth  < minWidth)  Console.WindowWidth  = minWidth;
            if (minHeight <= Console.LargestWindowHeight && Console.WindowHeight < minHeight) Console.WindowHeight = minHeight;
        }
        catch { /* manche Hosts erlauben kein Resizing */ }
        try { Console.CursorVisible = false; } catch { }
        Console.Clear();
    }

    private void Render()
    {
        try { Console.SetCursorPosition(0, 0); } catch { Console.Clear(); }

        Console.Write('+' + new string('-', Width - 2) + "+\n");
        for (int y = 1; y < Height - 1; y++)
        {
            Console.Write('|');
            for (int x = 1; x < Width - 1; x++)
            {
                var pos = new Position(x, y);
                char c = ' ';
                if (pos.Equals(Snake.Head())) c = 'O';
                else if (Snake.Body.Contains(pos)) c = 'o';
                else if (pos.Equals(Food.Position)) c = '*';
                Console.Write(c);
            }
            Console.Write("|\n");
        }
        Console.Write('+' + new string('-', Width - 2) + "+\n");
        Console.WriteLine($"Score: {Score}   ESC to quit");
    }
}
