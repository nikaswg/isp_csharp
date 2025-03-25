using System;
using System.IO;

namespace ConsoleApp
{
    class FileProcessor
    {
        public static void ProcessTextFile()
        {
            string inputFilePath = Path.Combine(Directory.GetCurrentDirectory(), "input.txt"); // Абсолютный путь к input.txt
            string outputFilePath = Path.Combine(Directory.GetCurrentDirectory(), "output.txt"); // Абсолютный путь к output.txt

            // Создаем файл input.txt, если его нет
            if (!File.Exists(inputFilePath))
            {
                File.WriteAllText(inputFilePath, "пример текста с согласными буквами и строчными словами.");
                Console.WriteLine($"Файл {inputFilePath} создан с тестовыми данными.");
            }

            Console.WriteLine($"Путь к входному файлу: {inputFilePath}");

            var inputText = File.ReadAllText(inputFilePath);
            var result = CapitalizeConsonantWords(inputText);

            // Создаем или перезаписываем файл output.txt
            File.WriteAllText(outputFilePath, result);
            Console.WriteLine($"Результат записан в файл: {outputFilePath}");
        }

        private static string CapitalizeConsonantWords(string text)
        {
            string vowels = "aeiouAEIOUаеёиоуыэюяАЕЁИОУЫЭЮЯ";
            var words = text.Split(new[] { ' ', '\n', '\r', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++)
            {
                if (!string.IsNullOrEmpty(words[i]) && !vowels.Contains(words[i][0]) && char.IsLower(words[i][0]))
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
                }
            }

            return string.Join(" ", words);
        }
    }
}