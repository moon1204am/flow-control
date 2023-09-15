using System.Text.RegularExpressions;

namespace FlowExercise
{
    internal class Program
    {
        enum AgeGroup
        {
            Adolescent = 80,
            Pensioner = 90,
            Regular = 120,
            Free = 0
        }

        const int minLengthSentence = 3;

        static void Main(string[] args)
        {
            bool keepShowingCommands = true;
            while (keepShowingCommands)
            {
                ShowOptions();
                string command = Console.ReadLine();

                switch (command)
                {
                    case "0":
                        keepShowingCommands = false;
                        break;
                    case "1":
                        AdolescentOrOPensioner();
                        break;
                    case "2":
                         GetPriceForFullCompany();
                        break;
                    case "3":
                        RepeatTenTimes();
                        break;
                     case "4":
                        TheThirdWord();
                        break;
                    default:
                        Console.WriteLine("Illegal command. Try again.");
                        break;
                }
            }
        }

        /// <summary>
        /// Displays the options to the user.
        /// </summary>
        public static void ShowOptions()
        {
            Console.WriteLine("Please enter a command.");
            Console.WriteLine("[1] Adolescent or pensioner");
            Console.WriteLine("[2] Price for whole company");
            Console.WriteLine("[3] Repeat 10 times");
            Console.WriteLine("[4] The third word");
            Console.WriteLine("[0] Quit");
        }

        /// <summary>
        /// Gets the price for the person given an age.
        /// </summary>
        private static int AdolescentOrOPensioner(int index = -1, bool isCompany = false)
        {
            string displayMsg;
            if(isCompany)
            {
                displayMsg = $"Please enter age for person {index + 1}:";
            }
            else
            {
                displayMsg = $"Please enter your age:";
            }
            
            int age = ShowCommandAndGetUserInput(displayMsg);

            return CheckAgeAndGetPrice(age, isCompany);

        }

        /// <summary>
        /// Gets the price for a full company, given their ages.
        /// </summary>
        private static void GetPriceForFullCompany()
        {
            string displayMsg = "How many persons are going to the cinema?";
            int nrOfPeople = ShowCommandAndGetUserInput(displayMsg);

            int sum = 0;
            for (int i = 0; i < nrOfPeople; i++)
            {
                sum += AdolescentOrOPensioner(i, true);
            }

            Print($"\nNr of people: {nrOfPeople}\nTotal price: {sum} SEK\n");
        }

        /// <summary>
        /// Takes a text as input and then prints it ten times on the same line.
        /// </summary>
        private static void RepeatTenTimes()
        {
            Print("Enter an arbitrary text");
            string text = GetInput();
            while (!ValidateText(text))
            {
                text = InvalidInputForText();
            }
            for(int i = 0; i < 10; i++) 
            {
                Print2($"{i + 1}. {text} ");
            }
            Print("");
        }

        /// <summary>
        /// Takes a user input sentence and displays the third word of that sentence.
        /// </summary>
        private static void TheThirdWord()
        {
            string displayMsg = "Enter a sentence:";
            string userInput = GetUserInput(displayMsg);
            userInput = FormatString(userInput);

            while (!CheckLength(userInput))
            {
                displayMsg = "Sentence needs to be at least 3 words long. Try again.";
                userInput = GetUserInput(displayMsg);
                userInput = FormatString(userInput);
            }
            string[] words = userInput.Split(' ');
            Print($"\nThe third word: {words[2]}\n");

        }

