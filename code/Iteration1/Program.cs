using System;
using SplashKitSDK;

public class Program
{
    public static void Main()
    {
        Window window = new Window("RaceGame", 800, 600);
        Bitmap Road = new Bitmap("Road", "Road1.png");
        Bitmap player = new Bitmap("Player", "Playercar1.png");
        const int LANE_LEFT = 250;
        const int LANE_WIDTH = 60;
        while (!window.CloseRequested)
        {
            SplashKit.ProcessEvents();
            window.Clear(Color.RGBColor(193, 154, 107));
            Road.Draw((window.Width - Road.Width) / 2, 0);
            player.Draw(LANE_LEFT + LANE_WIDTH * 2, window.Height - player.Height);
            window.Refresh();
        }
        window.Close();
        window = null;
    }
}