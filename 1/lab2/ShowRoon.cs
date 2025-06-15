using System;
using System.Collections.Generic;
using System.Linq;

namespace CarShowroom
{
    // 18. Собственный интерфейс
    public interface IShowroomDisplay
    {
        void ShowDisplay();
    }

    // 12. Обобщенный класс
    // 14. Наследование обобщений
    public class Showroom<T> where T : Vehicle
    {
        // 7. Инкапсуляция (protected поле)
        protected List<T> _items = new List<T>();

        // 3. Свойства
        public string Name { get; set; }
        public string Address { get; }
        public int Count => _items.Count;

        // 2. Конструктор с параметрами
        public Showroom(string name, string address)
        {
            Name = name;
            Address = address;
        }

        // 5. Индексатор
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _items.Count)
                    throw new IndexOutOfRangeException();
                return _items[index];
            }
            set
            {
                if (index < 0 || index >= _items.Count)
                    throw new IndexOutOfRangeException();
                _items[index] = value;
            }
        }

        // 16. Агрегация (Showroom содержит коллекцию Vehicle)
        public void AddItem(T item)
        {
            _items.Add(item);
        }

        // 13. Обобщенный метод
        public IEnumerable<T> GetItemsByCondition(Func<T, bool> condition)
        {
            return _items.Where(condition);
        }

        public void DisplayAllItems()
        {
            Console.WriteLine($"Салон: {Name}");
            Console.WriteLine($"Адрес: {Address}\n");

            foreach (var item in _items)
            {
                item.DisplayInfo();
            }
        }
    }

    // 14. Наследование обобщений
    public class Showroom : Showroom<Car>, IShowroomDisplay
    {
        // 2. Конструктор с параметрами
        public Showroom(string name, string address) : base(name, address) { }

        // 16. Агрегация (специализированные методы для Car)
        public void AddCar(Car car) => AddItem(car);

        public IEnumerable<Car> GetCarsByCondition(Func<Car, bool> condition) =>
            GetItemsByCondition(condition);

        public void DisplayAllCars() => DisplayAllItems();

        // 18. Реализация интерфейса
        public void ShowDisplay()
        {
            Console.WriteLine($"=== Информация о салоне {Name} ===");
            Console.WriteLine($"Количество автомобилей: {Count}");
        }

        //// 11. Перегрузка оператора
        //public static Showroom operator +(Showroom showroom, Car car)
        //{
        //    showroom.AddCar(car);
        //    return showroom;
        //}
    }

    // 15. Метод расширения
    public static class ShowroomExtensions
    {
        public static decimal CalculateTotalValue(this Showroom showroom)
        {
            return showroom.GetCarsByCondition(c => true).Sum(c => c.Price);
        }
    }
}
