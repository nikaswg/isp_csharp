using Lunopark.Core.Entities;
using Lunopark.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lunopark.Services
{
    public class AttractionService
    {
        private readonly UnitOfWork _unitOfWork;

        public AttractionService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // CRUD операции
        public List<Attraction> GetAllAttractions()
        {
            return _unitOfWork.Attractions.GetAll().ToList();
        }

        public Attraction GetAttractionById(int id)
        {
            return _unitOfWork.Attractions.GetById(id);
        }

        public int AddAttraction(Attraction attraction)
        {
            ValidateAttraction(attraction);
            return _unitOfWork.Attractions.Create(attraction);
        }

        public bool UpdateAttraction(Attraction attraction)
        {
            ValidateAttraction(attraction);
            return _unitOfWork.Attractions.Update(attraction);
        }

        public bool DeleteAttraction(int id)
        {
            // Проверка, нет ли связанных билетов
            var tickets = _unitOfWork.Tickets.GetAll().Where(t => t.AttractionId == id).ToList();
            if (tickets.Any())
            {
                throw new InvalidOperationException(
                    "Нельзя удалить аттракцион, так как есть связанные билеты");
            }

            return _unitOfWork.Attractions.Delete(id);
        }

        private void ValidateAttraction(Attraction attraction)
        {
            if (string.IsNullOrWhiteSpace(attraction.AttractionName))
                throw new ArgumentException("Название аттракциона не может быть пустым");

            if (attraction.InstallationYear < 1900 || attraction.InstallationYear > DateTime.Now.Year)
                throw new ArgumentException("Некорректный год установки");
        }

        // Получение аттракционов по ответственному сотруднику
        public List<Attraction> GetAttractionsByEmployee(int employeeId)
        {
            return _unitOfWork.Attractions.GetAll()
                .Where(a => a.ResponsibleEmployeeId == employeeId)
                .ToList();
        }

        // Получение списка аттракционов для ComboBox
        public Dictionary<int, string> GetAttractionsForComboBox()
        {
            return _unitOfWork.Attractions.GetAll()
                .ToDictionary(a => a.AttractionId, a => a.AttractionName);
        }
    }
}