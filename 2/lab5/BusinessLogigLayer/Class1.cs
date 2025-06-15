using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer; // Подключаем пространство имен с моделями

namespace BusinessLogicLayer
{
    public class StudentService
    {
        private List<Student> students;
        private List<Group> groups;

        public StudentService()
        {
            students = new List<Student>
            {
                new Student { Name = "Алексей", Age = 20, Group = "ИС-21" },
                new Student { Name = "Мария", Age = 17, Group = "ИС-21" },
                new Student { Name = "Олег", Age = 22, Group = "ПМ-22" },
                new Student { Name = "Ирина", Age = 19, Group = "ПМ-22" },
                new Student { Name = "Дмитрий", Age = 23, Group = "ПИ-23" }
            };

            groups = new List<Group>
            {
                new Group { Name = "ИС-21", Faculty = "Информатика", YearFounded = 2015 },
                new Group { Name = "ПМ-22", Faculty = "Математика", YearFounded = 2018 },
                new Group { Name = "ПИ-23", Faculty = "Программирование", YearFounded = 2020 }
            };
        }

        public IEnumerable<Student> GetStudentsAboveAge(int age)
        {
            return students.Where(s => s.Age > age);
        }

        public IEnumerable<Student> GetStudentsByGroupAndAge(string group, int maxAge)
        {
            if (string.IsNullOrEmpty(group))
                return Enumerable.Empty<Student>();

            return students.Where(s => s.Group == group && s.Age < maxAge);
        }

        public IEnumerable<Student> GetSortedStudentsByName()
        {
            return students.OrderBy(s => s.Name);
        }

        public int GetStudentCountInGroup(string groupName)
        {
            return students.Count(s => s.Group == groupName);
        }

        public (int MaxAge, double AvgAge, int SumAge) GetStudentStatistics()
        {
            return (students.Max(s => s.Age), students.Average(s => s.Age), students.Sum(s => s.Age));
        }

        public IEnumerable<object> GetGroupAges()
        {
            return groups.Select(g => new { g.Name, YearsSinceFounded = DateTime.Now.Year - g.YearFounded });
        }

        public IEnumerable<IGrouping<string, Student>> GetStudentsGroupedByGroup()
        {
            return students.GroupBy(s => s.Group);
        }

        public IEnumerable<object> GetStudentsWithFaculty()
        {
            return students.Join(groups,
                student => student.Group,
                group => group.Name,
                (student, group) => new { student.Name, student.Age, group.Faculty });
        }

        public IEnumerable<object> GetStudentsGroupedByFaculty()
        {
            return groups.GroupJoin(students,
                group => group.Name,
                student => student.Group,
                (group, studentGroup) => new { group.Faculty, Students = studentGroup });
        }

        public bool AreAllStudentsAboveAge(int age)
        {
            return students.All(s => s.Age > age);
        }

        public bool IsAnyStudentInGroup(string groupName)
        {
            return students.Any(s => s.Group == groupName);
        }
    }
}