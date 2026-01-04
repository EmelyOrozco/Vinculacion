
using Vinculacion.Domain.Constants;

namespace Vinculacion.Application.Interfaces.Repositories
{
    public interface IQueryRepository
    {
        Task<IEnumerable<TDestination>> ExecuteQuery<TDestination>(string query, Dictionary<string, object?> parameters, CancellationToken cancellationToken) where TDestination : new();
        Task ExecuteStoredProcedure(string procedureName, IEnumerable<StoredProcedureParameter> parameters, CancellationToken cancellationToken);
    }
}
