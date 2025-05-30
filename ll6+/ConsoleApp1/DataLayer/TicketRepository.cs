using RailwayTransport.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RailwayTransport.Data.Repositories
{
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(AppDbContext context) : base(context)
        {
        }

        protected override Ticket MapDataReaderToEntity(SqlDataReader reader)
        {
            return new Ticket
            {
                TicketId = reader.GetInt32(reader.GetOrdinal("TicketID")),
                PassengerName = reader.GetString(reader.GetOrdinal("PassengerName")),
                TrainId = reader.GetInt32(reader.GetOrdinal("TrainID")),
                CarriageId = reader.GetInt32(reader.GetOrdinal("CarriageID")),
                SeatNumber = reader.GetInt32(reader.GetOrdinal("SeatNumber")),
                SaleDate = reader.GetDateTime(reader.GetOrdinal("SaleDate"))
            };
        }

        public IEnumerable<Ticket> GetAll()
        {
            return ExecuteReader("sp_GetAllTickets", null);
        }

        public Ticket GetById(int id)
        {
            return ExecuteSingleReader("sp_GetTicketById", cmd =>
            {
                cmd.Parameters.AddWithValue("@TicketID", id);
            });
        }

        public int Create(Ticket ticket)
        {
            return Convert.ToInt32(ExecuteScalar("sp_AddTicket", cmd =>
            {
                cmd.Parameters.AddWithValue("@PassengerName", ticket.PassengerName);
                cmd.Parameters.AddWithValue("@TrainID", ticket.TrainId);
                cmd.Parameters.AddWithValue("@CarriageID", ticket.CarriageId);
                cmd.Parameters.AddWithValue("@SeatNumber", ticket.SeatNumber);
                cmd.Parameters.AddWithValue("@SaleDate", ticket.SaleDate);
            }));
        }

        public bool Update(Ticket ticket)
        {
            return ExecuteNonQuery("sp_UpdateTicket", cmd =>
            {
                cmd.Parameters.AddWithValue("@TicketID", ticket.TicketId);
                cmd.Parameters.AddWithValue("@PassengerName", ticket.PassengerName);
                cmd.Parameters.AddWithValue("@TrainID", ticket.TrainId);
                cmd.Parameters.AddWithValue("@CarriageID", ticket.CarriageId);
                cmd.Parameters.AddWithValue("@SeatNumber", ticket.SeatNumber);
                cmd.Parameters.AddWithValue("@SaleDate", ticket.SaleDate);
            }) > 0;
        }

        public bool Delete(int id)
        {
            return ExecuteNonQuery("sp_DeleteTicket", cmd =>
            {
                cmd.Parameters.AddWithValue("@TicketID", id);
            }) > 0;
        }

        public IEnumerable<Ticket> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return ExecuteReader("sp_GetTicketsByDateRange", cmd =>
            {
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);
            });
        }
    }
}