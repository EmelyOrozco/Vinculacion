
using System.Linq.Expressions;
using Vinculacion.Domain.Base;

namespace Vinculacion.Application.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<OperationResult<List<TEntity>>> GetAllAsync(Expression<Func<TEntity, bool>> filter);
        Task<OperationResult<TEntity>> GetByIdAsync(int id);
        Task<OperationResult<TEntity>> AddAsync(TEntity entity);
        OperationResult<TEntity> Update(TEntity entity);
    }
}
