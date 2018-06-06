# RaceGameTutorial
- This is a coding tutorial for C# beginners to code a RaceGame step by step
- The final program code is [here](https://github.com/PeiDeng/Racegame) 
- This is a study task for SIT771 of Deakin University
- This final game is look like this:

![game](/img/9.gif)



---
title: Coding a Simple RaceGame
date: 2018-06-06 03:39 UTC
tags: tutorial
author: Pei Deng
author_url: http://github.com/PeiDeng
summary: |
  SplashKit is a very helpful toolkit for programming beginner, 
  this guide will show you how to use SplashKit to code a simple C# Race Game.
related_funcs:

  - process_events
  - load_bitmap
  - bitmap_name
  - bitmap_named
  - key_down
  - key_released
  - load_font
  - font_named
  - rnd
  - display_dialog
---



- You can follow this tutorial to code a simple game iteration by iteration.
- This final game is look like this:

  ![Imgur](https://i.imgur.com/XeJCotA.gif)

##Table of Contents

* [Preparation](#preparation)
* [Iteration1](#iteration1)
* [Iteration2](#iteration2)
* [Iteration3](#iteration3)
* [Iteration4](#iteration4)
* [Iteration5](#iteration5)
* [Iteration6](#iteration6)
* [Iteration7](#iteration7)
* [Iteration8](#iteration8)

## Preparation

Before starting, the below tools need to be installed:

------

1. [Microsoft .NET framework](https://www.microsoft.com/net/download)
2. [Visual Studio Code](https://code.visualstudio.com/ )
3. [SplashKit](http://www.splashkit.io/articles/installation)

------

Now, your have all the tools we need, let us create the program files.

- Open your terminal, use the command  `cd`  to locate the folder where you want to save your program.

  ```powershell
  cd /d/xxxx/xxxx/xxxx
  ```

- Use the command `mkdir` to make a directory with your preferred folder name. Then use `cd` to get into this folder.

  ```powershell
  mkdir RaceGame
  cd RaceGame
  ```

- Now, let us create the project using `skm dotnet new console` and then use `skm dotnet restore` to initialize the settings. 

  ```
  skm dotnet new console
  skm dotnet restore
  ```

- Finally,  add the game resource files, using `skm resources`  to create the recourse folder, then click [here](https://github.com/PeiDeng/RaceGameTutorial/raw/master/files/Resources.rar) to download the game resource files and override the original folder.

------



## Iteration1

##### In this iteration, we will create the program and draw the basic images on the window. 

------

- Open your VS Code,  Open your program Folder.

- Create the Window to display our game.

  ```c#
  using System;
  using SplashKitSDK;
  
  public class Program
  {
      public static void Main()
      {
          Window window = new Window("RaceGame", 800, 600);
      }
  }
  ```

- Create a bitmap object using the road image in resource file

  ```c#
  Bitmap Road = new Bitmap("Road", "Road1.png");
  ```

- Draw this road bitmap on the window

  ```c#
  window.Clear(Color.RGBColor(193, 154, 107));
  Road.Draw((window.Width-Road.Width)/2, 0);
  window.Refresh();
  ```

  - Clear the window with the color of desert
  - Draw the road bitmap on the middle of window
  - Refresh the window to display any change.

- Now you can switch to the terminal to run your program

  ```powershell
  skm dotnet run	
  ```

  - You can see your program will be shown and disappear in 1 second.

- Use a while loop to keep the window display continuously

  ```c#
  while (!window.CloseRequested)
  {
  	SplashKit.ProcessEvents();
  	window.Clear(Color.RGBColor(193, 154, 107));
  	Road.Draw((window.Width - Road.Width) / 2, 0);
  	window.Refresh();
  }
  window.Close();
  window = null;
  ```

  - Now your program can continuously run until you close the window.

- The program in this step:![Road](https://i.imgur.com/0ANM4UO.png)

- Before we draw the car, we need to calculate the X value for each lane, so we use constants to store these figures: 

  ![WIDTH](https://i.imgur.com/XV0t3fV.jpg)

  ```
  const int LANE_LEFT = 250;
  const int LANE_WIDTH = 60;
  ```

- Create the Player object and draw it 

  ```C#
  Bitmap player = new Bitmap("Player","Playercar1.png");
  
  player.Draw(LANE_LEFT + LANE_WIDTH * 2, window.Height - player.Height);
  //we need to draw the player after we draw the road, because our player is upon the road.
  ```

- The ~~picture~~ program in this Step:

  ![Iteration1](https://i.imgur.com/dH4ao3y.png)

### [Final Code](https://github.com/PeiDeng/RaceGameTutorial/tree/master/code/Iteration1)



## Iteration2

##### In this iteration, we will let our car "keep moving"

##### It will look like:

![Iteration2](https://i.imgur.com/hov7lvE.gif)

------

- Actually, the car is **static**.  we just alternately display the different road images to make the car looks like being moving.

- First, we move our code to a new class `RaceGame`, and just use the program class as an entrance

  ```C#
  public class RaceGame
  {
      ....
  }
  ```

- In `Program` class, we just create a `RaceGame` object and call it do something.

  ```
  public class Program
  {
      public static void Main()
      {
          Window _window = new Window("RaceGame", 800, 800);
          RaceGame _raceGame = new RaceGame();
          
          while (!_window.CloseRequested)
          {
              SplashKit.ProcessEvents();
              _window.Clear(Color.RGBColor(193, 154, 107));
              ...
              _window.Refresh();
          }
          _window.Close();
          _window = null;
      }
  }
  ```

- In `RaceGame` class, declare these fields

  ```C#
  private Window _window;
  private Bitmap _player;
  private Bitmap _road;
  ```

- Add a method to load all images 

  ```C#
  public void LoadResource()
  {
      SplashKit.LoadBitmap("Player1", "PlayerCar1.png");
      SplashKit.LoadBitmap("Road1", "Road1.png");
      SplashKit.LoadBitmap("Road2", "Road2.png");
      SplashKit.LoadBitmap("Road3", "Road3.png");
      SplashKit.LoadBitmap("Cactus", "Cactus.png");
  }
  ```

- Now we write the constructor for `RaceGame` Class. The constructor is a method which will be run automatically when an object of this class is being created. 

- In the constructor, we need to use a parameter to pass in the window object which we create in the `Program` class and reference it to the `_window` field 

  ```
  public class RaceGame
  {
      public RaceGame(Window w)
      {
      	_window = w;
      }
  ```

- Meanwhile, when we create the `RaceGame` object we need to pass this parameter.

  ```
  public class Program
  {
      public static void Main()
      {
          Window _window = new Window("RaceGame", 800, 800);
          RaceGame _raceGame = new RaceGame(_window);
          .....
      }
  }
  ```

- Then call the `LoadResource()` method to load images, and using a `SplashKit` function `SplashKit.BitmapNamed()` to store the image into the variable `_player`.  

  ```
  public class RaceGame
  {
      public RaceGame(Window w)
      {
          _window = w;
          LoadResource();
          _player = SplashKit.BitmapNamed("Player1");
      }
  ```

  To swap the images regularly,  we add new  `Timer`  field to record the time

  ```C#
  private Timer _myTimer;
  ```

- Start recording time since program run (In constructor)

  ```
  _myTimer = new Timer("Timer");
  _myTimer.Start();
  ```

- Add a new method `RoadMove()` for swapping road mages

  - Use  `_myTimer.Ticks` to get the millisecond number

  - Use if statement to check the time and swap the image every 200ms

  ```C#
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
      	_myTimer.Start();         //reset time recording
      }
  }
  ```

- Add `Draw()`method and `Move()` method to refresh game and draw bitmap

  ```C#
  public void Draw()
  {
      _road.Draw((_window.Width - _road.Width) / 2, 0);
      _player.Draw(LANE_LEFT + LANE_WIDTH * 2, _window.Height - _player.Height);
  }
  
  public void Move()
  {
      RoadMove();
  }
  ```

- Run your program you will see your car is "moving"

- Now we add the cactus to the roadside to improve our "moving"

  ```c#
  private Bitmap _cactusBitmap;
  //
  _cactusBitmap = SplashKit.BitmapNamed("Cactus");
  ```

- We give a initial position to the cactus in constructor, and change it Y value to make it looks like being moving.

  ```C#
  private double _cactus1X;
  private double _cactus1Y;
  private double _cactus2X;
  private double _cactus2Y;
  public int CactusSpeed = 5;
  
  public RaceGame(Window window)
  {
      .....
      _cactus1X = LANE_LEFT - _cactusBitmap.Width - 5;
      _cactus1Y = 0;
      _cactus2X = LANE_LEFT + LANE_WIDTH * 5 + 5;
      _cactus2Y = -_window.Height / 2;
  }
  
  public void CactusMove()
  {
      _cactus1Y += CactusSpeed;		// make the cactus1 move down
      if (_cactus1Y >= _window.Height)
      {
      	_cactus1Y = 0;    //reset the Y value when it out of screen
      }
      	_cactus2Y += CactusSpeed;	// make the cactus2 move down
      if (_cactus2Y >= _window.Height)	
      {
      	_cactus2Y = 0;	 //reset the Y value when it out of screen
      }
  }
  ```

- Put the method into the `Update()` and then draw them in `Draw()`

  ```c#
  public void Update()
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
  ```

------

- In `Program` class, call the `Update()` and `Draw()`

  ```C#
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
  ```

------

### [Final Code](https://github.com/PeiDeng/RaceGameTutorial/tree/master/code/Iteration2/)



## Iteration3

##### In this iteration, we add the code for controlling our player car and sort our code with more reasonable classification. The UML is 

![UML1](https://i.imgur.com/W8P24Rt.png)

------

- First, create the `Map` class then put all codes about background into it.

  ```C#
  public class Map
  {
      private Bitmap _roadBitmap;
      //...
     	
      public Map(Window window)
      {
          _cactusBitmap = SplashKit.BitmapNamed("Cactus");
          _gameWindow = window;
          _myTimer = new Timer("timer");
          _myTimer.Start();
          _cactus1X = LANE_LEFT - _cactusBitmap.Width - 5;
          _cactus1Y = 0;
          _cactus2X = LANE_LEFT + LANE_WIDTH * 5 + 5;
          _cactus2Y = -_gameWindow.Height / 2;
      }
      
      public void Draw()
      {
          _roadBitmap.Draw((_gameWindow.Width - _roadBitmap.Width) / 2, 0);
          _cactusBitmap.Draw(_cactus1X, _cactus1Y);
          _cactusBitmap.Draw(_cactus2X, _cactus2Y);
      }
  
      public void Update()
      {
          RoadMove();
          CactusMove();
      }
          
      private void KeepMoving()
      {
          ...
      }
      
      private void CactusMove()
      {
          ...
      }
  }
  ```

- Create the `Player` class and move the codes about player into it

  ```C#
  public class Player
  {
      private Window _gameWindow;
      public Bitmap _CarBitmap;
  	
      public double X;
      public double Y;
  	// To control our player car,we need the X and Y value to change its position  
  }
  ```

- In the `Player()` constructor, we give it a initial position

  ```C#
  public Player(Window window)
  {
      CarBitmap = SplashKit.BitmapNamed("Player1");
      _gameWindow = window;
      X = Map.LANE_LEFT + Map.LANE_WIDTH * 2;
      Y = _gameWindow.Height - CarBitmap.Height;
  }
  ```

- Write a `HandleInput()` method to read the user input

  ```C#
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
  }
  ```

  - We change the X and Y values to make it move.

- Add the `Move()` method and `Draw()` method

  ```C#
  public void Move()
  {
      HandleInput();
  }
  
  public void Draw()
  {
      CarBitmap.Draw(X, Y);
  }
  ```

- We want to add a exit function, when user press the ESC Key, to close the program.

- Add a bool value to detect the user input

  ```C#
  public bool Quit { get; set; }
  //...
  
  if (SplashKit.KeyReleased(KeyCode.EscapeKey))
      {
          Quit = true;
      }
  ```

- Also, in `RaceGame` Class add a `ESC` property

  ```C#
  public bool ESC
  {
      get
      {
          return _player.Quit;
      }
  }
  ```

- Create the `_player` and `_map` object in `RaceGame` class and use the method `Update()`  and `Draw()`  to call them 

  ```
  private Player _player;
  private Map _map;
  
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
  ```

- Change the loop condition in `Program` class,  also call the `_raceGame` object to `Update()` and `Draw()`

  ```C#
  while (!_window.CloseRequested && !_raceGame.ESC)
  {
      SplashKit.ProcessEvents();
      _window.Clear(Color.RGBColor(193, 154, 107));
      _raceGame.Update();
      _raceGame.Draw();
      _window.Refresh();
  }
  ```

- Run your program, we could control our car to move.

- However, the car can be out of the track even out of the screen

- Now we add the `StayInTrack()` method to limit it

  ```C#
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
  ```

- Finally, put the `StayInTrack()`into the `Move()` method

------

### [Final Code](https://github.com/PeiDeng/RaceGameTutorial/tree/master/code/Iteration3/)



## Iteration4

##### In this Iteration, we would like to add a AI car, and we need to control our car to dodge it.

##### The AI class UML should be:

![UML2](https://i.imgur.com/o5KWu0R.png)

------

- Create the `AI` class. Similar to the cactus, we declare the bitmap, X, Y, and make it move

  ```C#
  public class AI
  {
      public Bitmap CarBitmap;
      public double X;
      public double Y;
      public double Speed = 3;
      
      public AI()
      {
          CarBitmap = SplashKit.BitmapNamed("AICar1");
          //Load bitmap resources in LoadResource()
          X = Map.LANE_LEFT + Map.LANE_WIDTH * 2;
          Y = -CarBitmap.Height;
      }
  
      public void Draw()
      {
          CarBitmap.Draw(X,Y);
      }
  
      public void Move()
      {
          Y += Speed;
      }
  }
  ```

  

- Update the `RaceGame` Class, create the `AI` object and call it `move()` and `draw()`,then run your program, you have a moving AI car like this:

![Iteration4-1](https://i.imgur.com/20o0U6u.gif)

- Now we use `SplashKit` function to write a method to detect the collision and return a bool value

  ```C#
  public bool ColliedWith(Player p)
  {
      return CarBitmap.BitmapCollision(X, Y, p.CarBitmap, p.X, p.Y);
  }
  ```

- Add a `Collision()`method in `RaceGame` class. When our player collide with the AI car, the game will be reset.

  - We could use  `SplashKit` to show a message box but we should load the font which is in the [Resource](files/) files

  ```C#
  public void LoadResource()
  {	
  	SplashKit.LoadFont("FontC", "calibri.ttf"); 
  }
  ```

  - Add a bool value `Restart`  assign it in the `Collision()`

  ```C#
  public bool Restart;
  
  public void Collision()
  {
      if (_ai.ColliedWith(_player))
      {
          SplashKit.DisplayDialog("GameOver", "GameOver", SplashKit.FontNamed("FontC"), 20);
          Restart = true;
      }
  }
  ```

  - use the `Restart` value in `Program` class to reset game

  ```C#
  while (!_window.CloseRequested && !_raceGame.ESC)
  {
      if (_raceGame.Restart)
      {
          _raceGame = new RaceGame(_window);
      }
      ...
  }
  ```

- Put the `Collision()` into `Update()` then run your program, it will look like

  ![Iteration4-2](https://i.imgur.com/TSHrYJZ.gif)

------

### [Final Code](https://github.com/PeiDeng/RaceGameTutorial/tree/master/code/Iteration4/)



## Iteration5

##### In this iteration, we will add more AI cars and make them emerge randomly.

##### it will look like this

![Iteration5](https://i.imgur.com/A5ljkD9.gif)

------

- First, we use a List to store these AI cars.

  ```C#
  using System.Collections.Generic;// we need to add this namespace to use List
  
  private List<AI> _ai = new List<AI>();
  ```

- We hope the cars could appear in all lanes, so we give them randomly initial `X` value in the `AI` constructor

  ```C#
  public AI()
  {
      CarBitmap = SplashKit.BitmapNamed("AICar1"); 
      Y = -CarBitmap.Height;
  
      double r = SplashKit.Rnd();//this will generate a random number from 0 to 1
      if (r < 0.2)
      {
          X = Map.LANE_LEFT;
      }
      if (r >= 0.2 && r < 0.4)
      {
          X = Map.LANE_LEFT + Map.LANE_WIDTH;
      }
      if (r >= 0.4 && r < 0.6)
      {
          X = Map.LANE_LEFT + Map.LANE_WIDTH * 2;
      }
      if (r >= 0.6 && r < 0.8)
      {
          X = Map.LANE_LEFT + Map.LANE_WIDTH * 3;
      }
      if (r >= 0.8)
      {
          X = Map.LANE_LEFT + Map.LANE_WIDTH * 4;
      }
  }
  ```

- In `RaceGame` class, we add car into the list for test.

  ```C#
  _ai.Add(new AI());
  ```

- Then we need to use the `foreach` loop to call all element in the list

  ```C#
  public void Update()
  {
      ....
      foreach (AI ai in _ai)
      {
      	ai.Move();
      }  
  }
   
  public void Draw()
  {
      ....
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
              ...
          }
      }
  }
  ```

- Run the program, the car will appear in different lane randomly.

- We need cars emerge continuously,  in this game, we generate a new car when the previous car is over 1/4 of the game window

  - add a Boolean value in `AI` class 

    ```C#
    public bool IsOverLine;
    ```

  - add the Boolean value `_addNew` and the  `CheckOverLine()` method

    ```C#
    private bool _addNew;
    
    public void CheckOverLine()
    {
        foreach (AI ai in _ai)
        {
            if (ai.IsOverLine != true && ai.Y > _window.Height/4)
            {
                _addNew = true;
                ai.IsOverLine = true;
            }
        }
    }
    ```

  - add a `AddNewCar() ` method to add the car to the list. 

    ```C#
    public void AddNewCar()
    {
        if (_addNew == true)
        {
            _ai.Add(new AI());
            _addNew = false;
        }
    }
    ```

- Then the `CheckOverLine()` and `AddNewCar() ` methods into the `Update()` and run your program

- So far, you can see your program works well but we also need to add an extra method to dispose useless cars.

  - we use a new list to store these useless cars

    ```C#
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
    }
    ```

  - then we remove all the cars in the useless list from the `_ai` list

    ```C#
    foreach (AI r in _uselessAI)
    {
        _ai.Remove(r);
    }
    ```

- Finally, call the `RemoveAI()` method in `Update()`

------

### [Final Code](https://github.com/PeiDeng/RaceGameTutorial/tree/master/code/Iteration5/)



## Iteration6

##### In this Iteration, we will use inheritance to add more car types and we add an interface for all movable class, the UML is

![UML3](https://i.imgur.com/yhYLTy1.png)

------

- First, we load the images of other cars in `LoadResource() `

- Change the AI class to an  abstract class and then create the child classes for different AI car types.

  ```C#
  public abstract class AI
  {}
  public class AICar1 : AI
  {}
  public class AICar2 : AI
  {}
  public class AICar3 : AI
  {}
  ```

- Meanwhile, add an interface `IMovable`  and make all movable to inherit it

  ```C#
  public interface IMovable
  {
      void Move();
  }
  
  public class Map : IMovable{}
  public class Player : IMovable{}
  public abstract class AI : IMovable{}
  ```

- The bitmaps of the three AI cars are different, so we initialize the bitmap in the child constructor which will be run follow the base constructor. 

  ```C#
  public class AICar1 : AI
  {
      public AICar1()
      {
          CarBitmap = SplashKit.BitmapNamed("AICar1");
          Y = -CarBitmap.Height; //The Y value is associate with the bitmap
      }
  }
  ```

- The move speed is different between these cars, so we change the `Move()` in base class to a virtual method, and use the methods in child class to override it.

  ```C#
  public abstract class AI
  {
      public virtual void Move() { }
  }
  
  public class AICar1 : AI
  {
      public override void Move()
      {
      	Y += Speed;
      }
  }
  
  public class AICar2 : AI
  {
      public override void Move()
      {
          Y += Speed /3 * 4;
      }
  }
  
  public class AICar3 : AI
  {
      public override void Move()
      {
          Y += Speed / 3 * 5;
      }
  }
  ```

- In `RaceGame` class, we create a method to randomly add these three types of car into the list

  ```C#
  public void RandomAI()
  {
      double rnd = SplashKit.Rnd();
      if (rnd > 0.4)
      {
          _ai.Add(new AICar1());
      }
      if (rnd <= 0.4 && rnd > 0.2)
      {
          _ai.Add(new AICar2());
      }
      if (rnd <= 0.2)
      {
          _ai.Add(new AICar3());
      }
  }
  ```

- Use the `RandomAI()` method to generate cars in `AddNewCar() ` and `RaceGame` constructor.

- Run your program, your will see the three types of car.

- However, there is a problem here. The different AI cars will  crash each others if they are in a same lane since they have different speeds.

- To solve this problems, we need to ensure that every lane only has one AI cars at the same time.

  - We add a `Lane` variable in AI class and initialize it in constructor. This variable could indicate indicate which lane each car is in.

    ```C#
    public int Lane;
    
    public Car()
    {
        double r = SplashKit.Rnd();
        if (r < 0.2)
        {
            X = Map.LANE_LEFT;
            Lane = 1;
        }
        if (r >= 0.2 && r < 0.4)
        {
            X = Map.LANE_LEFT + Map.LANE_WIDTH;
            Lane = 2;
        }
        ....
    }
    ```

  - The we ad a Boolean array to represent is there a car in each lane.

    ```C#
    private bool[] _lane = new bool[5];
    ```

  - A method to update the `_lane[]` value

    ```C#
    public void LaneStatus()
    {
        foreach (AI ai in _ai)
        {
            _lane[ai.Lane - 1] = true;
        }
    }
    ```

  - In `RemoveAI()` method, change the `_lane[]` value to false when a car is removed.

  - Now, the system can know which lane is empty. We could add a while loop in `RandomAI()` method to check the lane. If the lane of the new car is not empty, we withdraw the new car and generate another car until it is in the empty lane.

  - We could add a method to check whether the car is in the empty lane

    ```C#
    public bool CheckLane(AI ai)//the method will return a bool value
    {
        if (_lane[ai.Lane - 1])
        {
            ai = null;	//withdraw this car
            return false;  
        }
        return true;
    }
    ```

  - Use the method as a condition in the while loop in `RandomCar()`

    ```C#
    public void RandomAI()
    {
        bool rightLane = false;
        while (!rightLane)
        {
            double rnd = SplashKit.Rnd();
            if (rnd > 0.4)
            {
                AI newAI = new AICar1();
                if (CheckLane(newAI))
                {
                    _ai.Add(newAI);
                    rightLane = true;
                }
            }
            if (rnd <= 0.4 && rnd > 0.2)
    		....
    		....
        }
    }
    ```

- Run your program, it will look like this

  ![Iteration5](https://i.imgur.com/zcmfoKG.gif)

------

### [Final Code](https://github.com/PeiDeng/RaceGameTutorial/tree/master/code/Iteration6/)



## Iteration7

##### In this iteration, we add score and level to the game. The game will become harder with the level increases. In addition, we add a reward to make the game more interesting.

##### It will look like

![Iteration7](https://i.imgur.com/lbA8ynF.gif)

------

- First, we add the `_score` that increases base on the moving distance and the `_level` that increases base on time.

  ```C#
  private Timer _timer;
  private int _score;
  private int _level;
  
  public RaceGame(Window w)
  {
      ....
      _timer = new Timer("gameTimer");
      _timer.Start();
  }
  public void Level()
  {
      _score += 3;
      _level = Convert.ToInt32(_timer.Ticks) / 10000;
  }
  ```

- Add a method to show the score and level.

  ```C#
  public void DrawUI()
  {
      _window.DrawText($"Your Score: {_score} M", Color.Black, SplashKit.FontNamed("FontU"), 		20, 20, 20); //Load font resource in LoadResource()
      _window.DrawText($"Level {_level}", Color.Black, SplashKit.FontNamed("FontJ"), 40, 20, 		250);	//Load font resource in LoadResource()
  }
  ```

  - Change the `Collision()`method to show the score when game is over.

- Now, we hope this game become harder as `_level` increase.

- We add a `_basicSpeed` variable and make it increase with `_level`, also, the `_score` increases with it.

  ```C#
   public void Level()
  {
      _basicSpeed = 3 + _level;
      _score += _basicSpeed;
      _level = Convert.ToInt32(_timer.Ticks) / 10000;
  }
  ```

- Now we add a method to set the speed of all AI.

  ```C#
  public void SetSpeed()
  {
      _map.CactusSpeed = _basicSpeed + 2; // Do not forget the cactus
      foreach (AI ai in _ai)
      {
          ai.Speed = _basicSpeed;
      }
  }
  ```

- Run the program, the AI will become faster and faster as time flies.

- Now, let us make the game more interesting! We could add a reward between the AI cars, when our player hit it. we will get double speed.

- First, create the `Reward1` class

  ```C#
  public class Reward1 : AI, IMovable
  {
      public Reward1()
      {
          CarBitmap = SplashKit.BitmapNamed("Reward1"); // Load the bitmap resource in advance
          Y = -CarBitmap.Height;
      }
  
      public override void Move()
      {
          Y += Speed * 2;
      }
  }
  ```

  - Change the `RandomAI()` method to generate it randomly.

- When the player collide with the reward, we double the `_basicSpeed` for 10 seconds. During the double speed period, if the player collide the reward again. we add more 10 seconds for that. 

- To achieve that, we need a variable to store the reward time and a Boolean value for `CheckReward()`

  ```C#
  private uint _doubleSpeedTime;
  private bool _reward1;
  
  public void CheckReward()
  {
      _reward1 = (_timer.Ticks < _doubleSpeedTime); // check whether in reward period or not
  }
  ```

- Then change the `Collision() ` method 

  ```C#
  public void Collision()
  {
      foreach (AI ai in _ai)
      {
          if (ai.ColliedWith(_player))
          {
              if (SplashKit.BitmapName(ai.CarBitmap) == "Reward1") 
                  							//To check whether the player hit the reward.
              {
                  if (!_reward1) //if not in reward period
                  {
                      _doubleSpeedTime = _timer.Ticks + 10000; // set the reward period
                  }
                  else
                      _doubleSpeedTime += 10000; // extend the reward period
              }
              else
              {
                  ... // Game Over
              }
          }
      }
  }
  ```

- Finally, change the `_basicSpeed` in `SetSpeed()` method.

  ```C#
  public void SetSpeed()
  {
      if (_reward1)
      {
          _map.CactusSpeed = (_basicSpeed + 2) * 2;
          foreach (AI ai in _ai)
          {
              ai.Speed = _basicSpeed * 2;
          }
      }
      else
      {
      	.... 
      }
  }
  ```

------

### [Final Code](https://github.com/PeiDeng/RaceGameTutorial/tree/master/code/Iteration7/)



## Iteration8

##### In this iteration, we are going to add a hot key to swap our player bitmap. Then we will add another reward which could make our player become invincible. 

##### It will look like: 

![Iteration8](https://i.imgur.com/XeJCotA.gif)

------

- For swapping car, we need a method to swap the bitmap.

  - We write a delegate to get the current player bitmap

    ```C#
    public delegate string GetBitmapName(Bitmap bitmap);
    
    GetBitmapName PlayerBitmap = delegate (Bitmap bitmap)
    {
    	return SplashKit.BitmapName(bitmap);
    };
    ```

  - then we pass the delegate as a parameter to the `SwapPlayer( )` method

    ```C#
    public void SwapPlayer(Bitmap p, GetBitmapName getname)
    {
        if (getname(p) == "Player1")
        {
            CarBitmap = SplashKit.BitmapNamed("Player2"); // Remember loading resource
        }
        if (getname(p) == "Player2")
        {
            CarBitmap = SplashKit.BitmapNamed("Player1");
        }
    }
    ```

  - Then call this method when we press the key

    ```C#
    public void HandleInput()
    {
        ...
        if (SplashKit.KeyReleased(KeyCode.LeftCtrlKey))
        {
            SwapPlayer(CarBitmap, GetBitmap);
        }
    }
    ```

  - Run your program and try the swap key.

- Now, let us add the invincible reward.

- Similar to the double speed reward, we create the `Reward2` class and add it into `RandomAI()`.

- We use the same approach as `Reward1` to check the collision and then we could change the game over condition. 

  ```C#
  public void Collision()
  {
      foreach (Car car in _car)
      {
          if (car.ColliedWith(_player))
          {
              .....
              else if (SplashKit.BitmapName(ai.CarBitmap) == "Reward2")
              {
                  if (!_reward2)
                  {
                      _invincibleTime = _timer.Ticks + 10000;
                  }
                  else
                      _invincibleTime += 10000;
              }
              else if (!_reward2 && SplashKit.BitmapName(ai.CarBitmap) != "Reward1")
              {
                  SplashKit.DisplayDialog("GameOver", $"Your Score is: {_score} m", SplashKit.FontNamed("FontC"), 20);
                  Restart = true;
              }
          }
      }
  }
  ```

- Besides this, we want to swap the bitmap if we get this effect of invincibility.

  - add a method which is similar to the `SwapPlayer( )` method.

    ```C#
    public delegate string GetBitmapName(Bitmap bitmap);
    
    public void InvincibleBitmap(Bitmap player, GetBitmapName getname)
    {
        if (_reward2)  //Change the bitmap if in reward2 period
        {
            if (getname(player) == "Player1")
            {
                _player.CarBitmap = SplashKit.BitmapNamed("Player1S");
            }
            if (getname(player) == "Player2")
            {
                _player.CarBitmap = SplashKit.BitmapNamed("Player2S");
            }
        }
        else   // restore the bitmap
        {
            if (getname(player) == "Player1S")
            {
                _player.CarBitmap = SplashKit.BitmapNamed("Player1");
            }
            if (getname(player) == "Player2S")
            {
                _player.CarBitmap = SplashKit.BitmapNamed("Player2");
            }
        }
    }
    ```

  - call this method in `CheckReward()`, and this time, we use the lambda expression to pass the delegate

  - Let us see how to simplify a delegate to lambda expression

    - This is the original code 

      ```C#
      GetBitmapName PlayerBitmap = delegate (Bitmap bitmap)
      {
      	return SplashKit.BitmapName(bitmap);
      };
      ```

    - We know that the both side of an equal sign are same. So we could use code on the left of the sign like this.

      ```C#
      InvincibleBitmap(_player.CarBitmap, delegate (Bitmap bitmap){return SplashKit.BitmapName(bitmap);}) 
      ```

    - Here, we could use `=>` to replace the `delegate`.

      ```C#
      InvincibleBitmap(_player.CarBitmap, (Bitmap bitmap) => {return SplashKit.BitmapName(bitmap);})
      ```

    - Since we already declare data type when we declare the delegate, so we could omit the `Bitmap`

      ```C#
      InvincibleBitmap(_player.CarBitmap, (bitmap) => {return SplashKit.BitmapName(bitmap);})
      ```

    - For this case, we only have one variable in `()` , therefore, we could omit the `()`. Also, there is only one sentence in `{}` so we could omit the `{}` and `return`

      > *the () only can be omitted when there is only one variable in (). Same as {}*.

    - Therefore, the final lambda expression is 

      ```C#
      InvincibleBitmap(_player.CarBitmap, bitmap => SplashKit.BitmapName(bitmap))
      ```

  - Revise the `SwapPlayer()` method to make sure we can swap the player car during the invincible period.

- Now we add an extra function for invincibility

  - We hope our player car to twinkle in the last 3 seconds of the invincible period. 

  - Write a method for draw player conditionally.

    ```C#
    public void PlayerTwinkle()
    {
        if (_invincibleTime - _timer.Ticks > 700 && _invincibleTime - _timer.Ticks < 1000)
        { }
        else if (_invincibleTime - _timer.Ticks > 1700 && _invincibleTime - _timer.Ticks < 2000) 
        { }
        else if (_invincibleTime - _timer.Ticks > 2700 && _invincibleTime - _timer.Ticks < 3000)
        { }
        else
        {
            _player.Draw();
        }
    }
    ```

- Modify the `AddNewCar()` to avoid the situation that there is no car in each Lane since we eliminate them during invincible period.

  ```C#
  public void AddNewCar()
  {
  	....
  	else if (!(_lane[0] || _lane[1] || _lane[2] || _lane[3] || _lane[4]))
      {
          RandomAI();
      }
  }
  ```

- Finally, improve the user interface 

  ```C#
  public void DrawUI()
  {
      _window.DrawText($"Your Score: {_score} M", Color.Black, SplashKit.FontNamed("FontU"), 20, 20, 20);
      _window.DrawText($"Level {_level}", Color.Black, SplashKit.FontNamed("FontJ"), 40, 20, 250);
      _window.DrawBitmap(SplashKit.BitmapNamed("Key"), 0, 400);
      //load the keyboard instructions bitmap
  
      _window.DrawBitmap(SplashKit.BitmapNamed("Reward1"), 600, 150);
      _window.DrawText("SpeedUp", Color.Black, SplashKit.FontNamed("FontJ"), 20, 660, 210);
      if (_reward1)
          _window.DrawText($"Time left: {(_doubleSpeedTime - _timer.Ticks) / 1000} s", Color.Red, SplashKit.FontNamed("FontU"), 20, 660, 250);
  	// display the remaining time of reward1
      
      _window.DrawBitmap(SplashKit.BitmapNamed("Reward2"), 600, 300);
      _window.DrawText("Invinciable", Color.Black, SplashKit.FontNamed("FontJ"), 20, 660, 360);
      if (_reward2)
          _window.DrawText($"Time left: {(_invincibleTime - _timer.Ticks) / 1000} s", Color.Red, SplashKit.FontNamed("FontU"), 20, 660, 400);
  }
  	// display the remaining time of reward2
  ```

------

### [Final Code](https://github.com/PeiDeng/RaceGameTutorial/tree/master/code/Iteration8/)

