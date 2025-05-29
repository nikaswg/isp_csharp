using Lunopark.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;

namespace Lunopark.Data.Repositories
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> GetAll();
        Ticket GetById(int id);
        int Create(Ticket ticket);
        bool Update(Ticket ticket);
        bool Delete(int id);
        IEnumerable<Ticket> GetByDateRange(DateTime startDate, DateTime endDate);
    }

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
                TicketNumber = reader.GetString(reader.GetOrdinal("TicketNumber")),
                AttractionId = reader.GetInt32(reader.GetOrdinal("AttractionID")),
                TicketPrice = reader.GetDecimal(reader.GetOrdinal("TicketPrice")),
                EmployeeId = reader.IsDBNull(reader.GetOrdinal("EmployeeID")) ?
                    (int?)null : reader.GetInt32(reader.GetOrdinal("EmployeeID")),
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
                cmd.Parameters.AddWithValue("@TicketNumber", ticket.TicketNumber);
                cmd.Parameters.AddWithValue("@AttractionID", ticket.AttractionId);
                cmd.Parameters.AddWithValue("@TicketPrice", ticket.TicketPrice);
                cmd.Parameters.AddWithValue("@EmployeeID", (object)ticket.EmployeeId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SaleDate", ticket.SaleDate);
            }));
        }

        public bool Update(Ticket ticket)
        {
            return ExecuteNonQuery("sp_UpdateTicket", cmd =>
            {
                cmd.Parameters.AddWithValue("@TicketID", ticket.TicketId);
                cmd.Parameters.AddWithValue("@TicketNumber", ticket.TicketNumber);
                cmd.Parameters.AddWithValue("@AttractionID", ticket.AttractionId);
                cmd.Parameters.AddWithValue("@TicketPrice", ticket.TicketPrice);
                cmd.Parameters.AddWithValue("@EmployeeID", (object)ticket.EmployeeId ?? DBNull.Value);
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