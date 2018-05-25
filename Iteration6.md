# Iteration6

##### In this Iteration, we will use inheritance to add more car types and we add an interface for all movable class, the UML is

![UML3](/img/uml3.png)

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

  ![Iteration5](/img/7.gif)

------

# [Final Code](code/Iteration6)