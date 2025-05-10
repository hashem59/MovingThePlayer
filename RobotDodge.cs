using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace MovingThePlayer
{
  public class RobotDodge
  {
    private Player _player;
    private Window _gameWindow;
    private List<Robot> _robots;
    private List<Bullet> _bullets;
    private SplashKitSDK.Timer _gameTimer;
    private const int SCORE_INTERVAL = 1000; // 1 second in milliseconds

    public bool Quit
    {
      get { return _player.Quit; }
    }

    public RobotDodge(Window gameWindow)
    {
      _gameWindow = gameWindow;
      _player = new Player(_gameWindow);
      _robots = new List<Robot>();
      _bullets = new List<Bullet>();
      _gameTimer = new SplashKitSDK.Timer("GameTimer");
      _gameTimer.Start();
    }

    public void HandleInput()
    {
      _player.HandleInput();
      _player.StayOnWindow(_gameWindow);

      // Handle shooting
      if (SplashKit.MouseClicked(MouseButton.LeftButton))
      {
        _bullets.Add(new Bullet(_player, SplashKit.MousePosition()));
      }
    }

    public void Draw()
    {
      _gameWindow.Clear(Color.White);
      foreach (Robot robot in _robots)
      {
        robot.Draw();
      }
      foreach (Bullet bullet in _bullets)
      {
        bullet.Draw();
      }
      _player.Draw();
      _gameWindow.Refresh(60);
    }

    private Robot RandomRobot()
    {
      double r = SplashKit.Rnd();
      if (r < 1.0 / 3.0)
        return new Boxy(_gameWindow, _player);
      else if (r < 2.0 / 3.0)
        return new Roundy(_gameWindow, _player);
      else
        return new ZigZaggy(_gameWindow, _player);
    }

    public void Update()
    {
      // Update score based on time
      if (_gameTimer.Ticks > SCORE_INTERVAL)
      {
        _player.UpdateScore(1);
        _gameTimer.Reset();
      }

      // Add new robots if needed
      if (_robots.Count < 3)
      {
        _robots.Add(RandomRobot());
      }

      // Update robots
      foreach (Robot robot in _robots)
      {
        robot.Update();
      }

      // Update bullets
      foreach (Bullet bullet in _bullets.ToArray())
      {
        bullet.Update();
        if (bullet.IsOffscreen(_gameWindow))
        {
          _bullets.Remove(bullet);
        }
      }

      CheckCollisions();
    }

    public void CheckCollisions()
    {
      List<Robot> robotsToRemove = new List<Robot>();
      List<Bullet> bulletsToRemove = new List<Bullet>();

      // Check player-robot collisions
      foreach (Robot robot in _robots)
      {
        if (_player.CollidedWith(robot))
        {
          _player.LoseLife();
          robotsToRemove.Add(robot);
        }
      }

      // Check bullet-robot collisions
      foreach (Bullet bullet in _bullets)
      {
        foreach (Robot robot in _robots)
        {
          if (bullet.CollidedWith(robot))
          {
            robotsToRemove.Add(robot);
            bulletsToRemove.Add(bullet);
            break;
          }
        }
      }

      // Remove collided robots and bullets
      foreach (Robot robot in robotsToRemove)
      {
        _robots.Remove(robot);
      }
      foreach (Bullet bullet in bulletsToRemove)
      {
        _bullets.Remove(bullet);
      }

      // Remove offscreen robots
      foreach (Robot robot in _robots.ToArray())
      {
        if (robot.IsOffscreen(_gameWindow))
        {
          _robots.Remove(robot);
        }
      }
    }
  }
}