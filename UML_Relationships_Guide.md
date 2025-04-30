# UML Relationships Guide for Robot Dodge

## Types of Relationships

### 1. Composition (Strong Ownership)
- Notation: Filled diamond arrow (♦──►)
- In our system:
  ```
  RobotDodge ♦──► Player
  ```
- Explanation: RobotDodge completely owns the Player. The Player cannot exist without RobotDodge.
- Multiplicity: 1 to 1 (One RobotDodge has exactly one Player)
- Implementation: `private Player _player;` in RobotDodge class

### 2. Aggregation (Collection Relationship)
- Notation: Empty diamond arrow (◇──►)
- In our system:
  ```
  RobotDodge ◇──► Robot
  RobotDodge ◇──► Bullet
  ```
- Explanation: RobotDodge maintains collections of Robots and Bullets
- Multiplicity: 1 to many (One RobotDodge has multiple Robots and Bullets)
- Implementation: 
  ```csharp
  private List<Robot> _robots;
  private List<Bullet> _bullets;
  ```

### 3. Dependencies (Uses Relationship)
- Notation: Dashed arrow (---->)
- In our system:
  ```
  Program ----> RobotDodge
  Player ----> Robot
  Bullet ----> Player
  Bullet ----> Robot
  Robot ----> Player
  ```
- Explanations:
  1. Program depends on RobotDodge to run the game
  2. Player depends on Robot for collision detection
  3. Bullet depends on Player for initialization
  4. Bullet depends on Robot for collision detection
  5. Robot depends on Player for tracking movement

## Drawing Guidelines

### 1. Class Box Structure
```
┌─────────────────┐
│    ClassName    │
├─────────────────┤
│   Attributes    │
├─────────────────┤
│    Methods      │
└─────────────────┘
```

### 2. Relationship Placement
- Place related classes closer to each other
- Minimize line crossings
- Use straight lines where possible
- Add arrow heads in correct direction

### 3. Multiplicity Notation
Place multiplicity indicators near the ends of relationship lines:
```
RobotDodge ◇──► Robot
     1         0..*    (One RobotDodge to many Robots)

RobotDodge ♦──► Player
     1         1       (One RobotDodge to one Player)
```

### 4. Suggested Layout
```
┌─────────────┐
│   Program   │
└─────┬───────┘
      |
      ▼
┌─────────────┐     ┌─────────────┐
│ RobotDodge  │ ♦─► │   Player    │
└─────────────┘     └─────────────┘
      ◇                    ▲
      |              - - - | 
    ┌─┴─────────┐         |
    ▼           ▼    ┌────┴────┐
┌────────┐  ┌────────┐   │  Robot   │
│ Bullet │  │ Robot  │   └─────────┘
└────────┘  └────────┘
```

## Implementation Details

### 1. Composition Example (RobotDodge-Player)
```csharp
public class RobotDodge
{
    private Player _player;
    
    public RobotDodge(Window gameWindow)
    {
        _player = new Player(gameWindow);
    }
}
```

### 2. Aggregation Example (RobotDodge-Robot/Bullet)
```csharp
public class RobotDodge
{
    private List<Robot> _robots;
    private List<Bullet> _bullets;
    
    public RobotDodge(Window gameWindow)
    {
        _robots = new List<Robot>();
        _bullets = new List<Bullet>();
    }
}
```

### 3. Dependency Example (Bullet-Robot)
```csharp
public class Bullet
{
    public bool CollidedWith(Robot robot)
    {
        return SplashKit.CirclesIntersect(
            SplashKit.CircleAt(_x, _y, Radius),
            robot.CollisionCircle
        );
    }
}
```

## Best Practices
1. Always show relationship direction
2. Include multiplicity where relevant
3. Use appropriate relationship type:
   - Composition for strong ownership
   - Aggregation for collections
   - Dependencies for usage relationships
4. Keep the diagram clean and readable
5. Group related classes together 