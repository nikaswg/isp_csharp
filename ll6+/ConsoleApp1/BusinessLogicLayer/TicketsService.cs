using RailwayTransport.Core.Entities;
using RailwayTransport.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RailwayTransport.Services
{
    public class TicketService
    {
        private readonly UnitOfWork _unitOfWork;

        public TicketService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Ticket> GetAllTickets()
        {
            return _unitOfWork.Tickets.GetAll().ToList();
        }

        public Ticket GetTicketById(int id)
        {
            return _unitOfWork.Tickets.GetById(id);
        }

        public int AddTicket(Ticket ticket)
        {
            ValidateTicket(ticket);

            // Проверка доступности места
            var existingTicket = _unitOfWork.Tickets.GetAll()
                .FirstOrDefault(t => t.CarriageId == ticket.CarriageId && t.SeatNumber == ticket.SeatNumber);

            if (existingTicket != null)
            {
                throw new InvalidOperationException("Это место уже занято");
            }

            ticket.SaleDate = DateTime.Now;
            return _unitOfWork.Tickets.Create(ticket);
        }

        public bool UpdateTicket(Ticket ticket)
        {
            ValidateTicket(ticket);
            return _unitOfWork.Tickets.Update(ticket);
        }

        public bool DeleteTicket(int id)
        {
            return _unitOfWork.Tickets.Delete(id);
        }

        public List<Ticket> GetTicketsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _unitOfWork.Tickets.GetByDateRange(startDate, endDate).ToList();
        }

        private void ValidateTicket(Ticket ticket)
        {
            if (string.IsNullOrWhiteSpace(ticket.PassengerName))
                throw new ArgumentException("Имя пассажира не может быть пустым");

            if (ticket.TrainId <= 0)
                throw new ArgumentException("Не указан поезд");

            if (ticket.CarriageId <= 0)
                throw new ArgumentException("Не указан вагон");

            if (ticket.SeatNumber <= 0)
                throw new ArgumentException("Номер места должен быть положительным");

            // Проверка существования поезда и вагона
            var train = _unitOfWork.Trains.GetById(ticket.TrainId);
            if (train == null)
                throw new ArgumentException("Указанный поезд не существует");

            var carriage = _unitOfWork.Carriages.GetById(ticket.CarriageId);
            if (carriage == null)
                throw new ArgumentException("Указанный вагон не существует");

            // Проверка что вагон принадлежит поезду
            if (carriage.TrainId != train.TrainId)
                throw new ArgumentException("Указанный вагон не принадлежит указанному поезду");

            // Проверка что номер места не превышает количество мест в вагоне
            if (ticket.SeatNumber > carriage.SeatCount)
                throw new ArgumentException("Номер места превышает количество мест в вагоне");
        }
    }
}