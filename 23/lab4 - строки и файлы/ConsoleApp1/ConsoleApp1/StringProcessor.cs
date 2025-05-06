using System;
using System.Linq;

namespace ConsoleApp
{
    class StringProcessor
    {
        public static void ProcessStrings()
        {
            Console.WriteLine("Введите текст:");
            string inputText = Console.ReadLine();
            var result = FindLongestWordSameStartEnd(inputText);

            Console.WriteLine("Слово максимальной длины, начинающееся и заканчивающееся одной буквой:");
            Console.WriteLine(string.IsNullOrEmpty(result) ? "Такого слова не найдено." : result);
        }

        private static string FindLongestWordSameStartEnd(string text)
        {
            var words = text.Split(new[] { ' ', '.', ',', '!', '?', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);
            return words
                .Where(w => w.Length > 1 && char.ToLower(w[0]) == char.ToLower(w[^1]))
                .OrderByDescending(w => w.Length)
                .FirstOrDefault() ?? "";
        }
    }
}