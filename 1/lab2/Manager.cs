namespace CarShowroom
{
    // 12. Обобщенный класс
    public class Manager<T> where T : Vehicle
    {
        // 3. Свойства
        public string Name { get; set; }
        public int ExperienceYears { get; set; }
        public int SoldCarsCount { get; private set; }

        // 2. Конструктор с параметрами
        public Manager(string name, int experienceYears)
        {
            Name = name;
            ExperienceYears = experienceYears;
        }

        // 17. Композиция (Manager использует Vehicle)
        public void SellCar(T vehicle)
        {
            SoldCarsCount++;
            Console.WriteLine($"\nМенеджер {Name} продал {vehicle.GetVehicleType()}: {vehicle.Model}");
        }

        // 10. Переопределение метода
        public override string ToString()
        {
            return $"{Name} (опыт: {ExperienceYears} лет, продано: {SoldCarsCount})";
        }
    }
}
