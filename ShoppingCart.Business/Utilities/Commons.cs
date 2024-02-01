using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCart.Business.Utilities
{
    public static class Commons
    {
        public static string GetPathImage(string hostName)
        {
            string host = "https://" + hostName + "/";
            return host;
        }
        public static List<int> ConvertStringToList(string input)
        {
            // Split the input string using commas as separators
            string[] numberStrings = input.Split(',');

            // Create a list to store the parsed integers
            List<int> numbersList = new List<int>();

            // Parse each substring and add it to the list
            foreach (string numberString in numberStrings)
            {
                // Attempt to parse the string to an integer
                if (int.TryParse(numberString, out int number))
                {
                    // Successfully parsed, add it to the list
                    numbersList.Add(number);
                }
                else
                {
                    // Handle parsing failure if needed
                    Console.WriteLine($"Unable to parse: {numberString}");
                }
            }

            return numbersList;
        }

    }
}
