using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class StringProcessor
    {
        public static void ProcessStrings()
        {
            Console.WriteLine("Введите текст:");
            string inputText = Console.ReadLine();
            var result = FindNonSymmetricWords(inputText);

            Console.WriteLine("Несимметричные слова с четной длиной:");
            foreach (var word in result)
            {
                Console.WriteLine(word);
            }
        }

        private static IEnumerable<string> FindNonSymmetricWords(string text)
        {
            var words = text.Split(new[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Where(word => word.Length % 2 == 0 && word != new string(word.Reverse().ToArray()));
        }
    }
}