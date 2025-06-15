namespace CarShowroom
{
    // 9. Наследование (от Car)
    public class UsedCar : Car
    {
        // 3. Свойство
        public int Mileage { get; set; }

        // 2. Конструктор с параметрами
        public UsedCar(string model, decimal price, int manufactureYear, int mileage)
            : base(model, price, manufactureYear)
        {
            Mileage = mileage;
        }

        // 10. Переопределение метода
        public override void DisplayInfo()
        {
            Console.WriteLine($"{Model}, {_manufactureYear} год, Пробег: {Mileage} км, Цена: {Price:N0} руб. (б/у)");
        }

        // 10. Переопределение абстрактного метода
        public override string GetVehicleType() => "Подержанный автомобиль";
    }
}
