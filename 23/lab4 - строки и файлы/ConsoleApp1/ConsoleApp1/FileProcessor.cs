using System;
using System.IO;
using System.Linq;

namespace ConsoleApp
{
    class FileProcessor
    {
        public static void ProcessTextFile()
        {
            string inputFilePath = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
            string outputFilePath = Path.Combine(Directory.GetCurrentDirectory(), "output.txt");

            if (!File.Exists(inputFilePath))
            {
                File.WriteAllText(inputFilePath, "  Это   пример   текста  с   лишними    пробелами. ");
                Console.WriteLine($"Файл {inputFilePath} создан с тестовыми данными.");
            }

            Console.WriteLine($"Путь к входному файлу: {inputFilePath}");

            var inputText = File.ReadAllText(inputFilePath);
            var result = ProcessText(inputText);

            File.WriteAllText(outputFilePath, result);
            Console.WriteLine($"Результат записан в файл: {outputFilePath}");
        }

        private static string ProcessText(string text)
        {
            var words = text
                .Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(",", words) + ".";
        }
    }
}