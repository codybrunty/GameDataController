# GameDataController

The GameDataControler is used for storing persistent data for games in Unity. 
The controler writes a binary file and that file is stored on the users device.
This binary file can then be loaded into the controler and referenced while
the user plays the game. 

This is great for storing persistent data over multiple play throughs. I currently
use it to store progress on a puzzle game I have created. The user can log into the
game and their level progress and coin achievements are all right where they left off.

To get this C# script to work in Unity the user just has to attach it to an empty 
GameObject. After you have done that when you press play the GameObject will write a
new GameData file to your device where it will reference in the future.

In other scripts you can now call this GameDataControler script since it is static with
"GameDataControler.gdControl" you can call different variables for example "coinsEarned"
by saying "GameDataControler.gdControl.coinsEarned" these variables you have set up in the
GameDataControler can be modified and changed as the player hits specific milestones in
your game. Once a variable is changed make sure to run "GameDataControler.gdControl.SaveGameData()"
to save any game data in the binary file.

Thank You and have fun
Cody Brunty
