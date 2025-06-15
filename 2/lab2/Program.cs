using System;
using System.Collections.Generic;
using UniversitySystem;

namespace UniversityApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("=== Университетская система ===");

                var university = new University("Национальный Технический Университет", 1920);

                // Добавляем факультеты
                university.AddDepartment(new Department("Компьютерные Науки", "КН-100"));
                university.AddDepartment(new Department("Математика", "МТ-200"));

                // Добавляем людей
                university.AddPerson(new Professor("Иван Петров", "д.т.н.", 75000));
                university.AddPerson(new Professor("Мария Сидорова", "к.т.н.", 65000));
                university += new Student("Алексей Иванов", 3, "КН-101");
                university += new Student("Елена Сергеева", 2, "МТ-201");

                // Устанавливаем ректора
                university.RectorName = "Профессор Василий Кузнецов";

                // Демонстрация работы системы
                DemonstrateUniversitySystem(university);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }

        static void DemonstrateUniversitySystem(University university)
        {
            // Вывод основной информации
            Console.WriteLine($"\nУниверситет: {university.Name}");
            Console.WriteLine($"Год основания: {university.FoundingYear}");
            Console.WriteLine($"Ректор: {university.RectorName}");

            // Использование индексатора (после проверки)
            if (university.PeopleCount > 0)
            {
                Console.WriteLine($"\nПервый человек в списке: {university[0].Name}");
            }

            // Обобщенный метод с явным указанием типа
            var professors = university.GetItemsByCondition<Professor>(p => true);
            Console.WriteLine($"\nКоличество преподавателей: {professors.Count()}");

            var students = university.GetItemsByCondition<Student>(s => s.Year > 1);
            Console.WriteLine("\nСтуденты старше 1 курса:");
            foreach (var s in students)
            {
                Console.WriteLine($"- {s.Name}, {s.Year} курс");
            }

            // Метод расширения
            Console.WriteLine($"\nОбщая зарплата преподавателей: {university.CalculateTotalSalary()}");

            // Использование интерфейса
            IUniversityService service = university;
            service.ShowInfo();
        }
    }
}