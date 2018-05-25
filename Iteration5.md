# Iteration5

##### In this iteration, we will add more AI cars and make them emerge randomly.

##### it will look like this

![Iteration5](/img/6.gif)

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

# [Final Code](code/Iteration5)