namespace UniversitySystem
{
    public class Professor : Person
    {
        // 8. Статическое поле
        public static int TotalProfessors;

        // 2. Конструктор с параметрами
        public Professor(string name, string degree, decimal salary) : base(name)
        {
            Degree = degree;
            Salary = salary;
            TotalProfessors++;
        }

        // 3. Свойства
        public string Degree { get; set; }
        public decimal Salary { get; set; }

        // 6. Реализация абстрактного метода
        public override string GetRole() => "Преподаватель";

        // 10. Переопределение метода
        public override string ToString()
        {
            return $"{Name}, {Degree}";
        }
    }
}