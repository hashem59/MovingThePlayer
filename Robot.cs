using System;
using SplashKitSDK;

namespace MovingThePlayer
{
  public class Robot
  {
    private double X { get; set; }
    private double Y { get; set; }
    private Color MainColor { get; set; }
    private Vector2D Velocity { get; set; }

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

    public void Update()
    {
      X += Velocity.X;
      Y += Velocity.Y;
    }

    public void Draw()
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


    public bool IsOffscreen(Window gameWindow)
    {
      return X < -Width || X > gameWindow.Width || Y < -Height || Y > gameWindow.Height;
    }
  }
}