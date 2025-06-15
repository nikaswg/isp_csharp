using System;
using System.Linq;

namespace ConsoleApp
{
    public static class TextProcessor
    {
        public static void FindWordWithMostI()
        {
            Console.WriteLine("Введите текст:");
            string inputText = Console.ReadLine();

            var words = inputText.Split(new[] { ' ', '.', ',', '!', '?', ';', ':' },
                StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 0)
            {
                Console.WriteLine("Текст не содержит слов.");
                return;
            }

            var wordWithMostI = words
                .OrderByDescending(w => w.Count(c => c == 'и' || c == 'И'))
                .FirstOrDefault();

            Console.WriteLine($"\nПервое слово с наибольшим числом букв 'и': {wordWithMostI}");
            Console.WriteLine($"Количество букв 'и': {wordWithMostI.Count(c => c == 'и' || c == 'И')}");
        }
    }
}