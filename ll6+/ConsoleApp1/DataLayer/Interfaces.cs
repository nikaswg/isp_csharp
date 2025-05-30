using Lunopark.Core.Entities;
using RailwayTransport.Core.Entities;
using System.Collections.Generic;

namespace RailwayTransport.Data.Repositories
{
    public interface ITrainRepository
    {
        IEnumerable<Train> GetAll();
        Train GetById(int id);
        int Create(Train train);
        bool Update(Train train);
        bool Delete(int id);
    }

    public interface ICarriageRepository
    {
        IEnumerable<RailwayTransport.Core.Entities.Carriage> GetAll();
        RailwayTransport.Core.Entities.Carriage GetById(int id);
        int Create(RailwayTransport.Core.Entities.Carriage carriage);
        bool Update(RailwayTransport.Core.Entities.Carriage carriage);
        bool Delete(int id);
        IEnumerable<RailwayTransport.Core.Entities.Carriage> GetByTrain(int trainId);
    }

    public interface ITicketRepository
    {
        IEnumerable<Ticket> GetAll();
        Ticket GetById(int id);
        int Create(Ticket ticket);
        bool Update(Ticket ticket);
        bool Delete(int id);
        IEnumerable<Ticket> GetByDateRange(DateTime startDate, DateTime endDate);
    }
}