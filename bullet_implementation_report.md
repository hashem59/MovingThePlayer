# Bullet Implementation Report

## 1. Modeling Bullets

Bullets are represented by the `Bullet` class in `Bullet.cs`. Each instance encapsulates:

- **Position (`_x`, `_y`)**: stored as doubles, initialized to the player's center point at the moment of firing.
- **Velocity (`_velocity`)**: a unit direction vector (from player to mouse target) scaled by a constant speed (`SPEED = 8.0`).
- **Visual (`_bulletBitmap`)**: a bitmap loaded once per instance for rendering.

Construction takes two parameters (`Player player`, `Point2D target`), computes a normalized direction vector from the player's current position to the mouse click target, then multiplies by `SPEED` to set `_velocity`.

## 2. Movement Mechanics

Movement is handled entirely in `Bullet.Update()`, which advances the bullet by adding `(_velocity.X, _velocity.Y)` to its `(X, Y)` each frame.  

Off‐screen bullets are detected via `Bullet.IsOffscreen(Window)`, which checks whether the bullet's center has moved beyond the window bounds plus its radius. When off‐screen, the bullet is removed from the active list in the main loop.

## 3. Rendering Orientation

In `Bullet.Draw()`, the current velocity direction is converted into an angle (degrees) via `SplashKit.VectorAngle(_velocity)`. The bitmap is then drawn at `(X – Width/2, Y – Height/2)` rotated to face the direction of movement, ensuring the sprite visually aligns with its trajectory.

Internally, `VectorAngle` uses `atan2(velocity.Y, velocity.X)` to compute the rotation in degrees from the positive X‐axis, and this angle is passed to `OptionRotateBmp(angle)` to rotate the bullet sprite about its center.

## 4. Collision Detection & Robot Destruction

- **Detection**: `Bullet.CollidedWith(Robot robot)` calls `SplashKit.CirclesIntersect` on two circles: one centered at the bullet's `(X, Y)` with radius `_bulletBitmap.Width/2`, and the robot's `CollisionCircle`, which has a fixed radius of 20 (as defined in `Robot.CollisionCircle`).  

- **Resolution**: In `RobotDodge.CheckCollisions()`, bullet-robot collision pairs are collected and both objects are flagged for removal.  

- **Removal**: After detection, the main update loop removes any collided bullets and robots from their respective lists, ensuring they no longer participate in subsequent updates or rendering.

## 5. Integration & Lifecycle Management

To incorporate bullets into the existing architecture:

- **Input Handling**: In `RobotDodge.HandleInput()`, a new `Bullet` is created on left‐mouse click and added to `_bullets`.
- **Draw Loop**: Bullets are drawn between robots and the player in `RobotDodge.Draw()`.
- **Update Loop**: Bullets are advanced and pruned for off‐screen removal in `RobotDodge.Update()` alongside robot updates.
- **Collision Orchestration**: Centralized in `RobotDodge.CheckCollisions()`, maintaining separation of concerns.
