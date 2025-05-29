
using Lunopark.Core.Entities;
using Lunopark.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lunopark.Services
{
    public class AppService : IDisposable
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly AppService _appService;

        public EmployeeService Employees { get; }
        public AttractionService Attractions { get; }
        public TicketService Tickets { get; }
        public ReportService Reports { get; }

        public AppService()
        {
            _unitOfWork = new UnitOfWork(new AppDbContext());
            Employees = new EmployeeService(_unitOfWork);
            Attractions = new AttractionService(_unitOfWork);
            Tickets = new TicketService(_unitOfWork, _appService);
            Reports = new ReportService(_unitOfWork);
        }

        // Методы для работы с ComboBox
        public Dictionary<int, string> GetEmployeesDictionary()
        {
            return _unitOfWork.Employees.GetAll()
                .ToDictionary(e => e.EmployeeId, e => e.FullName);
        }

        public Dictionary<int, string> GetAttractionsDictionary()
        {
            return _unitOfWork.Attractions.GetAll()
                .ToDictionary(a => a.AttractionId, a => a.AttractionName);
        }

        // Комплексные операции
        public List<Ticket> GetTicketsWithDetails()
        {
            var tickets = _unitOfWork.Tickets.GetAll().ToList();
            var attractions = GetAttractionsDictionary();
            var employees = GetEmployeesDictionary();

            foreach (var ticket in tickets)
            {
                ticket.AttractionName = attractions.ContainsKey(ticket.AttractionId) ?
                    attractions[ticket.AttractionId] : "Неизвестно";

                if (ticket.EmployeeId.HasValue && employees.ContainsKey(ticket.EmployeeId.Value))
                {
                    ticket.EmployeeName = employees[ticket.EmployeeId.Value];
                }
                else
                {
                    ticket.EmployeeName = "Не указан";
                }
            }

            return tickets;
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }
    }
}