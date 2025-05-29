using System;
using Lunopark.Data.Repositories;

namespace Lunopark.Data
{
    public class UnitOfWork : IDisposable
    {
        private readonly AppDbContext _context;
        private bool _disposed;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Employees = new EmployeeRepository(_context);
            Attractions = new AttractionRepository(_context);
            Tickets = new TicketRepository(_context);
        }

        public EmployeeRepository Employees { get; }
        public AttractionRepository Attractions { get; }
        public TicketRepository Tickets { get; }

        public int Complete()
        {
            // В ADO.NET можно реализовать транзакции здесь
            return 1;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }
                _disposed = true;
            }
        }
    }
}