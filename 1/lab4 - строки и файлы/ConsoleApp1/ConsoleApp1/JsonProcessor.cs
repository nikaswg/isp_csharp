using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ConsoleApp
{
    public static class JsonProcessor
    {
        public static void ProcessTeachersJson()
        {
            Console.Write("Введите путь к JSON файлу (оставьте пустым для использования teachers.json): ");
            string jsonFilePath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(jsonFilePath))
            {
                jsonFilePath = "teachers.json";
            }

            Console.Write("Введите путь для сохранения результатов (оставьте пустым для рабочего стола): ");
            string outputDirectory = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(outputDirectory))
            {
                outputDirectory = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "Teachers");
            }

            if (!File.Exists(jsonFilePath))
            {
                CreateSampleTeachersJson(jsonFilePath);
                Console.WriteLine($"\nФайл {jsonFilePath} создан с тестовыми данными.");
            }

            Console.WriteLine($"\nЧтение файла: {Path.GetFullPath(jsonFilePath)}");
            string jsonContent = File.ReadAllText(jsonFilePath);
            Console.WriteLine("\nСодержимое JSON файла:");
            Console.WriteLine(jsonContent);

            var teachers = JsonConvert.DeserializeObject<List<Teacher>>(jsonContent);
            Console.WriteLine($"\nНайдено {teachers?.Count ?? 0} преподавателей в файле");

            DistributeTeachersByChair(teachers, outputDirectory);
            Console.WriteLine($"\nПреподаватели распределены по кафедрам в папке: {outputDirectory}");
        }

        private static void CreateSampleTeachersJson(string filePath)
        {
            var teachers = new List<Teacher>
            {
                new Teacher { Name = "Иванов И.И.", Chair = "Информатика", PhoneNumber = "111-11-11" },
                new Teacher { Name = "Петров П.П.", Chair = "Математика", PhoneNumber = "222-22-22" },
                new Teacher { Name = "Сидоров С.С.", Chair = "Информатика", PhoneNumber = "333-33-33" },
                new Teacher { Name = "Кузнецов К.К.", Chair = "Физика", PhoneNumber = "444-44-44" },
                new Teacher { Name = "Смирнов С.С.", Chair = "Математика", PhoneNumber = "555-55-55" }
            };

            File.WriteAllText(filePath, JsonConvert.SerializeObject(teachers, Formatting.Indented));
        }

        private static void DistributeTeachersByChair(List<Teacher> teachers, string directoryPath)
        {
            Directory.CreateDirectory(directoryPath);
            Console.WriteLine($"\nСоздана директория: {directoryPath}");

            var chairs = teachers.GroupBy(t => t.Chair);
            Console.WriteLine("\nНайденные кафедры:");
            foreach (var chair in chairs)
            {
                Console.WriteLine($"- {chair.Key} ({chair.Count()} преподавателей)");

                string filePath = Path.Combine(directoryPath, $"{chair.Key}.txt");
                var lines = chair.Select(t => $"{t.Name}, {t.PhoneNumber}");
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