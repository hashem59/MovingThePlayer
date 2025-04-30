using System;
using SplashKitSDK;

namespace MovingThePlayer
{
    public class RobotDodge
    {
        private Player _Player;
        private Window _GameWindow;
        private List<Robot> _Robots;
        public bool Quit
        {
            get
            {
                return _Player.Quit;
            }
        }

        public RobotDodge(Window gameWindow)
        {
            _GameWindow = gameWindow;
            _Player = new Player(_GameWindow);
            _Robots = new List<Robot>();
        }

        public void HandleInput()
        {
            _Player.HandleInput();
            _Player.StayOnWindow(_GameWindow);
        }

        public void Draw()
        {
            _GameWindow.Clear(Color.White);
            foreach (Robot robot in _Robots)
            {
                robot.Draw();
            }
            _Player.Draw();
            _GameWindow.Refresh(60);
        }

        private Robot RandomRobot(Player player)
        {
            return new Robot(_GameWindow, player);
        }

        public void Update()
        {
            // add a new robot if needed
            if (_Robots.Count < 3)
            {
                _Robots.Add(RandomRobot(_Player));
            }

            foreach (Robot robot in _Robots)
            {
                robot.Update();
            }



            CheckCollisions();
        }


        public void CheckCollisions()
        {
            // create new list of robots to remove
            List<Robot> robotsToRemove = new List<Robot>();

            foreach (Robot robot in _Robots)
            {
                if (_Player.CollidedWith(robot) || robot.IsOffscreen(_GameWindow))
                {
                    robotsToRemove.Add(robot);
                }
            }

            foreach (Robot robot in robotsToRemove)
            {
                _Robots.Remove(robot);
            }
        }
    }
} 