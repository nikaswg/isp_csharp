using RailwayTransport.Core.Entities;
using RailwayTransport.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RailwayTransport.Services
{
    public class AppService : IDisposable
    {
        private readonly UnitOfWork _unitOfWork;

        public TrainService Trains { get; }
        public CarriageService Carriages { get; }
        public TicketService Tickets { get; }
        public ReportService Reports { get; }

        public AppService()
        {
            _unitOfWork = new UnitOfWork(new AppDbContext());
            Trains = new TrainService(_unitOfWork);
            Carriages = new CarriageService(_unitOfWork);
            Tickets = new TicketService(_unitOfWork);
            Reports = new ReportService(_unitOfWork);
        }

        public Dictionary<int, string> GetTrainsDictionary()
        {
            return Trains.GetTrainsDictionary();
        }

        public Dictionary<int, string> GetCarriagesDictionary(int trainId)
        {
            return _unitOfWork.Carriages.GetByTrain(trainId)
                .ToDictionary(c => c.CarriageId, c => $"{c.CarriageNumber} ({c.CarriageType})");
        }

        public List<Ticket> GetTicketsWithDetails()
        {
            var tickets = _unitOfWork.Tickets.GetAll().ToList();
            var trains = GetTrainsDictionary();

            foreach (var ticket in tickets)
            {
                if (trains.ContainsKey(ticket.TrainId))
                {
                    ticket.TrainNumber = trains[ticket.TrainId];
                }

                var carriage = _unitOfWork.Carriages.GetById(ticket.CarriageId);
                if (carriage != null)
                {
                    ticket.CarriageNumber = carriage.CarriageNumber;
                    ticket.SeatPrice = carriage.SeatPrice;
                }
            }

            return tickets;
        }

        public List<Ticket> GetTicketsWithDetails(IEnumerable<Ticket> tickets)
        {
            var ticketsList = tickets.ToList();
            var trains = GetTrainsDictionary();

            foreach (var ticket in ticketsList)
            {
                if (trains.ContainsKey(ticket.TrainId))
                {
                    ticket.TrainNumber = trains[ticket.TrainId];
                }

                var carriage = _unitOfWork.Carriages.GetById(ticket.CarriageId);
                if (carriage != null)
                {
                    ticket.CarriageNumber = carriage.CarriageNumber;
                    ticket.SeatPrice = carriage.SeatPrice;
                }
            }

            return ticketsList;
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}