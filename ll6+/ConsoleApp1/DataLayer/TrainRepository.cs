using RailwayTransport.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RailwayTransport.Data.Repositories
{
    public class TrainRepository : BaseRepository<Train>, ITrainRepository
    {
        public TrainRepository(AppDbContext context) : base(context)
        {
        }

        protected override Train MapDataReaderToEntity(SqlDataReader reader)
        {
            return new Train
            {
                TrainId = reader.GetInt32(reader.GetOrdinal("TrainID")),
                TrainNumber = reader.GetString(reader.GetOrdinal("TrainNumber")),
                TrainType = reader.GetString(reader.GetOrdinal("TrainType")),
                DeparturePoint = reader.GetString(reader.GetOrdinal("DeparturePoint")),
                DestinationPoint = reader.GetString(reader.GetOrdinal("DestinationPoint")),
                DepartureDate = reader.GetDateTime(reader.GetOrdinal("DepartureDate"))
            };
        }

        public IEnumerable<Train> GetAll()
        {
            return ExecuteReader("sp_GetAllTrains", null);
        }

        public Train GetById(int id)
        {
            return ExecuteSingleReader("sp_GetTrainById", cmd =>
            {
                cmd.Parameters.AddWithValue("@TrainID", id);
            });
        }

        public int Create(Train train)
        {
            return Convert.ToInt32(ExecuteScalar("sp_AddTrain", cmd =>
            {
                cmd.Parameters.AddWithValue("@TrainNumber", train.TrainNumber);
                cmd.Parameters.AddWithValue("@TrainType", train.TrainType);
                cmd.Parameters.AddWithValue("@DeparturePoint", train.DeparturePoint);
                cmd.Parameters.AddWithValue("@DestinationPoint", train.DestinationPoint);
                cmd.Parameters.AddWithValue("@DepartureDate", train.DepartureDate);
            }));
        }

        public bool Update(Train train)
        {
            return ExecuteNonQuery("sp_UpdateTrain", cmd =>
            {
                cmd.Parameters.AddWithValue("@TrainID", train.TrainId);
                cmd.Parameters.AddWithValue("@TrainNumber", train.TrainNumber);
                cmd.Parameters.AddWithValue("@TrainType", train.TrainType);
                cmd.Parameters.AddWithValue("@DeparturePoint", train.DeparturePoint);
                cmd.Parameters.AddWithValue("@DestinationPoint", train.DestinationPoint);
                cmd.Parameters.AddWithValue("@DepartureDate", train.DepartureDate);
            }) > 0;
        }

        public bool Delete(int id)
        {
            return ExecuteNonQuery("sp_DeleteTrain", cmd =>
            {
                cmd.Parameters.AddWithValue("@TrainID", id);
            }) > 0;
        }
    }
}