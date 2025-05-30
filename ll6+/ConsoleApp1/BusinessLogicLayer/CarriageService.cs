using RailwayTransport.Core.Entities;
using RailwayTransport.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RailwayTransport.Services
{
    public class CarriageService
    {
        private readonly UnitOfWork _unitOfWork;

        public CarriageService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Carriage> GetAllCarriages()
        {
            return _unitOfWork.Carriages.GetAll();
        }

        public Carriage GetCarriageById(int id)
        {
            return _unitOfWork.Carriages.GetById(id);
        }

        public int AddCarriage(Carriage carriage)
        {
            ValidateCarriage(carriage);
            return _unitOfWork.Carriages.Create(carriage);
        }

        public bool UpdateCarriage(Carriage carriage)
        {
            ValidateCarriage(carriage);
            return _unitOfWork.Carriages.Update(carriage);
        }

        public bool DeleteCarriage(int id)
        {
            // Check for related tickets
            var tickets = _unitOfWork.Tickets.GetAll().Where(t => t.CarriageId == id).ToList();
            if (tickets.Any())
            {
                throw new InvalidOperationException(
                    "Cannot delete carriage because it has related tickets");
            }

            return _unitOfWork.Carriages.Delete(id);
        }

        public IEnumerable<Carriage> GetCarriagesByTrain(int trainId)
        {
            return _unitOfWork.Carriages.GetByTrain(trainId);
        }

        private void ValidateCarriage(Carriage carriage)
        {
            if (string.IsNullOrWhiteSpace(carriage.CarriageNumber))
                throw new ArgumentException("Carriage number cannot be empty");

            if (string.IsNullOrWhiteSpace(carriage.CarriageType))
                throw new ArgumentException("Carriage type cannot be empty");

            if (carriage.SeatCount <= 0)
                throw new ArgumentException("Seat count must be positive");

            if (carriage.SeatPrice <= 0)
                throw new ArgumentException("Seat price must be positive");

            if (carriage.TrainId <= 0)
                throw new ArgumentException("Invalid train reference");
        }
    }
}