using MyFirstProgram;

var menu = new Menu();
var date = DateTime.UtcNow;
string name = Helper.GetName();
List<int> difficulty = Helper.gameDifficulty();
menu.ShowMenu(name, date, difficulty);













