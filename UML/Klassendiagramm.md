```markdown
# Klassendiagramm (Mermaid)

```mermaid
classDiagram
    class Game {
      +int Width
      +int Height
      +Snake Snake
      +Food Food
      +int Score
      +bool IsGameOver
      +void Run()
      +void Tick()
    }

    class Snake {
      +Direction Direction
      +List~Position~ Body
      +Position Head()
      +void SetDirection(Direction d)
      +void Move()
      +void Grow()
      +bool CollidesWithSelf()
    }

    class Food {
      +Position Position
      +void Respawn(int width, int height, List~Position~ occupied)
    }

    class Position {
      +int X
      +int Y
      +Equals()
      +GetHashCode()
    }

    enum Direction {
      Up
      Down
      Left
      Right
    }

    Game --> Snake
    Game --> Food
    Snake "1" o--> "n" Position
    Food --> Position