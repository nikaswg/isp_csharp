using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversitySystem
{
    public abstract class EducationalInstitution
    {
        public abstract string GetInstitutionType();
    }

    public class University : EducationalInstitution, IUniversityService
    {
        protected decimal _budget = 5000000;
        private List<Department> _departments = new List<Department>();
        private List<Person> _people = new List<Person>();

        public University(string name, int foundingYear)
        {
            Name = name;
            FoundingYear = foundingYear;
        }

        public string Name { get; set; }
        public int FoundingYear { get; private set; }
        public int DepartmentsCount => _departments.Count;
        public int PeopleCount => _people.Count;

        private string _rectorName;
        public string RectorName
        {
            get => _rectorName ?? "Не назначен";
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Имя ректора не может быть пустым");
                _rectorName = value;
            }
        }

        public Person this[int index]
        {
            get
            {
                if (index < 0 || index >= _people.Count)
                    throw new IndexOutOfRangeException("Индекс вне диапазона");
                return _people[index];
            }
            set
            {
                if (index < 0 || index >= _people.Count)
                    throw new IndexOutOfRangeException("Индекс вне диапазона");
                _people[index] = value;
            }
        }

        public void AddDepartment(Department department)
        {
            if (department == null)
                throw new ArgumentNullException(nameof(department));
            _departments.Add(department);
        }

        public void AddPerson(Person person)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person));
            _people.Add(person);
        }

        public static University operator +(University university, Student student)
        {
            university.AddPerson(student);
            return university;
        }

        public IEnumerable<T> GetItemsByCondition<T>(Func<T, bool> condition) where T : Person
        {
            return _people.OfType<T>().Where(condition);
        }

        public override string GetInstitutionType() => "Университет";

        public void ShowInfo()
        {
            Console.WriteLine($"\nИнформация об университете: {Name}, {FoundingYear} год");
            Console.WriteLine($"Количество факультетов: {DepartmentsCount}");
            Console.WriteLine($"Количество сотрудников и студентов: {PeopleCount}");
        }
    }

    public static class UniversityExtensions
    {
        public static decimal CalculateTotalSalary(this University university)
        {
            return university.GetItemsByCondition<Professor>(p => true).Sum(p => p.Salary);
        }
    }
}