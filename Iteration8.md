# Iteration8

##### In this iteration, we are going to add a hot key to swap our player bitmap. Then we will add another reward which could make our player become invincible. 

##### It will look like: 

![Iteration8](/img/9.gif)

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

    -  We know that the both side of an equal sign are same. So we could use code on the left of the sign like this.

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

# [Final Code](code/Iteration8)