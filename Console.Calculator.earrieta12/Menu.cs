

namespace MyFirstProgram
{
    internal class Menu
    {
        GameEngine gamesClass = new();

        internal void ShowMenu(string name, DateTime date, List<int> difficulty)
        {

            var isGameOn = true;
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"Hello {name.ToUpper()}. It's {date}. This is your math's game. That's great that you're working on improving yourself\n");
            do
            {
                Console.WriteLine(@$"What game would you like to play today? Choose from the options below:
1 - Addition
2 - Subtraction
3 - Multiplication
4 - Division
5 - Previous Games
6 - Random Game
7 - Select Difficulty
9 - Quit the program");
                Console.WriteLine("---------------------------------------------");

                var gameSelected = Console.ReadLine();

                //Select case Menu
                switch (gameSelected.Trim().ToLower())
                {
                    case "1":

                        gamesClass.AdditionGame(difficulty, name);
                        break;

                    case "2":

                        gamesClass.SubstractionGame(difficulty, name);
                        break;
                    case "3":
                        gamesClass.MultiplicationGame(difficulty, name);
                        break;

                    case "4":

                        gamesClass.DivisionGame(difficulty, name);
                        break;

                    case "9":

                        Console.WriteLine("Goodbye");
                        Environment.Exit(1);
                        isGameOn = false;
                        break;

                    case "5":

                        Console.WriteLine("Previous Games");
                        gamesClass.PreviousGame();
                        break;


                    case "6":

                        gamesClass.RandomGames(difficulty, name);
                        break;


                    case "7":

                        difficulty = Helper.GameDifficulty();

                        break;


                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }
            } while (isGameOn);
        }
    }
}
