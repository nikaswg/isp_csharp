using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceExploration
{
    class Program
    {
        static void Main(string[] args)
        {
            // Исходные данные
            List<CelestialBody> celestialBodies = new List<CelestialBody>
            {
                new CelestialBody { Name = "Глизе 581 g", DiscoveryYear = 2010, Type = "Экзопланета", Mass = 3.5 },
                new CelestialBody { Name = "Титан", DiscoveryYear = 1655, Type = "Спутник", Mass = 0.0225 },
                new CelestialBody { Name = "Бетельгейзе", DiscoveryYear = -129, Type = "Звезда", Mass = 15000 },
                new CelestialBody { Name = "Оумуамуа", DiscoveryYear = 2017, Type = "Межзвездный объект", Mass = 0.0000001 },
                new CelestialBody { Name = "Черная дыра в M87", DiscoveryYear = 2019, Type = "Черная дыра", Mass = 6500000000 }
            };

            List<SpaceMission> missions = new List<SpaceMission>
            {
                new SpaceMission { Name = "Вояджер-2", Target = "Спутник", Agency = "NASA", Year = 1977 },
                new SpaceMission { Name = "Хаббл", Target = "Звезда", Agency = "NASA/ESA", Year = 1990 },
                new SpaceMission { Name = "Чанъэ-4", Target = "Спутник", Agency = "CNSA", Year = 2018 },
                new SpaceMission { Name = "Джеймс Уэбб", Target = "Экзопланета", Agency = "NASA/ESA/CSA", Year = 2021 },
                new SpaceMission { Name = "Event Horizon", Target = "Черная дыра", Agency = "Международная", Year = 2017 }
            };

            Console.WriteLine("=== Космическая база данных ===");
            Console.WriteLine("\nНебесные тела:");
            celestialBodies.ForEach(b => Console.WriteLine($"- {b.Name} ({b.Type}, открыта в {b.DiscoveryYear} году, масса: {b.Mass} масс Земли)"));

            Console.WriteLine("\nКосмические миссии:");
            missions.ForEach(m => Console.WriteLine($"- {m.Name} ({m.Agency}, цель: {m.Target}, запуск: {m.Year} год)"));

            // 1. Фильтрация: космические объекты, открытые после 2000 года
            var recentDiscoveries = celestialBodies.Where(b => b.DiscoveryYear > 2000);
            Console.WriteLine("\n1. Объекты, открытые после 2000 года:");
            recentDiscoveries.ToList().ForEach(b => Console.WriteLine($"- {b.Name} ({b.DiscoveryYear} год)"));

            // 2. Фильтрация по двум критериям (год запрашивается у пользователя)
            Console.Write("\nВведите минимальный год открытия: ");
            int minYear = int.Parse(Console.ReadLine());
            var massiveBodies = celestialBodies.Where(b => b.DiscoveryYear > minYear && b.Mass > 1);
            Console.WriteLine($"\n2. Массивные объекты (масса > 1), открытые после {minYear} года:");
            massiveBodies.ToList().ForEach(b => Console.WriteLine($"- {b.Name} (масса: {b.Mass})"));

            // 3. Сортировка объектов по году открытия
            var sortedBodies = celestialBodies.OrderBy(b => b.DiscoveryYear);
            Console.WriteLine("\n3. Объекты, отсортированные по году открытия:");
            sortedBodies.ToList().ForEach(b => Console.WriteLine($"- {b.Name} ({b.DiscoveryYear} год)"));

            // 4. Количество звезд в каталоге
            int starsCount = celestialBodies.Count(b => b.Type == "Звезда");
            Console.WriteLine($"\n4. Количество звезд в каталоге: {starsCount}");

            // 5. Max, Average, Sum: работа с массой объектов
            double maxMass = celestialBodies.Max(b => b.Mass);
            double avgMass = celestialBodies.Average(b => b.Mass);
            double totalMass = celestialBodies.Sum(b => b.Mass);
            Console.WriteLine($"\n5. Самый массивный объект: {maxMass} масс Земли");
            Console.WriteLine($"Средняя масса: {avgMass:F2}");
            Console.WriteLine($"Суммарная масса всех объектов: {totalMass:F2}");

            // 6. Использование оператора let (возраст объектов)
            // 6. Использование оператора let (возраст объектов)
            var bodyAges = from b in celestialBodies
                           let ageYears = b.DiscoveryYear > 0 ? DateTime.Now.Year - b.DiscoveryYear : DateTime.Now.Year + (-b.DiscoveryYear)
                           let ageText = b.DiscoveryYear > 0 ? $"{ageYears} лет" : $"более {ageYears} лет"
                           select new { b.Name, Age = ageText };
            Console.WriteLine("\n6. Возраст объектов с момента открытия:");
            bodyAges.ToList().ForEach(b => Console.WriteLine($"- {b.Name}: {b.Age}"));


            // 7. Группировка объектов по типу
            var groupedByType = celestialBodies.GroupBy(b => b.Type);
            Console.WriteLine("\n7. Объекты, сгруппированные по типу:");
            groupedByType.ToList().ForEach(g =>
            {
                Console.WriteLine($"Тип: {g.Key}");
                g.ToList().ForEach(b => Console.WriteLine($"\t- {b.Name}"));
            });

            // 8. Join: миссии и их цели
            var missionsWithTargets = missions.Join(celestialBodies,
                m => m.Target,
                b => b.Type,
                (m, b) => new { m.Name, Target = b.Name, b.Type });
            Console.WriteLine("\n8. Миссии и их цели:");
            missionsWithTargets.ToList().ForEach(m => Console.WriteLine($"- {m.Name} -> {m.Target} ({m.Type})"));

            // 9. GroupJoin: космические агентства и их миссии
            var agenciesWithMissions = missions.GroupBy(m => m.Agency)
                .Select(g => new { Agency = g.Key, Missions = g.ToList() });
            Console.WriteLine("\n9. Агентства и их миссии:");
            agenciesWithMissions.ToList().ForEach(a =>
            {
                Console.WriteLine($"Агентство: {a.Agency}");
                a.Missions.ForEach(m => Console.WriteLine($"\t- {m.Name} ({m.Year} год)"));
            });

            // 10. All: все ли миссии запущены после 1957 года (начало космической эры)
            bool allModern = missions.All(m => m.Year > 1957);
            Console.WriteLine($"\n10. Все миссии запущены после 1957 года? {allModern}");

            // 11. Any: есть ли миссии к черным дырам
            bool anyBlackHoleMissions = missions.Any(m => m.Target == "Черная дыра");
            Console.WriteLine($"\n11. Есть ли миссии к черным дырам? {anyBlackHoleMissions}");
        }
    }
}
    