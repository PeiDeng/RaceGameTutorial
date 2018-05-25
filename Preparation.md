



# Preparation

This is a tutorial to make a C# Game with **SplashKit**.

Before starting, the below tools need to be installed:

------

1. [Microsoft .NET framework](https://www.microsoft.com/net/download)
2. [Visual Studio Code](https://code.visualstudio.com/ )
3. [SplashKit](http://www.splashkit.io/articles/installation/ )

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

- Finally,  add the game resource files, using `skm resources`  to create the recourse folder, then click [here](/files/resources.rar) to download the game resource files and override the original folder.




