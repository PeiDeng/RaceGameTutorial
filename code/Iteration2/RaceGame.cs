using System;
using SplashKitSDK;

public class RaceGame
{
    private Window _window;
    private Bitmap _player;
    private Bitmap _road;
    private Bitmap _cactusBitmap;
    private Timer _myTimer;
    private double _cactus1X;
    private double _cactus1Y;
    private double _cactus2X;
    private double _cactus2Y;
    public int CactusSpeed = 5;
    public const int LANE_LEFT = 250;
    public const int LANE_WIDTH = 60;

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
        _player = SplashKit.BitmapNamed("Player1");
        _myTimer = new Timer("timer");
        _myTimer.Start();

        _cactusBitmap = SplashKit.BitmapNamed("Cactus");
        _cactus1X = LANE_LEFT - _cactusBitmap.Width - 5;
        _cactus1Y = 0;
        _cactus2X = LANE_LEFT + LANE_WIDTH * 5 + 5;
        _cactus2Y = -_window.Height / 2;
    }

    public void Move()
    {
        RoadMove();
        CactusMove();
    }
    public void Draw()
    {
        _road.Draw((_window.Width - _road.Width) / 2, 0);
        _player.Draw(LANE_LEFT + LANE_WIDTH * 2, _window.Height - _player.Height);
        _cactusBitmap.Draw(_cactus1X, _cactus1Y);
        _cactusBitmap.Draw(_cactus2X, _cactus2Y);
    }

    public void RoadMove()
    {
        if (_myTimer.Ticks < 200)
        {
            _road = SplashKit.BitmapNamed("Road1");
        }
        if (_myTimer.Ticks >= 200 && _myTimer.Ticks < 400)
        {
            _road = SplashKit.BitmapNamed("Road2");
        }
        if (_myTimer.Ticks >= 400 && _myTimer.Ticks < 600)
        {
            _road = SplashKit.BitmapNamed("Road3");
        }
        if (_myTimer.Ticks >= 600)
        {
            _myTimer.Start();
        }
    }

    public void CactusMove()
    {
        _cactus1Y += CactusSpeed;     // make the cactus1 move down
        if (_cactus1Y >= _window.Height)
        {
            _cactus1Y = 0;    //reset the Y value when it out of screen
        }
        _cactus2Y += CactusSpeed; // make the cactus2 move down
        if (_cactus2Y >= _window.Height)
        {
            _cactus2Y = 0;   //reset the Y value when it out of screen
        }
    }
}