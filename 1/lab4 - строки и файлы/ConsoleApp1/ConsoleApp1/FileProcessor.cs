using System;
using System.IO;
using System.Linq;

namespace ConsoleApp
{
    public static class FileProcessor
    {
        public static void ReverseFileLines()
        {
            Console.Write("Введите путь к файлу (оставьте пустым для использования input.txt): ");
            string filePath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(filePath))
            {
                filePath = "input.txt";
            }

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "Первая строка\nВторая строка\nТретья строка");
                Console.WriteLine($"Файл {filePath} создан с тестовыми данными.");
            }

            Console.WriteLine($"\nЧтение файла: {Path.GetFullPath(filePath)}");
            var lines = File.ReadAllLines(filePath);

            Console.WriteLine("\nСодержимое файла в обратном порядке:");
            foreach (var line in lines.Reverse())
            {
                Console.WriteLine(line);
            }

            string outputFilePath = Path.Combine(
                Path.GetDirectoryName(filePath) ?? string.Empty,
                "reversed_" + Path.GetFileName(filePath));

            File.WriteAllLines(outputFilePath, lines.Reverse());
            Console.WriteLine($"\nРезультат записан в файл: {Path.GetFullPath(outputFilePath)}");
        }
    }
}