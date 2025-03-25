using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;

namespace SpaceApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // Исходные данные
            List<Spacecraft> spacecrafts = new List<Spacecraft>
            {
                new Spacecraft { Name = "Apollo 11", Speed = 40000, Planet = "Moon" },
                new Spacecraft { Name = "Voyager 1", Speed = 17000, Planet = "Jupiter" },
                new Spacecraft { Name = "Mars Rover", Speed = 20000, Planet = "Mars" },
                new Spacecraft { Name = "Hubble Telescope", Speed = 28000, Planet = "Earth" }
            };

            List<Planet> planets = new List<Planet>
            {
                new Planet { Name = "Moon", Galaxy = "Milky Way", YearDiscovered = 1610 },
                new Planet { Name = "Jupiter", Galaxy = "Milky Way", YearDiscovered = 1610 },
                new Planet { Name = "Mars", Galaxy = "Milky Way", YearDiscovered = 1659 },
                new Planet { Name = "Earth", Galaxy = "Milky Way", YearDiscovered = 0 }
            };

            // 1. Фильтрация: выдать корабли со скоростью > 20000 км/ч
            var fastSpacecrafts = spacecrafts.Where(s => s.Speed > 20000);
            Console.WriteLine("1. Корабли со скоростью > 20000 км/ч:");
            foreach (var s in fastSpacecrafts)
                Console.WriteLine($"- {s.Name}, скорость: {s.Speed} км/ч");

            // 2. Фильтрация по двум критериям (один критерий запрашивается у пользователя)
            Console.Write("\nВведите минимальную скорость корабля: ");
            int minSpeed = int.Parse(Console.ReadLine());
            var filteredSpacecrafts = spacecrafts.Where(s => s.Speed > minSpeed && s.Planet == "Mars");
            Console.WriteLine("\n2. Корабли с минимальной скоростью и планетой Mars:");
            foreach (var s in filteredSpacecrafts)
                Console.WriteLine($"- {s.Name}, скорость: {s.Speed} км/ч");

            // 3. Сортировка по скорости
            var sortedSpacecrafts = spacecrafts.OrderBy(s => s.Speed);
            Console.WriteLine("\n3. Корабли, отсортированные по скорости:");
            foreach (var s in sortedSpacecrafts)
                Console.WriteLine($"- {s.Name}, скорость: {s.Speed} км/ч");

            // 4. Количество кораблей, направляющихся к указанной планете
            int marsCount = spacecrafts.Count(s => s.Planet == "Mars");
            Console.WriteLine($"\n4. Количество кораблей, направляющихся к планете Mars: {marsCount}");

            // 5. Max, Average, Sum: работа со скоростью
            int maxSpeed = spacecrafts.Max(s => s.Speed);
            double avgSpeed = spacecrafts.Average(s => s.Speed);
            int totalSpeed = spacecrafts.Sum(s => s.Speed);
            Console.WriteLine($"\n5. Максимальная скорость: {maxSpeed} км/ч");
            Console.WriteLine($"Средняя скорость: {avgSpeed:F2} км/ч");
            Console.WriteLine($"Суммарная скорость: {totalSpeed} км/ч");

            // 6. Использование оператора let
            var speedDifferences = spacecrafts.Select(s => new
            {
                s.Name,
                SpeedFromLight = s.Speed / 300000.0
            });
            Console.WriteLine("\n6. Отношение скорости кораблей к скорости света:");
            foreach (var s in speedDifferences)
                Console.WriteLine($"- {s.Name}: {s.SpeedFromLight:F5} от скорости света");

            // 7. Группировка по планете
            var groupedByPlanet = spacecrafts.GroupBy(s => s.Planet);
            Console.WriteLine("\n7. Корабли, сгруппированные по планетам:");
            foreach (var group in groupedByPlanet)
            {
                Console.WriteLine($"Планета: {group.Key}");
                foreach (var s in group)
                    Console.WriteLine($"\t- {s.Name}");
            }

            // 8. Join: список кораблей и их галактика
            var joinedData = spacecrafts.Join(planets,
                s => s.Planet,
                p => p.Name,
                (s, p) => new { s.Name, s.Planet, p.Galaxy });
            Console.WriteLine("\n8. Корабли и их галактики:");
            foreach (var item in joinedData)
                Console.WriteLine($"- {item.Name} -> Планета: {item.Planet}, Галактика: {item.Galaxy}");

            // 9. GroupJoin: планеты и связанные с ними корабли
            var groupJoinData = planets.GroupJoin(spacecrafts,
                p => p.Name,
                s => s.Planet,
                (p, sGroup) => new { Planet = p.Name, Spacecrafts = sGroup });
            Console.WriteLine("\n9. Планеты и их корабли:");
            foreach (var group in groupJoinData)
            {
                Console.WriteLine($"Планета: {group.Planet}");
                foreach (var s in group.Spacecrafts)
                    Console.WriteLine($"\t- {s.Name}");
            }

            // 10. All: проверим, все ли корабли имеют скорость > 15000 км/ч
            bool allFast = spacecrafts.All(s => s.Speed > 15000);
            Console.WriteLine($"\n10. Все корабли имеют скорость > 15000 км/ч? {allFast}");

            // 11. Any: есть ли корабль, направляющийся к планете Jupiter
            bool anyToJupiter = spacecrafts.Any(s => s.Planet == "Jupiter");
            Console.WriteLine($"\n11. Есть ли корабль, направляющийся к планете Jupiter? {anyToJupiter}");
        }
    }
}