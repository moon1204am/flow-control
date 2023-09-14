﻿using static System.Net.Mime.MediaTypeNames;

namespace FlowExercise
{
    internal class Program
    {
        enum AgeGroup
        {
            Adolescent = 80,
            Pensioner = 90,
            Regular = 120
        }

        static void Main(string[] args)
        {
            bool keepShowingCommands = true;
            while (keepShowingCommands)
            {
                Console.WriteLine("Welcome. Please enter a command.");
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

        public static void ShowOptions()
        {
            Console.WriteLine("[1] Adolescent or pensioner");
            Console.WriteLine("[2] Price for whole company");
            Console.WriteLine("[3] Repeat 10 times");
            Console.WriteLine("[4] The third word");
            Console.WriteLine("[0] Quit");
        }

        private static int AdolescentOrOPensioner(bool isCompany = false)
        {
            string displayMsg = "Please enter your age:";
            int age = ShowCommandAndGetUserInput(displayMsg);

            return CheckAgeAndGetPrice(age, isCompany);

        }

        private static void GetPriceForFullCompany()
        {
            string displayMsg = "How many persons are going to the cinema?";
            int nrOfPeople = ShowCommandAndGetUserInput(displayMsg);

            int sum = 0;
            for (int i = 0; i < nrOfPeople; i++)
            {
                sum += AdolescentOrOPensioner(true);
            }

            Print($"\nNr of people: {nrOfPeople}\nTotal price: {sum} SEK\n");
        }

        private static void RepeatTenTimes()
        {
            Print("Enter an arbitrary text");
            string text = GetInput();
            while (ValidateText(text))
            {
                text = InvalidInputForText();
            }
            for(int i = 0; i < 10; i++) 
            {
                Print(text);
            }
        }

        private static void TheThirdWord()
        {
            string displayMsg = "Enter a sentence:";
            string userInput = GetUserInput(displayMsg);

            while (!CheckLength(userInput))
            {
                displayMsg = "Sentence needs to be at least 3 words long.";
                userInput = GetUserInput(displayMsg);
            }
            string[] words = userInput.Split(' ');
            Print($"The third word: {words[2]}");

        }

        private static bool CheckLength(string userSentence)
        {
            string[] words = userSentence.Split(' ');
            return SentenceIsLongEnough(words.Length);
        }

        private static bool SentenceIsLongEnough(int length)
        {
            if (length < 3)
            {
                return false;
            }
            return true;
        }

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

        private static int ShowCommandAndGetUserInput(string message)
        {
            Print(message);
            string input = GetInput();

            int number = ValidateNumber(input);
            while(number == -1) 
            {
                InvalidInputForNumber();
            }
            return number;

        }

        private static int CheckAgeAndGetPrice(int age, bool isCompany)
        {

            int ageTypePrice;

            if (age < 20)
            {
                ageTypePrice = (int) AgeGroup.Adolescent;
            }
            else if (age > 64) 
            {
                ageTypePrice = (int) AgeGroup.Pensioner;
            } 
            else
            {
                ageTypePrice = (int) AgeGroup.Regular;
            }

            if (!isCompany)
            {
                var a = (AgeGroup) ageTypePrice;
                Print($"{a} price: {ageTypePrice} SEK.\n");
            }

            return ageTypePrice;

        }

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

        private static bool ValidateText(string text)
        {
            return !string.IsNullOrEmpty(text);
        }

        private static int InvalidInputForNumber()
        {
            Print("Try again:");
            string input = GetInput();
            return ValidateNumber(input);
        }

        private static string InvalidInputForText()
        {
            Print("Can't be empty.");
            string sentence = GetInput();

            return sentence;
        }

        private static void Print(string message)
        {
            Console.WriteLine(message);
        }

        private static string GetInput()
        {
            return Console.ReadLine();
        }
    }
}