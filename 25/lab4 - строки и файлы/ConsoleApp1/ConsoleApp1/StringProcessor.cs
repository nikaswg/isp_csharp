using System;
using System.Linq;

namespace ConsoleApp
{
    public static class StringProcessor
    {
        public static void ProcessText()
        {
            Console.WriteLine("Введите текст:");
            string inputText = Console.ReadLine();

            Console.WriteLine("\nИсходный текст:");
            Console.WriteLine(inputText);

            var result = RemoveWordsWithMaxLength(inputText);

            Console.WriteLine("\nРезультат после удаления слов с максимальной длиной:");
            Console.WriteLine(result);
        }

        private static string RemoveWordsWithMaxLength(string text)
        {
            var words = text.Split(new[] { ' ', '.', ',', '!', '?', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 0) return text;

            int maxLength = words.Max(w => w.Length);
            Console.WriteLine($"\nМаксимальная длина слова: {maxLength}");

            var wordsToRemove = words.Where(w => w.Length == maxLength).ToList();
            Console.WriteLine("\nСлова для удаления:");
            foreach (var word in wordsToRemove)
            {
                Console.WriteLine(word);
            }

            var resultWords = words.Where(w => w.Length != maxLength);
            return string.Join(" ", resultWords);
        }
    }
}