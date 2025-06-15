using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer;

namespace MusicApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // Исходные данные
            List<MusicalInstrument> instruments = new List<MusicalInstrument>
            {
                new MusicalInstrument { Name = "Скрипка Страдивари", YearCreated = 1715, Type = "Струнный" },
                new MusicalInstrument { Name = "Фортепиано Бетховена", YearCreated = 1803, Type = "Клавишный" },
                new MusicalInstrument { Name = "Флейта Моцарта", YearCreated = 1785, Type = "Духовой" },
                new MusicalInstrument { Name = "Виолончель", YearCreated = 1660, Type = "Струнный" },
                new MusicalInstrument { Name = "Арфа", YearCreated = 1750, Type = "Струнный" }
            };

            List<Composer> composers = new List<Composer>
            {
                new Composer { Name = "Вивальди", Era = "Барокко", FavoriteInstrument = "Струнный" },
                new Composer { Name = "Бетховен", Era = "Классицизм", FavoriteInstrument = "Клавишный" },
                new Composer { Name = "Моцарт", Era = "Классицизм", FavoriteInstrument = "Духовой" },
                new Composer { Name = "Бах", Era = "Барокко", FavoriteInstrument = "Струнный" },
                new Composer { Name = "Шопен", Era = "Романтизм", FavoriteInstrument = "Клавишный" }
            };

            Console.WriteLine("=== Исходные данные ===");
            Console.WriteLine("\nМузыкальные инструменты:");
            foreach (var i in instruments)
                Console.WriteLine($"- {i.Name} ({i.Type}, создан в {i.YearCreated} году)");

            Console.WriteLine("\nКомпозиторы:");
            foreach (var c in composers)
                Console.WriteLine($"- {c.Name} (эпоха: {c.Era}, любимый инструмент: {c.FavoriteInstrument})");

            // 1. Фильтрация: выдать инструменты, созданные после 1700 года
            var modernInstruments = instruments.Where(i => i.YearCreated > 1700);
            Console.WriteLine("\n1. Инструменты, созданные после 1700 года:");
            foreach (var i in modernInstruments)
                Console.WriteLine($"- {i.Name} ({i.YearCreated} год)");

            // 2. Фильтрация по двум критериям (год запрашивается у пользователя)
            Console.Write("\nВведите минимальный год создания инструмента: ");
            int minYear = int.Parse(Console.ReadLine());
            var filteredInstruments = instruments.Where(i => i.YearCreated > minYear && i.Type == "Струнный");
            Console.WriteLine($"\n2. Струнные инструменты, созданные после {minYear} года:");
            foreach (var i in filteredInstruments)
                Console.WriteLine($"- {i.Name} ({i.YearCreated} год)");

            // 3. Сортировка инструментов по году создания
            var sortedInstruments = instruments.OrderBy(i => i.YearCreated);
            Console.WriteLine("\n3. Инструменты, отсортированные по году создания:");
            foreach (var i in sortedInstruments)
                Console.WriteLine($"- {i.Name} ({i.YearCreated} год)");

            // 4. Количество струнных инструментов
            int stringCount = instruments.Count(i => i.Type == "Струнный");
            Console.WriteLine($"\n4. Количество струнных инструментов: {stringCount}");

            // 5. Max, Average, Sum: работа с годами создания
            int maxYear = instruments.Max(i => i.YearCreated);
            double avgYear = instruments.Average(i => i.YearCreated);
            int totalYears = instruments.Sum(i => i.YearCreated);
            Console.WriteLine($"\n5. Самый новый инструмент создан в {maxYear} году");
            Console.WriteLine($"Средний год создания: {avgYear:F0}");
            Console.WriteLine($"Сумма всех годов создания: {totalYears}");

            // 6. Использование оператора let
            var instrumentAges = from i in instruments
                                 let age = DateTime.Now.Year - i.YearCreated
                                 select new { i.Name, Age = age };
            Console.WriteLine("\n6. Возраст инструментов:");
            foreach (var i in instrumentAges)
                Console.WriteLine($"- {i.Name}: {i.Age} лет");

            // 7. Группировка инструментов по типу
            var groupedByType = instruments.GroupBy(i => i.Type);
            Console.WriteLine("\n7. Инструменты, сгруппированные по типу:");
            foreach (var group in groupedByType)
            {
                Console.WriteLine($"Тип: {group.Key}");
                foreach (var i in group)
                    Console.WriteLine($"\t- {i.Name}");
            }

            // 8. Join: список композиторов и их любимые инструменты
            var composersWithInstruments = composers.Join(instruments,
                c => c.FavoriteInstrument,
                i => i.Type,
                (c, i) => new { c.Name, Instrument = i.Name, i.Type });
            Console.WriteLine("\n8. Композиторы и их любимые инструменты:");
            foreach (var item in composersWithInstruments)
                Console.WriteLine($"- {item.Name} -> {item.Instrument} ({item.Type})");

            // 9. GroupJoin: эпохи и связанные с ними композиторы
            var erasWithComposers = composers.GroupBy(c => c.Era)
                .Select(g => new { Era = g.Key, Composers = g.ToList() });
            Console.WriteLine("\n9. Эпохи и их композиторы:");
            foreach (var group in erasWithComposers)
            {
                Console.WriteLine($"Эпоха: {group.Era}");
                foreach (var c in group.Composers)
                    Console.WriteLine($"\t- {c.Name}");
            }

            // 10. All: проверим, все ли инструменты созданы до 1900 года
            bool allAncient = instruments.All(i => i.YearCreated < 1900);
            Console.WriteLine($"\n10. Все инструменты созданы до 1900 года? {allAncient}");

            // 11. Any: есть ли композитор эпохи романтизма
            bool anyRomantic = composers.Any(c => c.Era == "Романтизм");
            Console.WriteLine($"\n11. Есть ли композитор эпохи романтизма? {anyRomantic}");
        }
    }
}