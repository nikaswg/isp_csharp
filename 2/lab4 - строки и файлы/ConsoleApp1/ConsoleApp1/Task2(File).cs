using System;
using System.IO;
using System.Text.RegularExpressions;

namespace RealEstateApp
{
    public static class FileProcessor
    {
        public static void FindSentencesWithWord()
        {
            Console.Write("Введите путь к файлу (по умолчанию input.txt): ");
            string filePath = Console.ReadLine();
            filePath = string.IsNullOrWhiteSpace(filePath) ? "input.txt" : filePath;

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "Это первое предложение. Второе предложение содержит слово. Третье - нет.");
                Console.WriteLine($"Создан файл {filePath} с тестовыми данными.");
            }

            Console.Write("Введите слово для поиска: ");
            string searchWord = Console.ReadLine();

            string text = File.ReadAllText(filePath);
            var sentences = Regex.Split(text, @"(?<=[.!?])\s+");

            // Создаем список для хранения найденных предложений
            var foundSentences = new System.Collections.Generic.List<string>();

            foreach (var sentence in sentences)
            {
                if (Regex.IsMatch(sentence, $@"\b{Regex.Escape(searchWord)}\b", RegexOptions.IgnoreCase))
                {
                    foundSentences.Add(sentence.Trim());
                }
            }

            // Формируем имя файла для результатов
            string resultFilePath = Path.Combine(
                Path.GetDirectoryName(filePath) ?? Directory.GetCurrentDirectory(),
                "search_results.txt");

            // Записываем результаты в файл
            if (foundSentences.Count > 0)
            {
                File.WriteAllLines(resultFilePath, foundSentences);
                Console.WriteLine($"\nНайдено {foundSentences.Count} предложений. Результаты сохранены в файл: {resultFilePath}");
            }
            else
            {
                File.WriteAllText(resultFilePath, $"Предложений со словом '{searchWord}' не найдено.");
                Console.WriteLine($"\nПредложений со словом '{searchWord}' не найдено. Результаты сохранены в файл: {resultFilePath}");
            }
        }
    }
}