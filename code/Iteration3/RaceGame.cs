using System;
using SplashKitSDK;

public class RaceGame
{
    private Window _window;
    private Player _player;
    private Map _map;
    public bool ESC
    {
        get
        {
            return _player.Quit;
        }
    }
    public void LoadResource()
    {
        SplashKit.LoadBitmap("Player1", "PlayerCar1.png");
        SplashKit.LoadBitmap("Road1", "Road1.png");
        SplashKit.LoadBitmap("Road2", "Road2.png");
        SplashKit.LoadBitmap("Road3", "Road3.png");
        SplashKit.LoadBitmap("Cactus", "Cactus.png");
    }

    public RaceGame(Window w)
    {
        _window = w;
        LoadResource();
        _player = new Player(_window);
        _map = new Map(_window);
    }

    public void Update()
    {
        _player.Move();
        _map.Move();
    }

    public void Draw()
    {
        _map.Draw();
        _player.Draw();
    }
}