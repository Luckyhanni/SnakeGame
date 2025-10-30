using Xunit;
using SnakeGame;

namespace SnakeGame.Tests;

public class GameTests
{
    [Fact]
    public void Game_Ends_When_Snake_Hits_Wall()
    {
        var g = new Game(width: 10, height: 6);
        // nach ein paar Ticks trifft die Schlange sicher die Wand
        for (int i = 0; i < 20 && !g.IsGameOver; i++) g.Tick();
        Assert.True(g.IsGameOver);
    }

    [Fact]
    public void Score_Increases_When_Eating_Food()
    {
        var g = new Game(width: 12, height: 8);
        var head = g.Snake.Head();
        var next = head.Translate(g.Snake.Direction);
        typeof(Food).GetProperty("Position")!.SetValue(g.Food, next);
        int old = g.Score;
        g.Tick();
        Assert.True(g.Score > old);
    }
}