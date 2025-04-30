using System;
using SplashKitSDK;

namespace MovingThePlayer
{
  public class Bullet
  {
    private double _x;
    private double _y;
    private Vector2D _velocity;
    private const double SPEED = 8.0;
    private const int RADIUS = 5;

    public double X { get { return _x; } }
    public double Y { get { return _y; } }
    public int Radius { get { return RADIUS; } }

    public Bullet(Player player, Point2D target)
    {
      _x = player.X + player.Width / 2;
      _y = player.Y + player.Height / 2;

      // Calculate direction to target
      Point2D fromPt = new Point2D() { X = _x, Y = _y };
      Vector2D dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, target));
      _velocity = SplashKit.VectorMultiply(dir, SPEED);
    }

    public void Update()
    {
      _x += _velocity.X;
      _y += _velocity.Y;
    }

    public void Draw()
    {
      SplashKit.FillCircle(Color.Red, _x, _y, RADIUS);
    }

    public bool IsOffscreen(Window gameWindow)
    {
      return _x < -RADIUS || _x > gameWindow.Width + RADIUS ||
             _y < -RADIUS || _y > gameWindow.Height + RADIUS;
    }

    public bool CollidedWith(Robot robot)
    {
      return SplashKit.CirclesIntersect(
          SplashKit.CircleAt(_x, _y, RADIUS),
          robot.CollisionCircle
      );
    }
  }
}