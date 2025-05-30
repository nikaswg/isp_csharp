using RailwayTransport.Core.Entities;
using RailwayTransport.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RailwayTransport.Services
{
    public class TrainService
    {
        private readonly UnitOfWork _unitOfWork;

        public TrainService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Train> GetAllTrains()
        {
            return _unitOfWork.Trains.GetAll().ToList();
        }

        public Train GetTrainById(int id)
        {
            return _unitOfWork.Trains.GetById(id);
        }

        public int AddTrain(Train train)
        {
            ValidateTrain(train);
            return _unitOfWork.Trains.Create(train);
        }

        public bool UpdateTrain(Train train)
        {
            ValidateTrain(train);
            return _unitOfWork.Trains.Update(train);
        }

        public bool DeleteTrain(int id)
        {
            // Проверка на связанные вагоны
            var carriages = _unitOfWork.Carriages.GetByTrain(id).ToList();
            if (carriages.Any())
            {
                throw new InvalidOperationException(
                    "Нельзя удалить поезд, так как к нему привязаны вагоны");
            }

            return _unitOfWork.Trains.Delete(id);
        }

        private void ValidateTrain(Train train)
        {
            if (string.IsNullOrWhiteSpace(train.TrainNumber))
                throw new ArgumentException("Номер поезда не может быть пустым");

            if (string.IsNullOrWhiteSpace(train.TrainType))
                throw new ArgumentException("Тип поезда не может быть пустым");

            if (string.IsNullOrWhiteSpace(train.DeparturePoint))
                throw new ArgumentException("Пункт отправления не может быть пустым");

            if (string.IsNullOrWhiteSpace(train.DestinationPoint))
                throw new ArgumentException("Пункт назначения не может быть пустым");

            if (train.DepartureDate < DateTime.Today)
                throw new ArgumentException("Дата отправления не может быть в прошлом");
        }

        public Dictionary<int, string> GetTrainsDictionary()
        {
            return _unitOfWork.Trains.GetAll()
                .ToDictionary(t => t.TrainId, t => $"{t.TrainNumber} ({t.DeparturePoint} - {t.DestinationPoint})");
        }
    }
}