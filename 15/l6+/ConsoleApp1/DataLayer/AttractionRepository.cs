using Lunopark.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Lunopark.Data.Repositories
{
    public interface IAttractionRepository
    {
        IEnumerable<Attraction> GetAll();
        Attraction GetById(int id);
        int Create(Attraction attraction);
        bool Update(Attraction attraction);
        bool Delete(int id);
        IEnumerable<Attraction> GetByEmployee(int employeeId);
    }

    public class AttractionRepository : BaseRepository<Attraction>, IAttractionRepository
    {
        public AttractionRepository(AppDbContext context) : base(context)
        {
        }

        protected override Attraction MapDataReaderToEntity(SqlDataReader reader)
        {
            return new Attraction
            {
                AttractionId = reader.GetInt32(reader.GetOrdinal("AttractionID")),
                AttractionName = reader.GetString(reader.GetOrdinal("AttractionName")),
                InstallationYear = reader.GetInt32(reader.GetOrdinal("InstallationYear")),
                ResponsibleEmployeeId = reader.IsDBNull(reader.GetOrdinal("ResponsibleEmployeeID")) ?
                    (int?)null : reader.GetInt32(reader.GetOrdinal("ResponsibleEmployeeID"))
            };
        }

        public IEnumerable<Attraction> GetAll()
        {
            return ExecuteReader("sp_GetAllAttractions", null);
        }

        public Attraction GetById(int id)
        {
            return ExecuteSingleReader("sp_GetAttractionById", cmd =>
            {
                cmd.Parameters.AddWithValue("@AttractionID", id);
            });
        }

        public int Create(Attraction attraction)
        {
            return Convert.ToInt32(ExecuteScalar("sp_AddAttraction", cmd =>
            {
                cmd.Parameters.AddWithValue("@AttractionName", attraction.AttractionName);
                cmd.Parameters.AddWithValue("@InstallationYear", attraction.InstallationYear);
                cmd.Parameters.AddWithValue("@ResponsibleEmployeeID",
                    (object)attraction.ResponsibleEmployeeId ?? DBNull.Value);
            }));
        }

        public bool Update(Attraction attraction)
        {
            return ExecuteNonQuery("sp_UpdateAttraction", cmd =>
            {
                cmd.Parameters.AddWithValue("@AttractionID", attraction.AttractionId);
                cmd.Parameters.AddWithValue("@AttractionName", attraction.AttractionName);
                cmd.Parameters.AddWithValue("@InstallationYear", attraction.InstallationYear);
                cmd.Parameters.AddWithValue("@ResponsibleEmployeeID",
                    (object)attraction.ResponsibleEmployeeId ?? DBNull.Value);
            }) > 0;
        }

        public bool Delete(int id)
        {
            return ExecuteNonQuery("sp_DeleteAttraction", cmd =>
            {
                cmd.Parameters.AddWithValue("@AttractionID", id);
            }) > 0;
        }

        public IEnumerable<Attraction> GetByEmployee(int employeeId)
        {
            return ExecuteReader("sp_GetAttractionsByEmployee", cmd =>
            {
                cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
            });
        }
    }
}