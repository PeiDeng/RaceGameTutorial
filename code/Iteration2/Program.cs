using System;
using SplashKitSDK;

public class Program
{
    public static void Main()
    {
        Window _window = new Window("RaceGame", 800, 800);
        RaceGame _raceGame = new RaceGame(_window);
        
        while (!_window.CloseRequested)
        {
            SplashKit.ProcessEvents();
            _window.Clear(Color.RGBColor(193, 154, 107));
            _raceGame.Move();
            _raceGame.Draw();
            _window.Refresh();
        }
        _window.Close();
        _window = null;
    }
}