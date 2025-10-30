using Xunit;
using SnakeGame;

namespace SnakeGame.Tests;

public class SnakeTests
{
    [Fact]
    public void Snake_Moves_Correctly_Right()
    {
        var s = new Snake(new Position(5, 5), length: 3);
        var oldHead = s.Head();
        s.SetDirection(Direction.Right);
        s.Move();
        Assert.Equal(new Position(oldHead.X + 1, oldHead.Y), s.Head());
    }

    [Fact]
    public void Snake_Grows_On_Grow()
    {
        var s = new Snake(new Position(5, 5), length: 3);
        int oldLen = s.Body.Count;
        s.Grow();
        Assert.Equal(oldLen + 1, s.Body.Count);
    }

    [Fact]
    public void Snake_SelfCollision_Detected_Deterministically()
    {
        var s = new Snake(new Position(5, 5), length: 5);
        s.SetDirection(Direction.Down); s.Move();
        s.SetDirection(Direction.Left); s.Move();
        s.SetDirection(Direction.Up); s.Move();
        s.SetDirection(Direction.Right); s.Move(); // -> trifft den Körper
        Assert.True(s.CollidesWithSelf());
    }
}