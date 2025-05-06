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
            string jsonFilePath = "cars.json";
            string directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Cars");

            if (!File.Exists(jsonFilePath))
            {
                CreateSampleJson(jsonFilePath);
                Console.WriteLine($"Файл {jsonFilePath} создан с тестовыми данными.");
            }

            var cars = JsonConvert.DeserializeObject<List<Car>>(File.ReadAllText(jsonFilePath));
            DistributeCarsByBrand(cars, directoryPath);
            Console.WriteLine($"Автомобили распределены по маркам в папке {directoryPath}");
        }

        private static void CreateSampleJson(string filePath)
        {
            var cars = new List<Car>
            {
                new Car { NameOwner = "Алексей", ModelCar = "Toyota", NumberCar = "X123AA" },
                new Car { NameOwner = "Мария", ModelCar = "BMW", NumberCar = "Y456BB" },
                new Car { NameOwner = "Игорь", ModelCar = "Toyota", NumberCar = "Z789CC" }
            };

            File.WriteAllText(filePath, JsonConvert.SerializeObject(cars, Formatting.Indented));
        }

        private static void DistributeCarsByBrand(List<Car> cars, string directoryPath)
        {
            Directory.CreateDirectory(directoryPath);

            var grouped = cars.GroupBy(c => c.ModelCar);
            foreach (var group in grouped)
            {
                string filePath = Path.Combine(directoryPath, $"{group.Key}.txt");
                var lines = group.Select(c => $"{c.NameOwner}, {c.NumberCar}");
                File.WriteAllLines(filePath, lines);
            }
        }
    }
}