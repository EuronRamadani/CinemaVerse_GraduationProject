using Microsoft.EntityFrameworkCore.Storage;
using Movies.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Movies.Data.Interfaces
{
    public partial interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(int? id, bool includeDeleted = false);
        Task<IList<TEntity>> GetByIdsAsync(IList<int> ids, bool includeDeleted = false);
        Task<TEntity> GetAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null, bool includeDeleted = false);
        Task<IList<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null, bool includeDeleted = false);
        Task<IList<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> func = null, bool includeDeleted = false);
        Task<int> GetCountAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null, bool includeDeleted = false);
        Task InsertAsync(TEntity entity, bool publishEvent = true);
        Task InsertAsync(IList<TEntity> entities, bool publishEvent = true);
        void Update(TEntity entity, bool publishEvent = true);
        void Update(IList<TEntity> entities, bool publishEvent = true);
        void Delete(TEntity entity, bool publishEvent = true);
        void Delete(IList<TEntity> entities, bool publishEvent = true);
        Task<int> Delete(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> LoadOriginalCopyAsync(TEntity entity);

        IDbContextTransaction BeginTransaction();

        IQueryable<TEntity> Table { get; }
    }
}
