using System;
using System.Linq;

namespace RealEstateApp
{
    public static class TextProcessor
    {
        public static void ProcessText()
        {
            Console.WriteLine("Введите текст:");
            string inputText = Console.ReadLine();

            Console.WriteLine("\nИсходный текст:");
            Console.WriteLine(inputText);

            var words = inputText.Split(new[] { ' ', '.', ',', '!', '?', ';', ':' },
                StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 0)
            {
                Console.WriteLine("Текст не содержит слов.");
                return;
            }

            var shortestWord = words.OrderBy(w => w.Length).First();
            var longestWord = words.OrderByDescending(w => w.Length).First();

            if (shortestWord.Length != longestWord.Length)
            {
                int firstShortIndex = Array.FindIndex(words, w => w.Length == shortestWord.Length);
                int firstLongIndex = Array.FindIndex(words, w => w.Length == longestWord.Length);

                // Замена только первого вхождения
                string result = inputText.Replace(words[firstShortIndex], words[firstLongIndex]);
                Console.WriteLine("\nРезультат после замены:");
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine("\nДлины самого короткого и длинного слов совпадают. Текст не изменен.");
            }
        }
    }
}