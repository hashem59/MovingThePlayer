using System;
using SplashKitSDK;

namespace MovingThePlayer
{
  public abstract class Robot
  {
    public double X { get; protected set; }
    public double Y { get; protected set; }
    public Color MainColor { get; protected set; }
    public Vector2D Velocity { get; protected set; }

    public int Width
    {
      get { return 50; }
    }

    public int Height
    {
      get { return 50; }
    }

    public Circle CollisionCircle
    {
      get { return SplashKit.CircleAt(X + Width / 2, Y + Height / 2, 20); }
    }

    public Robot(Window gameWindow, Player player)
    {
      // Randomly pick... Top / Bottom or Left / Right
      if (SplashKit.Rnd() < 0.5)
      {
        // Start by picking a random position left to right (X)
        X = SplashKit.Rnd(gameWindow.Width);

        // Now work out if we are top or bottom?
        if (SplashKit.Rnd() < 0.5)
          Y = -Height; //Top... so above top
        else
          Y = gameWindow.Height;
      }
      else
      {
        // We picked... Left / Right
        // Pick random Y position
        Y = SplashKit.Rnd(gameWindow.Height);

        // Decide left or right side
        if (SplashKit.Rnd() < 0.5)
          X = -Width; // Left side
        else
          X = gameWindow.Width; // Right side
      }

      const int SPEED = 3;

      // Get a Point for the Robot
      Point2D fromPt = new Point2D()
      {
        X = X,
        Y = Y
      };

      // Get a Point for the Player
      Point2D toPt = new Point2D()
      {
        X = player.X,
        Y = player.Y
      };

      // Calculate the direction to head.
      Vector2D dir;
      dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));

      // Set the speed and assign to the Velocity
      Velocity = SplashKit.VectorMultiply(dir, SPEED);

      MainColor = Color.RandomRGB(200);
    }

    public virtual void Update()
    {
      X += Velocity.X;
      Y += Velocity.Y;
    }

    public abstract void Draw();

    public bool IsOffscreen(Window gameWindow)
    {
      return X < -Width || X > gameWindow.Width || Y < -Height || Y > gameWindow.Height;
    }
  }

  public class Boxy : Robot
  {
    public Boxy(Window gameWindow, Player player) : base(gameWindow, player) { }

    public override void Draw()
    {
      double leftX = X + 12;
      double rightX = X + 27;
      double eyeY = Y + 10;
      double mouthY = Y + 30;

      // Draw body
      SplashKit.FillRectangle(Color.Gray, X, Y, 50, 50);

      // Draw eyes
      SplashKit.FillRectangle(MainColor, leftX, eyeY, 10, 10);
      SplashKit.FillRectangle(MainColor, rightX, eyeY, 10, 10);

      // Draw mouth
      SplashKit.FillRectangle(MainColor, leftX, mouthY, 25, 10);
      SplashKit.FillRectangle(MainColor, leftX + 2, mouthY + 2, 21, 6);
    }
  }

  public class Roundy : Robot
  {
    public Roundy(Window gameWindow, Player player) : base(gameWindow, player) { }

    public override void Draw()
    {
      double leftX, midX, rightX;
      double midY, eyeY, mouthY;
      leftX = X + 17;
      midX = X + 25;
      rightX = X + 33;
      midY = Y + 25;
      eyeY = Y + 20;
      mouthY = Y + 35;

      SplashKit.FillCircle(Color.White, midX, midY, 25);
      SplashKit.DrawCircle(Color.Gray, midX, midY, 25);
      SplashKit.FillCircle(MainColor, leftX, eyeY, 5);
      SplashKit.FillCircle(MainColor, rightX, eyeY, 5);
      SplashKit.FillEllipse(Color.Gray, X, eyeY, 50, 30);
      SplashKit.DrawLine(Color.Black, X, mouthY, X + 50, Y + 35);
    }
  }

  public class ZigZaggy : Robot
  {
    private int _zigzagCounter = 0;
    private int _zigzagDirection = 1;

    public ZigZaggy(Window gameWindow, Player player) : base(gameWindow, player) { }

    public override void Draw()
    {
      // Draw a diamond shape for ZigZaggy using two triangles
      // Top triangle
      SplashKit.FillTriangle(MainColor, X + 25, Y, X + 50, Y + 25, X, Y + 25);
      // Bottom triangle
      SplashKit.FillTriangle(MainColor, X + 25, Y + 50, X + 50, Y + 25, X, Y + 25);
      // Eyes
      SplashKit.FillCircle(Color.Black, X + 17, Y + 20, 4);
      SplashKit.FillCircle(Color.Black, X + 33, Y + 20, 4);
      // Mouth
      SplashKit.DrawLine(Color.Black, X + 18, Y + 35, X + 32, Y + 35);
    }

    public override void Update()
    {
      // Move in a zigzag pattern
      X += Velocity.X + 2 * _zigzagDirection;
      Y += Velocity.Y;
      _zigzagCounter++;
      if (_zigzagCounter % 30 == 0)
      {
        _zigzagDirection *= -1;
      }
    }
  }
}