using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Domain.Constants;
using Vinculacion.Persistence.Base;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Repositories
{
    public class QueryRepository(VinculacionContext vinculacionContext) : IQueryRepository
    {

        private readonly VinculacionContext _context = vinculacionContext;

        private static SqlParameter[] GetSqlParameters(Dictionary<string, object?> parameters)
        {
            var sqlParameters = new List<SqlParameter>();

            foreach (var param in parameters)
            {
                var sqlParameter = new SqlParameter
                {
                    ParameterName = $"@{param.Key}",
                    Value = param.Value ?? DBNull.Value
                };

                sqlParameters.Add(sqlParameter);
            }

            return [.. sqlParameters];
        }

        private static SqlParameter[] GetSqlParameters(IEnumerable<StoredProcedureParameter> parameters)
        {
            var sqlParameters = new List<SqlParameter>();

            foreach (var param in parameters)
            {
                var sqlParameter = new SqlParameter
                {
                    ParameterName = $"@{param.Name}",
                    Value = param.Value ?? DBNull.Value
                };

                if (param.IsStructured)
                {
                    sqlParameter.SqlDbType = SqlDbType.Structured;
                    sqlParameter.TypeName = param.UdttName;
                }

                sqlParameters.Add(sqlParameter);
            }

            return [.. sqlParameters];
        }

        public async Task<IEnumerable<TDestination>> ExecuteQuery<TDestination>(string query, Dictionary<string, object?> parameters, CancellationToken cancellationToken) where TDestination : new()
        {
            if (string.IsNullOrEmpty(query))
                throw new ArgumentException("The query must be provided.", nameof(query));

            using var command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            command.Parameters.AddRange(GetSqlParameters(parameters));

            if (_context.Database.GetDbConnection().State == ConnectionState.Closed)
            {
                await _context.Database.GetDbConnection().OpenAsync(cancellationToken);
            }

            try
            {
                using var reader = await command.ExecuteReaderAsync(cancellationToken);
                var results = new List<TDestination>();

                while (await reader.ReadAsync(cancellationToken))
                {
                    var instance = new TDestination();

                    foreach (var prop in typeof(TDestination).GetProperties())
                    {
                        if (!reader.HasColumn(prop.Name) || reader[prop.Name] == DBNull.Value) continue;

                        prop.SetValue(instance, reader[prop.Name]);
                    }

                    results.Add(instance);
                }

                return results;
            }
            finally
            {
                await _context.Database.CloseConnectionAsync();
            }
        }

        public async Task ExecuteStoredProcedure(
            string procedureName,
            IEnumerable<StoredProcedureParameter> parameters,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(procedureName))
                throw new ArgumentException("The procedure name must be provided.", nameof(procedureName));

            using var command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandText = procedureName;
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddRange(GetSqlParameters(parameters));

            if (_context.Database.GetDbConnection().State == ConnectionState.Closed)
            {
                await _context.Database.GetDbConnection().OpenAsync(cancellationToken);
            }
            try
            {
                await command.ExecuteNonQueryAsync(cancellationToken);
            }
            finally
            {
                await _context.Database.CloseConnectionAsync();
            }
        }
    }
}
