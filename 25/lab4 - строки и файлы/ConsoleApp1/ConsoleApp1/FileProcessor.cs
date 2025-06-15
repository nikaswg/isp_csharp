using System;
using System.IO;

namespace ConsoleApp
{
    public static class FileProcessor
    {
        public static void ProcessTextFile()
        {
            Console.Write("Введите путь к файлу (оставьте пустым для использования input.txt): ");
            string filePath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(filePath))
            {
                filePath = "input.txt";
            }

            // Создаем файл input.txt, если его нет
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "пример текста с согласными буквами и строчными словами.");
                Console.WriteLine($"Файл {filePath} создан с тестовыми данными.");
            }

            Console.WriteLine($"\nЧтение файла: {Path.GetFullPath(filePath)}");
            var inputText = File.ReadAllText(filePath);
            Console.WriteLine("\nСодержимое файла:");
            Console.WriteLine(inputText);

            var result = CapitalizeConsonantWords(inputText);

            string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath) ?? string.Empty, "output.txt");
            File.WriteAllText(outputFilePath, result);

            Console.WriteLine("\nРезультат обработки:");
            Console.WriteLine(result);
            Console.WriteLine($"\nРезультат записан в файл: {Path.GetFullPath(outputFilePath)}");
        }

        private static string CapitalizeConsonantWords(string text)
        {
            string vowels = "aeiouAEIOUаеёиоуыэюяАЕЁИОУЫЭЮЯ";
            var words = text.Split(new[] { ' ', '\n', '\r', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine("\nОбработка слов:");

            for (int i = 0; i < words.Length; i++)
            {
                if (!string.IsNullOrEmpty(words[i]) && !vowels.Contains(words[i][0]) && char.IsLower(words[i][0]))
                {
                    Console.WriteLine($"Преобразование: {words[i]} -> {char.ToUpper(words[i][0]) + words[i].Substring(1)}");
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
                }
            }

            return string.Join(" ", words);
        }
    }
}