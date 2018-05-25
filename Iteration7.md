# Iteration7

##### In this iteration, we add score and level to the game. The game will become harder with the level increases. In addition, we add a reward to make the game more interesting.

##### It will look like

![Iteration7](/img/8.gif)

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

# [Final Code](code/Iteration7)