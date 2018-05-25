using System;
using SplashKitSDK;
using System.Collections.Generic;

public class RaceGame
{
    private Window _window;
    private Player _player;
    private Map _map;
    private List<AI> _ai = new List<AI>();
    public bool Restart;
    private bool _addNew;
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
        _ai.Add(new AI());
    }

    public void Update()
    {
        _player.Move();
        _map.Move();
        foreach (AI ai in _ai)
        {
            ai.Move();
        }
        Collision();
        CheckOverLine();
        AddNewCar();
		RemoveAI();
    }

    public void Draw()
    {
        _map.Draw();
        _player.Draw();
        foreach (AI ai in _ai)
        {
            ai.Draw();
        }
    }

    public void Collision()
    {
        foreach (AI ai in _ai)
        {
            if (ai.ColliedWith(_player))
            {
                SplashKit.DisplayDialog("GameOver", "GameOver", SplashKit.FontNamed("FontC"), 20);
                Restart = true;
            }
        }
    }

    public void CheckOverLine()
    {
        foreach (AI ai in _ai)
        {
            if (ai.IsOverLine != true && ai.Y > _window.Height / 4)
            {
                _addNew = true;
                ai.IsOverLine = true;
            }
        }
    }

    public void AddNewCar()
    {
        if (_addNew == true)
        {
            _ai.Add(new AI());
            _addNew = false;
        }
    }

    public void RemoveAI()
    {
        List<AI> _uselessAI = new List<AI>();
        foreach (AI ai in _ai)
        {
            if (ai.Y > _window.Height || ai.ColliedWith(_player))
            {
                _uselessAI.Add(ai);
            }
        }
        foreach (AI r in _uselessAI)
        {
            _ai.Remove(r);
        }
    }
}