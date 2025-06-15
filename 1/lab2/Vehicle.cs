namespace CarShowroom
{
    // 6. Абстрактный класс
    public abstract class Vehicle : IVehicleDisplay
    {
        // 7. Инкапсуляция (protected поле)
        protected string _model;

        // 3. Свойство с логикой
        public string Model
        {
            get => _model;
            set => _model = string.IsNullOrWhiteSpace(value) ? "Unknown" : value;
        }

        // 6. Абстрактный член класса
        public abstract string GetVehicleType();

        // 18. Реализация интерфейса
        public abstract void DisplayInfo();
    }
}
