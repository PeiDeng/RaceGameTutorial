using System;
using SplashKitSDK;

public class RaceGame
{
    private Window _window;
    private Player _player;
    private Map _map;
    private AI _ai;
    public bool Restart;
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
        SplashKit.LoadBitmap("AICar1", "AICar1.png");
        SplashKit.LoadBitmap("Road1", "Road1.png");
        SplashKit.LoadBitmap("Road2", "Road2.png");
        SplashKit.LoadBitmap("Road3", "Road3.png");
        SplashKit.LoadBitmap("Cactus", "Cactus.png");
        SplashKit.LoadFont("FontC", "calibri.ttf");
    }

    public RaceGame(Window w)
    {
        _window = w;
        LoadResource();
        _player = new Player(_window);
        _map = new Map(_window);
        _ai = new AI();
    }

    public void Update()
    {
        _player.Move();
        _map.Move();
        _ai.Move();
        Collision();
    }

    public void Draw()
    {
        _map.Draw();
        _player.Draw();
        _ai.Draw();
    }

    public void Collision()
    {
        if (_ai.ColliedWith(_player))
        {
            SplashKit.DisplayDialog("GameOver", "GameOver", SplashKit.FontNamed("FontC"), 20);
            Restart = true;
        }
    }
}