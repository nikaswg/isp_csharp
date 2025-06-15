namespace UniversitySystem
{
    public class Department
    {
        // 2. Конструктор с параметрами
        public Department(string name, string code)
        {
            Name = name;
            Code = code;
        }

        // 3. Свойства
        public string Name { get; set; }
        public string Code { get; set; }

        // 4. Свойство с логикой
        private string _headName;
        public string HeadName
        {
            get => _headName ?? "Не назначен";
            set => _headName = value;
        }

        // 10. Переопределение метода
        public override string ToString()
        {
            return $"{Name} ({Code})";
        }
    }
}