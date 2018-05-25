using System;
using SplashKitSDK;

public class AI
{
    public Bitmap CarBitmap;
    public double X;
    public double Y;
    public double Speed = 3;
    public AI()
    {
        CarBitmap = SplashKit.BitmapNamed("AICar1"); //Load bitmap resources in LoadResource()
        X = Map.LANE_LEFT + Map.LANE_WIDTH * 2;
        Y = -CarBitmap.Height;
    }

    public void Draw()
    {
        CarBitmap.Draw(X, Y);
    }

    public void Move()
    {
        Y += Speed;
    }

    public bool ColliedWith(Player p)
    {
        return CarBitmap.BitmapCollision(X, Y, p.CarBitmap, p.X, p.Y);
    }
}