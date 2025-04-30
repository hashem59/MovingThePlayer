# UML Update Report

## Overview
This report provides a complete listing of all classes, their attributes, methods, and relationships in the Robot Dodge game implementation.

## Complete Class Specifications

### Program Class
Attributes:
- None

Methods:
- `+ Main () <<static>>`

### Player Class
Attributes:
- `- _playerBitmap : Bitmap`
- `- _heartBitmap : Bitmap`
- `+ X : double <<auto property>>`
- `+ Y : double <<auto property>>`
- `+ Lives : int <<auto property>>`
- `+ Score : int <<auto property>>`
- `+ Width : int <<readonly property>>`
- `+ Height : int <<readonly property>>`
- `+ Quit : bool <<auto property, private set>>`
- `- SPEED : double <<const>>`
- `- GAP : double <<const>>`

Methods:
- `+ Player (gameWindow : Window)`
- `+ Draw ()`
- `+ HandleInput ()`
- `+ StayOnWindow (limit : Window)`
- `+ LoseLife ()`
- `+ UpdateScore (points : int)`
- `+ CollidedWith (other : Robot) : bool`

### RobotDodge Class
Attributes:
- `- _player : Player`
- `- _gameWindow : Window`
- `- _robots : List<Robot>`
- `- _bullets : List<Bullet>`
- `- _gameTimer : Timer`
- `- SCORE_INTERVAL : int <<const>>`
- `+ Quit : bool <<readonly property>>`

Methods:
- `+ RobotDodge (gameWindow : Window)`
- `+ HandleInput ()`
- `+ Draw ()`
- `- RandomRobot () : Robot`
- `+ Update ()`
- `- CheckCollisions ()`

### Robot Class
Attributes:
- `- X : double <<property>>`
- `- Y : double <<property>>`
- `- MainColor : Color <<property>>`
- `- Velocity : Vector2D <<property>>`
- `+ Width : int <<readonly property>>`
- `+ Height : int <<readonly property>>`
- `+ CollisionCircle : Circle <<readonly property>>`

Methods:
- `+ Robot (gameWindow : Window, player : Player)`
- `+ Update ()`
- `+ Draw ()`
- `+ IsOffscreen (gameWindow : Window) : bool`

### Bullet Class
Attributes:
- `- _x : double`
- `- _y : double`
- `- _velocity : Vector2D`
- `- SPEED : double <<const>>`
- `- _bulletBitmap : Bitmap`
- `+ X : double <<readonly property>>`
- `+ Y : double <<readonly property>>`
- `+ Radius : int <<readonly property>>`

Methods:
- `+ Bullet (player : Player, target : Point2D)`
- `+ Update ()`
- `+ Draw ()`
- `+ IsOffscreen (gameWindow : Window) : bool`
- `+ CollidedWith (robot : Robot) : bool`

## Class Relationships

### Associations
1. RobotDodge -> Player (composition, 1 to 1)
2. RobotDodge -> Robot (aggregation, 1 to many)
3. RobotDodge -> Bullet (aggregation, 1 to many)

### Dependencies
1. Program -> RobotDodge
2. Player -> Robot (for collision detection)
3. Bullet -> Player (for initialization)
4. Bullet -> Robot (for collision detection)
5. Robot -> Player (for tracking)

## UML Diagram Guidelines

### Visual Elements
1. Use proper visibility modifiers:
   - `-` for private members
   - `+` for public members

2. Property Stereotypes:
   - `<<auto property>>` for automatic properties
   - `<<readonly property>>` for read-only properties
   - `<<property>>` for properties with custom getter/setter

3. Other Stereotypes:
   - `<<const>>` for constant fields
   - `<<static>>` for static members

### Relationship Notation
1. Composition: filled diamond arrow
2. Aggregation: hollow diamond arrow
3. Dependency: dashed arrow
4. Association: solid arrow

### Method Signatures
- Include full parameter lists with types
- Show return types for all methods
- Include visibility modifiers

## Implementation Notes
1. All properties must show their access modifiers and appropriate stereotypes
2. Constants should be marked with <<const>> stereotype
3. Static methods should be marked with <<static>> stereotype
4. All relationships should be clearly shown with proper multiplicity 