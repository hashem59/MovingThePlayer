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
    private Bitmap _bulletBitmap;

    public double X { get { return _x; } }
    public double Y { get { return _y; } }
    public int Radius { get { return _bulletBitmap.Width / 2; } }

    public Bullet(Player player, Point2D target)
    {
      _bulletBitmap = new Bitmap("Bullet", "bullet.png");
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
      // Calculate the angle in degrees from the velocity vector
      double angle = SplashKit.VectorAngle(_velocity);
      // Draw the bitmap rotated to face the direction of movement
      SplashKit.DrawBitmap(_bulletBitmap, _x - _bulletBitmap.Width / 2, _y - _bulletBitmap.Height / 2, SplashKit.OptionRotateBmp(angle));
    }

    public bool IsOffscreen(Window gameWindow)
    {
      return _x < -Radius || _x > gameWindow.Width + Radius ||
             _y < -Radius || _y > gameWindow.Height + Radius;
    }

    public bool CollidedWith(Robot robot)
    {
      return SplashKit.CirclesIntersect(
          SplashKit.CircleAt(_x, _y, Radius),
          robot.CollisionCircle
      );
    }
  }
}