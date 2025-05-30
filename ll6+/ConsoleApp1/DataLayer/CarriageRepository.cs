using Lunopark.Core.Entities;
using RailwayTransport.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RailwayTransport.Data.Repositories
{
    public class CarriageRepository : BaseRepository<RailwayTransport.Core.Entities.Carriage>, ICarriageRepository
    {
        public CarriageRepository(AppDbContext context) : base(context)
        {
        }

        protected override RailwayTransport.Core.Entities.Carriage MapDataReaderToEntity(SqlDataReader reader)
        {
            return new RailwayTransport.Core.Entities.Carriage
            {
                CarriageId = reader.GetInt32(reader.GetOrdinal("CarriageID")),
                CarriageNumber = reader.GetString(reader.GetOrdinal("CarriageNumber")),
                CarriageType = reader.GetString(reader.GetOrdinal("CarriageType")),
                SeatCount = reader.GetInt32(reader.GetOrdinal("SeatCount")),
                SeatPrice = reader.GetDecimal(reader.GetOrdinal("SeatPrice")),
                TrainId = reader.GetInt32(reader.GetOrdinal("TrainID"))
            };
        }

        public IEnumerable<RailwayTransport.Core.Entities.Carriage> GetAll()
        {
            return ExecuteReader("sp_GetAllCarriages", null);
        }

        public RailwayTransport.Core.Entities.Carriage GetById(int id)
        {
            return ExecuteSingleReader("sp_GetCarriageById", cmd =>
            {
                cmd.Parameters.AddWithValue("@CarriageID", id);
            });
        }

        public int Create(RailwayTransport.Core.Entities.Carriage carriage)
        {
            return Convert.ToInt32(ExecuteScalar("sp_AddCarriage", cmd =>
            {
                cmd.Parameters.AddWithValue("@CarriageNumber", carriage.CarriageNumber);
                cmd.Parameters.AddWithValue("@CarriageType", carriage.CarriageType);
                cmd.Parameters.AddWithValue("@SeatCount", carriage.SeatCount);
                cmd.Parameters.AddWithValue("@SeatPrice", carriage.SeatPrice);
                cmd.Parameters.AddWithValue("@TrainID", carriage.TrainId);
            }));
        }

        public bool Update(RailwayTransport.Core.Entities.Carriage carriage)
        {
            return ExecuteNonQuery("sp_UpdateCarriage", cmd =>
            {
                cmd.Parameters.AddWithValue("@CarriageID", carriage.CarriageId);
                cmd.Parameters.AddWithValue("@CarriageNumber", carriage.CarriageNumber);
                cmd.Parameters.AddWithValue("@CarriageType", carriage.CarriageType);
                cmd.Parameters.AddWithValue("@SeatCount", carriage.SeatCount);
                cmd.Parameters.AddWithValue("@SeatPrice", carriage.SeatPrice);
                cmd.Parameters.AddWithValue("@TrainID", carriage.TrainId);
            }) > 0;
        }

        public bool Delete(int id)
        {
            return ExecuteNonQuery("sp_DeleteCarriage", cmd =>
            {
                cmd.Parameters.AddWithValue("@CarriageID", id);
            }) > 0;
        }

        public IEnumerable<RailwayTransport.Core.Entities.Carriage> GetByTrain(int trainId)
        {
            return ExecuteReader("sp_GetCarriagesByTrain", cmd =>
            {
                cmd.Parameters.AddWithValue("@TrainID", trainId);
            });
        }
    }
}