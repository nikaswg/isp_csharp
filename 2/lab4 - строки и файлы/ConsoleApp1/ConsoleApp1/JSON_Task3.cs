// JsonProcessor.cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace RealEstateApp
{
    public static class JsonProcessor
    {
        public static void ProcessApartmentsJson()
        {
            Console.Write("Введите путь к JSON файлу (по умолчанию apartments.json): ");
            string jsonPath = Console.ReadLine();
            jsonPath = string.IsNullOrWhiteSpace(jsonPath) ? "apartments.json" : jsonPath;

            Console.Write("Введите путь для сохранения (по умолчанию рабочий стол/Apartments): ");
            string outputDir = Console.ReadLine();
            outputDir = string.IsNullOrWhiteSpace(outputDir)
                ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Apartments")
                : outputDir;

            if (!File.Exists(jsonPath))
            {
                CreateSampleJson(jsonPath);
                Console.WriteLine($"Создан файл {jsonPath} с тестовыми данными.");
            }

            string json = File.ReadAllText(jsonPath);
            var apartments = JsonConvert.DeserializeObject<List<Apartment>>(json);

            Console.WriteLine("\nСодержимое JSON файла:");
            Console.WriteLine(json);

            Directory.CreateDirectory(outputDir);
            var grouped = apartments.GroupBy(a => a.NumberOfRooms);

            foreach (var group in grouped)
            {
                string filePath = Path.Combine(outputDir, $"{group.Key}rooms.txt");
                var lines = group.Select(a => $"{a.Address}, {a.PhoneNumberOwner}");
                File.WriteAllLines(filePath, lines);

                Console.WriteLine($"\nФайл: {filePath}");
                Console.WriteLine(string.Join(Environment.NewLine, lines));
            }
        }

        private static void CreateSampleJson(string path)
        {
            var apartments = new List<Apartment>
            {
                new Apartment { Address = "ул. Ленина, 10", NumberOfRooms = 2, PhoneNumberOwner = "111-11-11" },
                new Apartment { Address = "пр. Мира, 5", NumberOfRooms = 3, PhoneNumberOwner = "222-22-22" },
                new Apartment { Address = "ул. Гагарина, 15", NumberOfRooms = 2, PhoneNumberOwner = "333-33-33" },
                new Apartment { Address = "ул. Садовая, 3", NumberOfRooms = 1, PhoneNumberOwner = "444-44-44" },
                new Apartment { Address = "пр. Космонавтов, 20", NumberOfRooms = 3, PhoneNumberOwner = "555-55-55" }
            };

            File.WriteAllText(path, JsonConvert.SerializeObject(apartments, Formatting.Indented));
        }
    }

    public class Apartment
    {
        public string Address { get; set; }
        public int NumberOfRooms { get; set; }
        public string PhoneNumberOwner { get; set; }
    }
}