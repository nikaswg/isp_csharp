namespace UniversitySystem
{
    // 12. Обобщенный класс
    // 14. Наследование обобщений
    public class Course<T> where T : Professor
    {
        // 2. Конструктор с параметрами
        public Course(string name, T instructor)
        {
            Name = name;
            Instructor = instructor;
        }

        // 3. Свойства
        public string Name { get; set; }
        public T Instructor { get; set; }
    }
}