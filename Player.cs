using System;
using SplashKitSDK;

namespace MovingThePlayer
{
  public class Player
  {
    private Bitmap _playerBitmap;
    private Bitmap _heartBitmap;
    public double X { get; set; }
    public double Y { get; set; }
    public int Lives { get; private set; }
    public int Score { get; private set; }
    public int Width
    {
      get { return _playerBitmap.Width; }
    }
    public int Height
    {
      get { return _playerBitmap.Height; }
    }
    public bool Quit { get; private set; }
    private const double SPEED = 5.0;
    private const double GAP = 10.0;

    public Player(Window gameWindow)
    {
      _playerBitmap = new Bitmap("Player", "Player.png");
      _heartBitmap = new Bitmap("Heart", "heart.png");
      Quit = false;
      Lives = 5;
      Score = 0;

      // Center the player on the screen after bitmap is loaded
      X = (gameWindow.Width - Width) / 2.0;
      Y = (gameWindow.Height - Height) / 2.0;
    }

    public void Draw()
    {
      SplashKit.DrawBitmap(_playerBitmap, X, Y);
      // Draw one heart icon
      SplashKit.DrawBitmap(_heartBitmap, 5, 5);
      // Draw lives count below the heart
      SplashKit.DrawText($"x{Lives}", Color.Black, 35, 15);
      // Draw score
      SplashKit.DrawText($"Score: {Score}", Color.Black, 62, 15);
    }

    public void HandleInput()
    {
      if (SplashKit.KeyTyped(KeyCode.EscapeKey))
      {
        Quit = true;
      }
      if (SplashKit.KeyDown(KeyCode.LeftKey))
      {
        X -= SPEED;
      }
      if (SplashKit.KeyDown(KeyCode.RightKey))
      {
        X += SPEED;
      }
      if (SplashKit.KeyDown(KeyCode.UpKey))
      {
        Y -= SPEED;
      }
      if (SplashKit.KeyDown(KeyCode.DownKey))
      {
        Y += SPEED;
      }
    }

    public void StayOnWindow(Window gameWindow)
    {
      // Check left boundary
      if (X < GAP)
      {
        X = GAP;
      }
      // Check right boundary
      if (X + Width > gameWindow.Width - GAP)
      {
        X = gameWindow.Width - Width - GAP;
      }
      // Check top boundary
      if (Y < GAP)
      {
        Y = GAP;
      }
      // Check bottom boundary
      if (Y + Height > gameWindow.Height - GAP)
      {
        Y = gameWindow.Height - Height - GAP;
      }
    }

    public void LoseLife()
    {
      Lives--;
      if (Lives <= 0)
      {
        Quit = true;
      }
    }

    public void UpdateScore(int points)
    {
      Score += points;
    }

    public bool CollidedWith(Robot other)
    {
      return _playerBitmap.CircleCollision(X, Y, other.CollisionCircle);
    }
  }
}