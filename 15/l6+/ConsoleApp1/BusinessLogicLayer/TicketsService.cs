using Lunopark.Core.Entities;
using Lunopark.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lunopark.Services
{
    public class TicketService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly AppService _appService;

        public TicketService(UnitOfWork unitOfWork, AppService appService)
        {
            _unitOfWork = unitOfWork;
            _appService = appService;
        }

        // CRUD операции
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

        private void ValidateTicket(Ticket ticket)
        {
            if (string.IsNullOrWhiteSpace(ticket.TicketNumber))
                throw new ArgumentException("Номер билета не может быть пустым");

            if (ticket.TicketPrice <= 0)
                throw new ArgumentException("Цена билета должна быть положительной");

            if (ticket.SaleDate > DateTime.Now)
                throw new ArgumentException("Дата продажи не может быть в будущем");
        }

        // Получение билетов за период
        public List<Ticket> GetTicketsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _unitOfWork.Tickets.GetAll()
                .Where(t => t.SaleDate >= startDate && t.SaleDate <= endDate)
                .ToList();
        }

        // Получение сотрудников для ComboBox
        public Dictionary<int, string> GetEmployeesForComboBox()
        {
            return _unitOfWork.Employees.GetAll()
                .ToDictionary(e => e.EmployeeId, e => e.FullName);
        }

        //public IEnumerable<Ticket> GetTicketsByDateRange(DateTime startDate, DateTime endDate)
        //{
        //    return _appService.GetTicketsWithDetails()
        //        .Where(t => t.SaleDate >= startDate && t.SaleDate <= endDate)
        //        .ToList();
        //}
    }
}