namespace UniversitySystem
{
    // 12. Обобщения (используется в производных классах)
    public abstract class Person
    {
        // 2. Конструктор с параметрами
        protected Person(string name)
        {
            Name = name;
        }

        // 3. Свойства
        public string Name { get; set; }

        // 6. Абстрактный член
        public abstract string GetRole();
    }
}