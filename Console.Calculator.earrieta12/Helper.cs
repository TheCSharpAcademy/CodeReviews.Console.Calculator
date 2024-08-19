namespace MyFirstProgram
{

    internal class Helper
    {
        internal static List<Game> games = new List<Game>();

        internal static string GetName()
        {
            Console.WriteLine("What is your name?");
            var name = Console.ReadLine();

            while (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Name can't be empty");
                name = Console.ReadLine();
            }

            return name;
        }

        internal static List<int> GameDifficulty()
        {

            List<int> difficulty = new List<int>();
            Console.Clear();
            Console.WriteLine(@$"Select a difficult level. Choose from the options below:
1 - Easy
2 - Medium
3 - Impossible");

            var gameDifficulty = Console.ReadLine();
            if (gameDifficulty.Trim() == "1")
            {
                difficulty = new List<int> { 9, 5 };

            }
            else if (gameDifficulty.Trim() == "2")
            {
                difficulty = new List<int> { 99, 10 };
            }
            else if (gameDifficulty.Trim() == "3")
            {
                difficulty = new List<int> { 999, 15 };
            }
            else
            {
                Console.WriteLine("Invalid input");
                Console.WriteLine("Your difficult will be easy. You can select the difficulty again. Press any key to continue");
                Console.ReadLine();
                difficulty = new List<int> { 9, 5 };

            }

            return difficulty;


        }
        internal static void AddToHistory(int gameScore, string gameType, string name, string time)
        {


            games.Add(new Game
            {
                Date = DateTime.Now,
                Score = gameScore,
                Type = gameType,
                Name = name,
                Time = time
            });

        }


        internal class Game
        {

            public DateTime Date { get; set; }

            public int Score { get; set; }

            public string Type { get; set; }

            public string Name { get; set; }

            public string Time { get; set; }
        }

        internal static void PrintGames()
        {

            Console.Clear();
            Console.WriteLine("Games History");
            Console.WriteLine("---------------------------");
            foreach (var game in games)
            {
                Console.WriteLine($"{game.Date} - {game.Type}: {game.Score}pts, {game.Time} seconds");
            }
            Console.WriteLine("---------------------------\n");
            Console.WriteLine("Press any key to return to Main Menu");
            Console.ReadLine();
        }
    }


}
