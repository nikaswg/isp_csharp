using Lunopark.Core.Entities;
using Lunopark.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Lunopark.Services
{
    public class EmployeeService
    {
        private readonly UnitOfWork _unitOfWork;

        public EmployeeService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // CRUD операции
        public List<Employee> GetAllEmployees()
        {
            return _unitOfWork.Employees.GetAll().ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return _unitOfWork.Employees.GetById(id);
        }

        public int AddEmployee(Employee employee)
        {
            ValidateEmployee(employee);
            return _unitOfWork.Employees.Create(employee);
        }

        public bool UpdateEmployee(Employee employee)
        {
            ValidateEmployee(employee);
            return _unitOfWork.Employees.Update(employee);
        }

        public bool DeleteEmployee(int id)
        {
            // Проверка, не является ли сотрудник ответственным за аттракционы
            var attractions = _unitOfWork.Attractions.GetAll()
                .Where(a => a.ResponsibleEmployeeId == id).ToList();

            if (attractions.Any())
            {
                throw new InvalidOperationException(
                    "Нельзя удалить сотрудника, так как он ответственный за аттракционы: " +
                    string.Join(", ", attractions.Select(a => a.AttractionName)));
            }

            return _unitOfWork.Employees.Delete(id);
        }

        private void ValidateEmployee(Employee employee)
        {
            if (string.IsNullOrWhiteSpace(employee.FullName))
                throw new ArgumentException("ФИО сотрудника не может быть пустым");

            if (employee.BirthYear.HasValue &&
                (employee.BirthYear < 1900 || employee.BirthYear > DateTime.Now.Year - 16))
                throw new ArgumentException("Некорректный год рождения");

            if (string.IsNullOrWhiteSpace(employee.Position))
                throw new ArgumentException("Должность не может быть пустой");
        }

        // Поиск и фильтрация
        public List<Employee> SearchEmployees(string searchTerm)
        {
            return _unitOfWork.Employees.GetAll()
                .Where(e => e.FullName.Contains(searchTerm) ||
                           e.Position.Contains(searchTerm) ||
                           (e.Address != null && e.Address.Contains(searchTerm)))
                .ToList();
        }

        public List<Employee> FilterEmployees(int? minBirthYear, int? maxBirthYear, string position)
        {
            var query = _unitOfWork.Employees.GetAll().AsQueryable();

            if (minBirthYear.HasValue)
                query = query.Where(e => e.BirthYear >= minBirthYear);

            if (maxBirthYear.HasValue)
                query = query.Where(e => e.BirthYear <= maxBirthYear);

            if (!string.IsNullOrEmpty(position))
                query = query.Where(e => e.Position == position);

            return query.ToList();
        }

        // Сортировка
        public List<Employee> SortEmployeesByName(bool ascending = true)
        {
            var employees = _unitOfWork.Employees.GetAll();
            return ascending ?
                employees.OrderBy(e => e.FullName).ToList() :
                employees.OrderByDescending(e => e.FullName).ToList();
        }

        public List<Employee> SortEmployeesByBirthYear(bool ascending = true)
        {
            var employees = _unitOfWork.Employees.GetAll();
            return ascending ?
                employees.OrderBy(e => e.BirthYear).ToList() :
                employees.OrderByDescending(e => e.BirthYear).ToList();
        }

        public IEnumerable<Employee> SearchEmployees(string namePart, string positionPart, int? birthYear)
        {
            var query = _unitOfWork.Employees.GetAll();

            if (!string.IsNullOrEmpty(namePart))
                query = query.Where(e => e.FullName.Contains(namePart));

            if (!string.IsNullOrEmpty(positionPart))
                query = query.Where(e => e.Position.Contains(positionPart));

            if (birthYear.HasValue)
                query = query.Where(e => e.BirthYear == birthYear);

            return query.ToList();
        }
    }
}