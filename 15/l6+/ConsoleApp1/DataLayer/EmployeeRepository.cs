using Lunopark.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Lunopark.Data.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {
        }

        protected override Employee MapDataReaderToEntity(SqlDataReader reader)
        {
            return new Employee
            {
                EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeID")),
                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader.GetString(reader.GetOrdinal("Phone")),
                BirthYear = reader.IsDBNull(reader.GetOrdinal("BirthYear")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("BirthYear")),
                Position = reader.GetString(reader.GetOrdinal("Position"))
            };
        }

        public IEnumerable<Employee> GetAll()
        {
            return ExecuteReader("sp_GetAllEmployees", null);
        }

        public Employee GetById(int id)
        {
            return ExecuteSingleReader("sp_GetEmployeeById", cmd =>
            {
                cmd.Parameters.AddWithValue("@EmployeeID", id);
            });
        }

        public int Create(Employee employee)
        {
            return Convert.ToInt32(ExecuteScalar("sp_AddEmployee", cmd =>
            {
                cmd.Parameters.AddWithValue("@FullName", employee.FullName);
                cmd.Parameters.AddWithValue("@Address", (object)employee.Address ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", (object)employee.Phone ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@BirthYear", (object)employee.BirthYear ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Position", employee.Position);
            }));
        }

        public bool Update(Employee employee)
        {
            return ExecuteNonQuery("sp_UpdateEmployee", cmd =>
            {
                cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeId);
                cmd.Parameters.AddWithValue("@FullName", employee.FullName);
                cmd.Parameters.AddWithValue("@Address", (object)employee.Address ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", (object)employee.Phone ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@BirthYear", (object)employee.BirthYear ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Position", employee.Position);
            }) > 0;
        }

        public bool Delete(int id)
        {
            return ExecuteNonQuery("sp_DeleteEmployee", cmd =>
            {
                cmd.Parameters.AddWithValue("@EmployeeID", id);
            }) > 0;
        }

        public IEnumerable<Employee> GetByPosition(string position)
        {
            return ExecuteReader("sp_GetEmployeesByPosition", cmd =>
            {
                cmd.Parameters.AddWithValue("@Position", position);
            });
        }
    }
}