namespace COMP003AFinalProject
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        // Function to validate and get user input
        static T GetUserInput<T>(string prompt, Func<string, (bool isValid, T value)> validationFunc)
        {
            while (true)
            {
                Console.Write(prompt);
                string userInput = Console.ReadLine();

                var validationResult = validationFunc(userInput);
                if (validationResult.isValid)
                {
                    return validationResult.value;
                }

                Console.WriteLine("Invalid input. Please try again.");
            }
        }

        // Function to validate birth year
        static (bool isValid, int value) ValidateBirthYear(string input)
        {
            if (int.TryParse(input, out int year))
            {
                int currentYear = DateTime.Now.Year;
                return (1900 <= year && year <= currentYear, year);
            }

            return (false, 0);
        }

        // Function to validate gender
        static (bool isValid, string value) ValidateGender(string input)
        {
            string normalizedInput = input.ToUpper();
            return (normalizedInput == "M" || normalizedInput == "F" || normalizedInput == "O", normalizedInput);
        }

        // Function to display profile summary
        static void DisplayProfileSummary(string firstName, string lastName, int birthYear, string gender, List<(string question, string response)> responses)
        {
            // Calculate age
            int currentYear = DateTime.Now.Year;
            int age = currentYear - birthYear;

            // Display profile information
            Console.WriteLine($"Full Name: {lastName}, {firstName}");
            Console.WriteLine($"Age: {age}");

            // Map gender to full description
            Dictionary<string, string> genderMap = new Dictionary<string, string>
        {
            { "M", "Male" },
            { "F", "Female" },
            { "O", "Other" }
        };
            string genderDescription = genderMap.ContainsKey(gender) ? genderMap[gender] : "Unknown";
            Console.WriteLine($"Gender: {genderDescription}");

            // Display user responses
            for (int i = 0; i < responses.Count; i++)
            {
                Console.WriteLine($"Question {i + 1}: {responses[i].question}");
                Console.WriteLine($"Response {i + 1}: {responses[i].response}");
                Console.WriteLine();
            }
        }

        // Main function
        static void Main()
        {
            Console.WriteLine("Welcome to the Dating App Console Application!");

            // Get user input for profile
            string firstName = GetUserInput("Enter your First Name: ", input => (!string.IsNullOrWhiteSpace(input) && !input.Any(char.IsDigit) && !input.Any(char.IsPunctuation), input));
            string lastName = GetUserInput("Enter your Last Name: ", input => (!string.IsNullOrWhiteSpace(input) && !input.Any(char.IsDigit) && !input.Any(char.IsPunctuation), input));
            int birthYear = GetUserInput("Enter your Birth Year: ", ValidateBirthYear);
            string gender = GetUserInput("Enter your Gender (M/F/O): ", ValidateGender);

            // Create a questionnaire with 10 questions
            List<string> questions = new List<string>
        {
            "What is your favorite color?",
            "Would you say you prefer staying in or going out?",
            "Describe your ideal date night.",
            "What are your hobbies?",
            "Have you dated in the past?",
            "If so how many people have you dated?, (if not just say NA)",
            "What are you looking for (one night stand, long term, see where things go)?",
            "Whats your prefered partners gender?",
            "Would you say you prefer staying in or going out?",
            "Is there anything specific you would like people to know when meeting you?",
            "Last question is about us, how did you find this app?",
            
            // Add more questions as needed
        };

            // Get user responses
            List<(string question, string response)> responses = new List<(string question, string response)>();
            foreach (string question in questions)
            {
                Console.Write(question + " ");
                string response = Console.ReadLine();
                responses.Add((question, response));
            }

            // Display profile summary
            DisplayProfileSummary(firstName, lastName, birthYear, gender, responses);
        }
    }
}