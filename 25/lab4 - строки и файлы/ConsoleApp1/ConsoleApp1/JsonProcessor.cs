using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ConsoleApp
{
    public static class JsonProcessor
    {
        public static void ProcessJsonFile()
        {
            Console.Write("Введите путь к JSON файлу (оставьте пустым для использования flowers.json): ");
            string jsonFilePath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(jsonFilePath))
            {
                jsonFilePath = "flowers.json";
            }

            Console.Write("Введите путь для сохранения результатов (оставьте пустым для рабочего стола): ");
            string outputDirectory = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(outputDirectory))
            {
                outputDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Flowers");
            }

            // Создаем JSON-файл, если его нет
            if (!File.Exists(jsonFilePath))
            {
                CreateSampleJson(jsonFilePath);
                Console.WriteLine($"\nФайл {jsonFilePath} создан с тестовыми данными.");
            }

            Console.WriteLine($"\nЧтение файла: {Path.GetFullPath(jsonFilePath)}");
            string jsonContent = File.ReadAllText(jsonFilePath);
            Console.WriteLine("\nСодержимое JSON файла:");
            Console.WriteLine(jsonContent);

            var flowers = JsonConvert.DeserializeObject<List<Flower>>(jsonContent);
            Console.WriteLine($"\nНайдено {flowers?.Count ?? 0} цветов в файле");

            DistributeFlowersByCategory(flowers, outputDirectory);
            Console.WriteLine($"\nЦветы распределены по категориям в папке: {outputDirectory}");
        }

        private static void CreateSampleJson(string filePath)
        {
            var flowers = new List<Flower>
            {
                new Flower { Title = "Роза", Category = "Розы", Price = 100 },
                new Flower { Title = "Тюльпан", Category = "Тюльпаны", Price = 50 },
                new Flower { Title = "Лилия", Category = "Лилии", Price = 120 },
                new Flower { Title = "Ромашка", Category = "Полевые", Price = 30 },
                new Flower { Title = "Гвоздика", Category = "Полевые", Price = 45 }
            };

            File.WriteAllText(filePath, JsonConvert.SerializeObject(flowers, Formatting.Indented));
        }

        private static void DistributeFlowersByCategory(List<Flower> flowers, string directoryPath)
        {
            // Создаем папку
            Directory.CreateDirectory(directoryPath);
            Console.WriteLine($"\nСоздана директория: {directoryPath}");

            // Группируем цветы по категориям
            var categories = flowers.GroupBy(f => f.Category);
            Console.WriteLine("\nНайденные категории:");
            foreach (var category in categories)
            {
                Console.WriteLine($"- {category.Key} ({category.Count()} цветов)");

                string filePath = Path.Combine(directoryPath, $"{category.Key}.txt");
                var lines = category.Select(f => $"{f.Title}, {f.Price}");
                File.WriteAllLines(filePath, lines);

                Console.WriteLine($"  Создан файл: {filePath}");
                Console.WriteLine("  Содержимое файла:");
                foreach (var line in lines)
                {
                    Console.WriteLine($"  {line}");
                }
            }
        }
    }
}