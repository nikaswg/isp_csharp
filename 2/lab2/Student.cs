namespace UniversitySystem
{
    public class Student : Person
    {
        // 8. Статическое поле
        public static int TotalStudents;

        // 2. Конструктор с параметрами
        public Student(string name, int year, string group) : base(name)
        {
            Year = year;
            Group = group;
            TotalStudents++;
        }

        // 3. Свойства
        public int Year { get; set; }
        public string Group { get; set; }

        // 6. Реализация абстрактного метода
        public override string GetRole() => "Студент";

        // 10. Переопределение метода
        public override string ToString()
        {
            return $"{Name}, {Year} курс, группа {Group}";
        }
    }
}