        /// <summary>
        /// Finds the correct price for the corresponding age.
        /// </summary>
        /// <param name="age">The age of the person.</param>
        /// <param name="isCompany">If there is more than one person.</param>
        /// <returns>The price for this age.</returns>
        private static int CheckAgeAndGetPrice(int age, bool isCompany)
        {

            int ageTypePrice;
            if (age < 5 || age > 100)
            {
                ageTypePrice = (int)AgeGroup.Free;
            }
            else if (age < 20)
            {
                ageTypePrice = (int)AgeGroup.Adolescent;
            }
            else if (age > 64)
            {
                ageTypePrice = (int)AgeGroup.Pensioner;
            }
            else
            {
                ageTypePrice = (int)AgeGroup.Regular;
            }

            if (!isCompany && ageTypePrice != 0)
            {
                Print($"{(AgeGroup) ageTypePrice} price: {ageTypePrice} SEK.\n");
            }
            else if (!isCompany && ageTypePrice == 0)
            {
                Print($"Children under 5 and centenarians: Free entrance.\n");
            }

            return ageTypePrice;
        }

        /// <summary>
        /// Removes all whitespaces in a string and keeps the ones between each word inside the string.
        /// </summary>
        /// <param name="s">The string to format.</param>
        /// <returns>The formatted string.</returns>
        private static string FormatString(string s)
        {
            s = Regex.Replace(s, @"\s+", " ");
            return s.Trim();
        }

        /// <summary>
        /// Checks if the length of the user input sentence is long enough.
        /// </summary>
        /// <param name="userSentence">The sentence to be checked.</param>
        /// <returns>true if it's long enough, otherwise false.</returns>
        private static bool CheckLength(string userSentence)
        {
            string[] words = userSentence.Split(' ');
            if (words.Length < minLengthSentence)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Displays the command for the user, validates the string input and returns it.
        /// </summary>
        /// <param name="message">The command for the use.r</param>
        /// <returns>The validated input.</returns>
        private static string GetUserInput(string message)
        {
            Print(message);
            string input = GetInput();
            while (!ValidateText(input))
            {
                input = InvalidInputForText();
            }
            return input;
        }

        /// <summary>
        /// Displays the command for the user, validates the inputs that are expected to be integers and returns it.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static int ShowCommandAndGetUserInput(string message)
        {
            Print(message);
            string input = GetInput();

            int number = ValidateNumber(input);
            while(number == -1) 
            {
                number = InvalidInputForNumber();
            }
            return number;

        }

        /// <summary>
        /// Validator that checks if the string contained only numbers, and if that number is in a range.
        /// </summary>
        /// <param name="num">the string number to be validated.</param>
        /// <returns>the parsed number from string to int.</returns>
        private static int ValidateNumber(string? num)
        {
            if(!int.TryParse(num, out int parsedNum)) 
            {
                Console.WriteLine("Not a number.\n");
                return -1;
            }

            if(parsedNum <= 0 || parsedNum > 120)
            {
                Console.WriteLine("Enter a valid number.\n");
                return -1;
            }

            return parsedNum;
        }

        /// <summary>
        /// Validator to check if a string is null or empty.
        /// </summary>
        /// <param name="text">the string text to be checked.</param>
        /// <returns>true if not null or empty, otherwise false.</returns>
        private static bool ValidateText(string text)
        {
            return !string.IsNullOrEmpty(text);
        }

        /// <summary>
        /// If the input failed the validation, it displays it to the user and asks for input again.
        /// </summary>
        /// <returns>the new input by user.</returns>
        private static int InvalidInputForNumber()
        {
            Print("Try again:");
            string input = GetInput();
            return ValidateNumber(input);
        }

        /// <summary>
        /// If the input failed the validation, it displays it to the user and asks for input again.
        /// </summary>
        /// <returns>the new input by user.</returns>
        private static string InvalidInputForText()
        {
            Print("Can't be empty.");
            string sentence = GetInput();

            return sentence;
        }

        /// <summary>
        /// Prints to the console on new line.
        /// </summary>
        /// <param name="message">The message to be printed.</param>
        private static void Print(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Prints to the console on same line.
        /// </summary>
        /// <param name="message">The message to be printed.</param>
        private static void Print2(string message)
        {
            Console.Write(message);
        }

        /// <summary>
        /// Reads from the console.
        /// </summary>
        /// <returns>the input from the console.</returns>
        private static string GetInput()
        {
            return Console.ReadLine();
        }
    }
}