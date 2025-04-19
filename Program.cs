using System;
using SplashKitSDK;

namespace MovingThePlayer
{
    public class Program
    {
        public static void Main()
        {
            Window gameWindow = new Window("Moving The Player", 800, 600);
            Player player = new Player(gameWindow);

            while (!player.Quit && !gameWindow.CloseRequested)
            {
                SplashKit.ProcessEvents();
                player.HandleInput();
                player.StayOnWindow(gameWindow);
                gameWindow.Clear(Color.White);
                player.Draw();
                gameWindow.Refresh(60);
            }
        }
    }
}
