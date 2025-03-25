using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ConsoleApp
{
    class JsonProcessor
    {
        public static void ProcessJsonFile()
        {
            string jsonFilePath = "flowers.json";
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Flowers");

            // Создаем JSON-файл, если его нет
            if (!File.Exists(jsonFilePath))
            {
                CreateSampleJson(jsonFilePath);
                Console.WriteLine($"Файл {jsonFilePath} создан с тестовыми данными.");
            }

            // Загружаем и обрабатываем JSON
            var flowers = JsonConvert.DeserializeObject<List<Flower>>(File.ReadAllText(jsonFilePath));
            DistributeFlowersByCategory(flowers, directoryPath);
            Console.WriteLine($"Цветы распределены по категориям в папке {directoryPath}");
        }

        private static void CreateSampleJson(string filePath)
        {
            var flowers = new List<Flower>
            {
                new Flower { Title = "Роза", Category = "Розы", Price = 100 },
                new Flower { Title = "Тюльпан", Category = "Тюльпаны", Price = 50 },
                new Flower { Title = "Лилия", Category = "Лилии", Price = 120 }
            };

            File.WriteAllText(filePath, JsonConvert.SerializeObject(flowers, Formatting.Indented));
        }

        private static void DistributeFlowersByCategory(List<Flower> flowers, string directoryPath)
        {
            // Создаем папку
            Directory.CreateDirectory(directoryPath);

            // Группируем цветы по категориям
            var categories = flowers.GroupBy(f => f.Category);
            foreach (var category in categories)
            {
                string filePath = Path.Combine(directoryPath, $"{category.Key}.txt");
                var lines = category.Select(f => $"{f.Title}, {f.Price}");
                File.WriteAllLines(filePath, lines);
            }
        }
    }
}