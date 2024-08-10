using System.Text;

namespace SimpleMenuLibrary
{
    public class Menu(string title = "No Title")
    {
        public struct Option(string description, string selector)
        {
            public readonly string Description = description;
            public readonly string Selector = selector;
        }

        private readonly List<Option> _options = [];

        private static readonly int ConsoleWidth = Console.WindowWidth;
        private readonly int TotalPaddingLength = (ConsoleWidth / 2) + (title.Length / 2);
        private const string MenuBorder = "|";
        private const int InsideMarginWidth = 2;
        private const int OutsideMarginWidth = 6;
        private readonly string _outsideMargin = new string(' ', OutsideMarginWidth);
        private readonly string _insideMargin = new string(' ', InsideMarginWidth);

        public void AddMenuOption(Option menuOption)
        {
            _options.Add(menuOption);
        }

        public void ShowMenu(bool clear = true, string optionDelimiter = "",
        List<string>? footerContent = null)
        {
            if (clear)
                Console.Clear();

            var menuTitle = new StringBuilder();
            menuTitle.AppendFormat($"{_outsideMargin}");
            menuTitle.AppendFormat($"".PadLeft(TotalPaddingLength - OutsideMarginWidth, '='));
            menuTitle.AppendFormat($"\n");
            menuTitle.AppendFormat($"{_outsideMargin}");
            menuTitle.AppendFormat($"{title}".PadLeft(ConsoleWidth - menuTitle.Length).ToUpper());
            menuTitle.AppendFormat($"\n");
            menuTitle.AppendFormat($"{_outsideMargin}");
            menuTitle.AppendFormat($"".PadLeft(TotalPaddingLength - OutsideMarginWidth, '='));
            menuTitle.AppendFormat($"\n");
            Console.Write(menuTitle);

            foreach (var option in _options)
            {
                var menuLine = new StringBuilder();
                menuLine.AppendFormat($"{_outsideMargin}");
                menuLine.AppendFormat(MenuBorder);
                menuLine.AppendFormat($"{_insideMargin}");
                menuLine.AppendFormat($"{option.Selector}{optionDelimiter}");
                menuLine.AppendFormat(new string(' ', 20 -
                    option.Selector.Length - optionDelimiter.Length));
                menuLine.AppendFormat($"{option.Description}");
                menuLine.AppendFormat($"".PadLeft(TotalPaddingLength - menuLine.Length - 1));
                menuLine.AppendFormat(MenuBorder);
                menuLine.AppendFormat($"{_outsideMargin}");
                menuLine.AppendFormat("\n");
                Console.Write(menuLine);

            }
            ShowFooter(footerContent);
        }

        private void ShowFooter(string content = "")
        {
            var footer = new StringBuilder();
            footer.AppendFormat($"{_outsideMargin}");
            footer.AppendFormat($"".PadLeft(TotalPaddingLength - OutsideMarginWidth, '-'));
            footer.AppendFormat($"{_outsideMargin}");
            footer.AppendFormat($"\n");
            footer.AppendFormat($"{_outsideMargin}{_insideMargin}{content}{_outsideMargin}\n");
            footer.AppendFormat($"{_outsideMargin}");
            footer.AppendFormat("".PadRight(TotalPaddingLength - OutsideMarginWidth, '-'));
            Console.WriteLine(footer.ToString());
        }

        private void ShowFooter(List<string> content)
        {
            var footer = new StringBuilder();
            footer.AppendFormat($"{_outsideMargin}");
            footer.AppendFormat($"".PadLeft(TotalPaddingLength - OutsideMarginWidth, '-'));
            footer.AppendFormat($"{_outsideMargin}");
            footer.AppendFormat($"\n");
            foreach (string item in content)
            {
                footer.AppendFormat($"{_outsideMargin}{_insideMargin}{item}{_outsideMargin}\n");
            }
            footer.AppendFormat($"{_outsideMargin}");
            footer.AppendFormat("".PadRight(TotalPaddingLength - OutsideMarginWidth, '-'));
            Console.WriteLine(footer.ToString());
        }

        public string? Prompt(string promptText = "Enter Selection:", bool checkEnabled = false)
        {
            string? input = null;
            var prompt = new StringBuilder();
            prompt.AppendFormat($"{_outsideMargin}{_insideMargin}");
            prompt.AppendFormat(promptText);

            if (checkEnabled)
            {
                while (!CheckInput(input ??= "no input"))
                {
                    Console.Write($"{prompt}".ToUpper());
                    input = Console.ReadLine();
                }
            }
            else
            {
                Console.Write($"{prompt}".ToUpper());
                input = Console.ReadLine();
            }

            return input?.ToLower();
        }

        public List<Option> GetMenuOptions()
        {
            return _options;
        }
        private bool CheckInput(string? input)
        {
            return input != null && _options.Any(option => option.Selector == input);
        }
    }
}