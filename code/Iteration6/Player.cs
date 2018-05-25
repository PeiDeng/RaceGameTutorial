using System;
using SplashKitSDK;

public class Player : IMovable
{
    private Window _gameWindow;
    public Bitmap CarBitmap;
    public double X;
    public double Y;
    public bool Quit;

    public Player(Window window)
    {
        CarBitmap = SplashKit.BitmapNamed("Player1");
        _gameWindow = window;
        X = Map.LANE_LEFT + Map.LANE_WIDTH * 2;
        Y = _gameWindow.Height - CarBitmap.Height;
    }

    public void HandleInput()
    {
        int movement = Map.LANE_WIDTH;
        int speed = 4;
        if (SplashKit.KeyReleased(KeyCode.RightKey) || SplashKit.KeyReleased(KeyCode.DKey))
        {
            X += movement;
        }
        if (SplashKit.KeyReleased(KeyCode.LeftKey) || SplashKit.KeyReleased(KeyCode.AKey))
        {
            X -= movement;
        }
        if (SplashKit.KeyDown(KeyCode.UpKey) || SplashKit.KeyDown(KeyCode.WKey))
        {
            Y -= speed;
        }
        if (SplashKit.KeyDown(KeyCode.DownKey) || SplashKit.KeyDown(KeyCode.SKey))
        {
            Y += speed;
        }
        if (SplashKit.KeyReleased(KeyCode.EscapeKey))
        {
            Quit = true;
        }
    }

    public void Move()
    {
        HandleInput();
        StayInTrack();
    }

    public void Draw()
    {
        CarBitmap.Draw(X, Y);
    }

    private void StayInTrack()
    {
        if (X >= Map.LANE_LEFT + Map.LANE_WIDTH * 5) //the right side of track
        {
            X -= Map.LANE_WIDTH;
        }
        if (X < Map.LANE_LEFT) //the left side of track
        {
            X += Map.LANE_WIDTH;
        }
        if (Y > _gameWindow.Height - CarBitmap.Height)
        {
            Y = _gameWindow.Height - CarBitmap.Height;
        }
        if (Y < 0)
        {
            Y = 0;
        }
    }
}