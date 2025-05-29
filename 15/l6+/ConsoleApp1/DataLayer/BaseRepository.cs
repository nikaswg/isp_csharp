using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Lunopark.Data.Repositories
{
    public abstract class BaseRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        protected BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        protected SqlConnection GetConnection()
        {
            return _context.GetConnection();
        }

        protected abstract T MapDataReaderToEntity(SqlDataReader reader);

        protected IEnumerable<T> ExecuteReader(string storedProcedure, Action<SqlCommand> configureCommand)
        {
            var entities = new List<T>();
            using (var command = new SqlCommand(storedProcedure, GetConnection()))
            {
                command.CommandType = CommandType.StoredProcedure;
                configureCommand?.Invoke(command);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        entities.Add(MapDataReaderToEntity(reader));
                    }
                }
            }
            return entities;
        }

        protected T ExecuteSingleReader(string storedProcedure, Action<SqlCommand> configureCommand)
        {
            using (var command = new SqlCommand(storedProcedure, GetConnection()))
            {
                command.CommandType = CommandType.StoredProcedure;
                configureCommand?.Invoke(command);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapDataReaderToEntity(reader);
                    }
                }
            }
            return null;
        }

        protected int ExecuteNonQuery(string storedProcedure, Action<SqlCommand> configureCommand)
        {
            using (var command = new SqlCommand(storedProcedure, GetConnection()))
            {
                command.CommandType = CommandType.StoredProcedure;
                configureCommand?.Invoke(command);
                return command.ExecuteNonQuery();
            }
        }

        protected object ExecuteScalar(string storedProcedure, Action<SqlCommand> configureCommand)
        {
            using (var command = new SqlCommand(storedProcedure, GetConnection()))
            {
                command.CommandType = CommandType.StoredProcedure;
                configureCommand?.Invoke(command);
                return command.ExecuteScalar();
            }
        }
    }
}