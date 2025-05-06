using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherApp
{
    class Program
    {
        public class WeatherForecast
        {
            public string Date { get; set; }
            public double TemperatureC { get; set; }
            public string CityCode { get; set; }
        }

        public class City
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Country { get; set; }
        }

        static void Main(string[] args)
        {
            var forecasts = new List<WeatherForecast>
            {
                new WeatherForecast { Date = "2025-05-05", TemperatureC = 22.5, CityCode = "NYC" },
                new WeatherForecast { Date = "2025-05-05", TemperatureC = 18.2, CityCode = "LDN" },
                new WeatherForecast { Date = "2025-05-05", TemperatureC = 30.1, CityCode = "DXB" },
                new WeatherForecast { Date = "2025-05-05", TemperatureC = 25.3, CityCode = "NYC" },
                new WeatherForecast { Date = "2025-05-05", TemperatureC = 12.0, CityCode = "LDN" }
            };

            var cities = new List<City>
            {
                new City { Code = "NYC", Name = "New York", Country = "USA" },
                new City { Code = "LDN", Name = "London", Country = "UK" },
                new City { Code = "DXB", Name = "Dubai", Country = "UAE" }
            };

            Console.WriteLine("Исходные данные о прогнозах погоды:");
            foreach (var f in forecasts)
                Console.WriteLine($"{f.Date} — {f.TemperatureC}C — {f.CityCode}");

            Console.WriteLine("\nИсходные данные о городах:");
            foreach (var c in cities)
                Console.WriteLine($"{c.Code} — {c.Name} — {c.Country}");

            Console.WriteLine("\n1. Фильтрация: Прогнозы с температурой > 20C:");
            var highTemp = forecasts.Where(f => f.TemperatureC > 20);
            foreach (var f in highTemp)
                Console.WriteLine($"{f.CityCode} — {f.TemperatureC}C");

            Console.WriteLine("\n2. Фильтрация по городу и температуре < userInput:");
            Console.Write("Введите максимальную температуру: ");
            double maxTemp = double.Parse(Console.ReadLine());
            var filtered = forecasts.Where(f => f.CityCode == "NYC" && f.TemperatureC < maxTemp);
            foreach (var f in filtered)
                Console.WriteLine($"{f.CityCode} — {f.TemperatureC}C");

            Console.WriteLine("\n3. Сортировка прогнозов по температуре:");
            var sorted = forecasts.OrderBy(f => f.TemperatureC);
            foreach (var f in sorted)
                Console.WriteLine($"{f.CityCode} — {f.TemperatureC}C");

            Console.WriteLine("\n4. Кол-во прогнозов для города LDN:");
            var count = forecasts.Count(f => f.CityCode == "LDN");
            Console.WriteLine($"Количество: {count}");

            Console.WriteLine("\n5. Max, Average, Sum температур:");
            Console.WriteLine($"Максимум: {forecasts.Max(f => f.TemperatureC)}C");
            Console.WriteLine($"Среднее: {forecasts.Average(f => f.TemperatureC):F2}C");
            Console.WriteLine($"Сумма: {forecasts.Sum(f => f.TemperatureC):F2}C");

            Console.WriteLine("\n6. Использование 'let': Температура в F:");
            var withFahrenheit = from f in forecasts
                                 let tempF = f.TemperatureC * 9 / 5 + 32
                                 select new { f.CityCode, f.TemperatureC, TemperatureF = tempF };
            foreach (var item in withFahrenheit)
                Console.WriteLine($"{item.CityCode}: {item.TemperatureC}C = {item.TemperatureF}F");

            Console.WriteLine("\n7. Группировка прогнозов по городу:");
            var grouped = from f in forecasts
                          group f by f.CityCode into grp
                          select grp;
            foreach (var group in grouped)
            {
                Console.WriteLine($"\nГород: {group.Key}");
                foreach (var f in group)
                    Console.WriteLine($"  {f.TemperatureC}C");
            }

            Console.WriteLine("\n8. Join: Прогнозы с названием города и страной:");
            var joined = from f in forecasts
                         join c in cities on f.CityCode equals c.Code
                         select new { c.Name, c.Country, f.TemperatureC };
            foreach (var item in joined)
                Console.WriteLine($"{item.Name}, {item.Country}: {item.TemperatureC}C");

            Console.WriteLine("\n9. GroupJoin: Города и их прогнозы:");
            var groupJoin = from c in cities
                            join f in forecasts on c.Code equals f.CityCode into cityForecasts
                            select new { c.Name, Forecasts = cityForecasts };
            foreach (var item in groupJoin)
            {
                Console.WriteLine($"\nГород: {item.Name}");
                foreach (var f in item.Forecasts)
                    Console.WriteLine($"  {f.TemperatureC}C");
            }

            Console.WriteLine("\n10. All: Все ли температуры выше 10C?");
            bool allAbove10 = forecasts.All(f => f.TemperatureC > 10);
            Console.WriteLine(allAbove10 ? "Да" : "Нет");

            Console.WriteLine("\n11. Any: Есть ли температура выше 28C?");
            bool anyHot = forecasts.Any(f => f.TemperatureC > 28);
            Console.WriteLine(anyHot ? "Да" : "Нет");
        }
    }
}