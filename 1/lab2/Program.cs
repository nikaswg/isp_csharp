using System;

namespace CarShowroom
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Демонстрация работы автомобильного салона ===\n");

            try
            {
                // 1. Использование собственных классов (Showroom, Car, UsedCar, Manager)
                // 2. Конструкторы с параметрами
                var showroom = new Showroom("АвтоПремиум", "ул. Автомобильная, 123");

                // 16. Агрегация
                showroom.AddCar(new Car("Toyota Camry", 2500000, 2022));
                showroom.AddCar(new Car("BMW X5", 5500000, 2023));
                showroom.AddCar(new Car("Honda Civic", 1800000, 2021));
                showroom.AddCar(new UsedCar("Ford Focus", 900000, 2018, 45000));

                // 5. Использование индексатора
                if (showroom.Count > 0)
                {
                    var firstCar = showroom[0];
                    Console.WriteLine($"\nПервый автомобиль в салоне: {firstCar.Model}");
                }

                var premiumCar = new Car("Mercedes S-Class", 8000000, 2023);
                showroom.AddCar(premiumCar);

                var usedCar = new UsedCar("Kia Rio", 750000, 2017, 80000);
                showroom.AddCar(usedCar);

                // 11. Перегрузка оператора
                showroom += new Car("Hyundai Solaris", 1200000, 2022);

                // 12. Использование обобщений
                var manager = new Manager<Car>("Иван Петров", 5);
                manager.SellCar(premiumCar);

                // 13. Обобщенный метод
                var expensiveCars = showroom.GetCarsByCondition(c => c.Price > 2000000);
                Console.WriteLine("\nДорогие автомобили:");
                foreach (var car in expensiveCars)
                {
                    car.DisplayInfo();
                }

                // 15. Метод расширения
                var totalValue = showroom.CalculateTotalValue();

                Console.WriteLine("\n=== Информация о всех автомобилях в салоне ===");
                showroom.DisplayAllCars();

                Console.WriteLine($"\nОбщая стоимость всех автомобилей: {totalValue:N0} руб.");

                // 3. Использование свойств
                showroom.Name = "АвтоПремиум+";
                Console.WriteLine($"\nНовое название салона: {showroom.Name}");

                // 4. Свойство с логикой
                try
                {
                    premiumCar.Price = -1000000;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"\nОшибка при установке цены: {ex.Message}");
                }

                // 8. Статический элемент
                Console.WriteLine($"\nВсего создано автомобилей: {Car.TotalCarsCreated}");

                // 18. Использование интерфейса
                IShowroomDisplay display = showroom;
                Console.WriteLine("\n=== Отображение через интерфейс ===");
                display.ShowDisplay();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nПроизошла ошибка: {ex.Message}");
            }

            Console.WriteLine("\n=== Демонстрация завершена ===");
        }
    }
}