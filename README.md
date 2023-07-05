# BattleshipGameChallenge
A simple game based on Battleship Board game. 

Game can be started by either runing it in Visual Studio or going to executables folder and runing exe file from there.

After starting the game grid is presented with gery fields. After clicking the field there are possible a few outcomes:
"X" means missed shot,
"#" means hit, additionaly if few fields in a row or column gets red borders it means entire ship has been sunk.

After sinking all the ships the game can be started over agian or closed.


Technical part!
Solution contains 3 main projects: BattleshipGameCore which is the main part, BattleshipUI which contains a very rough and simple UI and Battleship.Test which as name suggest contains unit test for core part.
