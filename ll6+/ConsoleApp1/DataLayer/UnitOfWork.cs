using System;
using RailwayTransport.Data.Repositories;

namespace RailwayTransport.Data
{
    public class UnitOfWork : IDisposable
    {
        private readonly AppDbContext _context;
        private bool _disposed;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Trains = new TrainRepository(_context);
            Carriages = new CarriageRepository(_context);
            Tickets = new TicketRepository(_context);
        }

        public TrainRepository Trains { get; }
        public CarriageRepository Carriages { get; }
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