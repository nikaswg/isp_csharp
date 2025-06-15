namespace CarShowroom
{
    // 9. Наследование (от Vehicle)
    public class Car : Vehicle
    {
        // 8. Статический элемент
        public static int TotalCarsCreated { get; private set; }

        // 7. Инкапсуляция (protected поле)
        protected int _manufactureYear;

        private decimal _price;

        // 4. Свойство с логикой в set
        public decimal Price
        {
            get => _price;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Цена должна быть положительной");
                _price = value;
            }
        }

        // 2. Конструктор с параметрами
        public Car(string model, decimal price, int manufactureYear)
        {
            Model = model;
            Price = price;
            _manufactureYear = manufactureYear;
            TotalCarsCreated++; // 8. Изменение статического поля
        }

        // 10. Переопределение абстрактного метода
        public override string GetVehicleType() => "Легковой автомобиль";

        // 10. Переопределение метода интерфейса
        public override void DisplayInfo()
        {
            Console.WriteLine($"{Model}, {_manufactureYear} год, Цена: {Price:N0} руб.");
        }

        // 11. Перегрузка оператора
        public static Showroom operator +(Showroom showroom, Car car)
        {
            showroom.AddCar(car);
            return showroom;
        }
    }
}