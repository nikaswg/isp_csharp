using System;
using System.Collections.Generic;
using System.Linq;

namespace ArchaeologyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Исходные данные
            List<Artifact> artifacts = new List<Artifact>
            {
                new Artifact { Name = "Маска Тутанхамона", DiscoveryYear = 1922, Culture = "Древний Египет", Value = 9500000 },
                new Artifact { Name = "Розеттский камень", DiscoveryYear = 1799, Culture = "Эллинистический Египет", Value = 8500000 },
                new Artifact { Name = "Терракотовая армия", DiscoveryYear = -210, Culture = "Древний Китай", Value = 15000000 },
                new Artifact { Name = "Гробница Цинь Шихуанди", DiscoveryYear = 1974, Culture = "Древний Китай", Value = 12000000 },
                new Artifact { Name = "Свитки Мертвого моря", DiscoveryYear = 1947, Culture = "Иудея", Value = 5000000 }
            };

            List<Expedition> expeditions = new List<Expedition>
            {
                new Expedition { Name = "Картер 1922", TargetCulture = "Древний Египет", Leader = "Говард Картер", Year = 1922 },
                new Expedition { Name = "Наполеоновская", TargetCulture = "Эллинистический Египет", Leader = "Французские ученые", Year = 1799 },
                new Expedition { Name = "Шэньси 1974", TargetCulture = "Древний Китай", Leader = "Крестьяне Янчжи", Year = 1974 },
                new Expedition { Name = "Кумран", TargetCulture = "Иудея", Leader = "Бедуины", Year = 1947 },
                new Expedition { Name = "Сиань", TargetCulture = "Древний Китай", Leader = "Китайские археологи", Year = 1974 }
            };

            Console.WriteLine("=== База данных археологических находок ===");
            Console.WriteLine("\nАртефакты:");
            artifacts.ForEach(a => Console.WriteLine($"- {a.Name} ({a.Culture}, найдена в {GetYearString(a.DiscoveryYear)}, стоимость: ${a.Value:N0}"));

            Console.WriteLine("\nЭкспедиции:");
            expeditions.ForEach(e => Console.WriteLine($"- {e.Name} ({e.Leader}, культура: {e.TargetCulture}, год: {e.Year})"));

            // 1. Фильтрация: артефакты, найденные после 1900 года
            var modernArtifacts = artifacts.Where(a => a.DiscoveryYear > 1900);
            Console.WriteLine("\n1. Артефакты, найденные после 1900 года:");
            modernArtifacts.ToList().ForEach(a => Console.WriteLine($"- {a.Name} ({a.DiscoveryYear} год)"));

            // 2. Фильтрация по двум критериям (год запрашивается у пользователя)
            Console.Write("\nВведите минимальный год находки: ");
            int minYear = int.Parse(Console.ReadLine());
            var valuableArtifacts = artifacts.Where(a => a.DiscoveryYear > minYear && a.Value > 10000000);
            Console.WriteLine($"\n2. Ценные артефакты (стоимость > $10M), найденные после {minYear} года:");
            valuableArtifacts.ToList().ForEach(a => Console.WriteLine($"- {a.Name} (стоимость: ${a.Value:N0})"));

            // 3. Сортировка артефактов по году находки
            var sortedArtifacts = artifacts.OrderBy(a => a.DiscoveryYear);
            Console.WriteLine("\n3. Артефакты, отсортированные по году находки:");
            sortedArtifacts.ToList().ForEach(a => Console.WriteLine($"- {a.Name} ({GetYearString(a.DiscoveryYear)})"));

            // 4. Количество китайских артефактов
            int chineseCount = artifacts.Count(a => a.Culture == "Древний Китай");
            Console.WriteLine($"\n4. Количество китайских артефактов: {chineseCount}");

            // 5. Max, Average, Sum: работа со стоимостью артефактов
            decimal maxValue = artifacts.Max(a => a.Value);
            decimal avgValue = artifacts.Average(a => a.Value);
            decimal totalValue = artifacts.Sum(a => a.Value);
            Console.WriteLine($"\n5. Самый ценный артефакт: ${maxValue:N0}");
            Console.WriteLine($"Средняя стоимость: ${avgValue:N0}");
            Console.WriteLine($"Общая стоимость всех артефактов: ${totalValue:N0}");

            // 6. Использование оператора let (возраст артефактов)
            var artifactAges = from a in artifacts
                               let ageYears = a.DiscoveryYear > 0 ? DateTime.Now.Year - a.DiscoveryYear : DateTime.Now.Year + (-a.DiscoveryYear)
                               let ageText = a.DiscoveryYear > 0 ? $"{ageYears} лет" : $"более {ageYears} лет"
                               select new { a.Name, Age = ageText };
            Console.WriteLine("\n6. Возраст артефактов с момента находки:");
            artifactAges.ToList().ForEach(a => Console.WriteLine($"- {a.Name}: {a.Age}"));

            // 7. Группировка артефактов по культуре
            var groupedByCulture = artifacts.GroupBy(a => a.Culture);
            Console.WriteLine("\n7. Артефакты, сгруппированные по культуре:");
            groupedByCulture.ToList().ForEach(g =>
            {
                Console.WriteLine($"Культура: {g.Key}");
                g.ToList().ForEach(a => Console.WriteLine($"\t- {a.Name}"));
            });

            // 8. Join: экспедиции и найденные артефакты
            var expeditionsWithFinds = expeditions.Join(artifacts,
                e => e.TargetCulture,
                a => a.Culture,
                (e, a) => new { e.Name, Artifact = a.Name, a.Culture });
            Console.WriteLine("\n8. Экспедиции и их находки:");
            expeditionsWithFinds.ToList().ForEach(e => Console.WriteLine($"- {e.Name} -> {e.Artifact} ({e.Culture})"));

            // 9. GroupJoin: руководители экспедиций и их находки
            var leadersWithExpeditions = expeditions.GroupBy(e => e.Leader)
                .Select(g => new { Leader = g.Key, Expeditions = g.ToList() });
            Console.WriteLine("\n9. Руководители экспедиций и их экспедиции:");
            leadersWithExpeditions.ToList().ForEach(l =>
            {
                Console.WriteLine($"Руководитель: {l.Leader}");
                l.Expeditions.ForEach(e => Console.WriteLine($"\t- {e.Name} ({e.Year} год)"));
            });

            // 10. All: все ли экспедиции проводились после 1700 года
            bool allModernExpeditions = expeditions.All(e => e.Year > 1700);
            Console.WriteLine($"\n10. Все экспедиции проводились после 1700 года? {allModernExpeditions}");

            // 11. Any: есть ли экспедиции к Древнему Китаю
            bool anyChineseExpeditions = expeditions.Any(e => e.TargetCulture == "Древний Китай");
            Console.WriteLine($"\n11. Есть ли экспедиции к Древнему Китаю? {anyChineseExpeditions}");
        }

        private static string GetYearString(int year)
        {
            return year > 0 ? $"{year} год" : $"{Math.Abs(year)} год до н.э.";
        }
    }
}