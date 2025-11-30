using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Vinculacion.Application.Interfaces.Repositories;
using Vinculacion.Domain.Base;
using Vinculacion.Persistence.Context;

namespace Vinculacion.Persistence.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        public readonly VinculacionContext _context;
        public readonly DbSet<TEntity> _dbSet;
        public BaseRepository(VinculacionContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<OperationResult<List<TEntity>>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                var data = await _dbSet.Where(filter).ToListAsync();
                return OperationResult<List<TEntity>>.Success($"{typeof(TEntity)} obtenida correctamente", data);
            }
            catch (Exception ex)
            {
                return OperationResult<List<TEntity>>.Failure($"Error obteniendo {typeof(TEntity)}: {ex.Message}");
            }
        }

        public virtual async Task<OperationResult<TEntity>> GetByIdAsync(int id)
        {
            try
            {
                var entity =  await _dbSet.FindAsync(id);
                if(entity is null)
                {
                   return OperationResult<TEntity>.Failure($"{typeof(TEntity)} con Id {id} no encontrada");
                }

                return OperationResult<TEntity>.Success($"{typeof(TEntity)} obtenida correctamente", entity);
            }
            catch (Exception ex)
            {

                return OperationResult<TEntity>.Failure($"Error obteniendo {typeof(TEntity)}: {ex.Message}");
            }

        }   

        public virtual async Task<OperationResult<TEntity>> AddAsync(TEntity entity)
        {
            try
            {
                var result = await _dbSet.AddAsync(entity);
                //await _context.SaveChangesAsync();
                return OperationResult<TEntity>.Success($"{typeof(TEntity)} agregada correctamente", result);
            }
            catch (Exception ex)
            {
                return OperationResult<TEntity>.Failure($"Error agregando {typeof(TEntity)}: {ex.Message}");
            }
        }

        public virtual OperationResult<TEntity> Update(TEntity entity)
        {

            _context.Entry(entity).State = EntityState.Modified;

            return OperationResult<TEntity>.Success($"{typeof(TEntity)} actualizada correctamente", entity);

        }

        //public virtual async Task<int> DeleteAsync(int id)
        //{

        //}

    }
}
