using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Movies.Core;
using Movies.Core.Domain.Common;
using Movies.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;

namespace Movies.Data.Repositories
{
    public partial class EntityRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly MoviesDbContext _databaseContext;
        protected readonly DbSet<TEntity> _table;

        public virtual IQueryable<TEntity> Table => _table;

        public EntityRepository(
            MoviesDbContext databaseContext)
        {
            _databaseContext = databaseContext;
            _table = _databaseContext.Set<TEntity>();
        }

        protected virtual async Task<IList<TEntity>> GetEntities(Func<Task<IList<TEntity>>> getAllAsync)
        {
            return await getAllAsync();
        }

        protected virtual IQueryable<TEntity> AddDeletedFilter(IQueryable<TEntity> query, in bool includeDeleted)
        {
            if (includeDeleted)
                return query;

            if (typeof(TEntity).GetInterface(nameof(ISoftDeletedEntity)) == null)
                return query;

            return query.OfType<ISoftDeletedEntity>().Where(entry => !entry.Deleted).OfType<TEntity>();
        }

        public virtual async Task<TEntity> GetByIdAsync(int? id, bool includeDeleted = false)
        {
            if (!id.HasValue || id == 0)
                return null;

            async Task<TEntity> getEntityAsync()
            {
                return await AddDeletedFilter(Table, includeDeleted).FirstOrDefaultAsync(entity => entity.Id == Convert.ToInt32(id));
            }

            return await getEntityAsync();
        }

        public virtual async Task<IList<TEntity>> GetByIdsAsync(IList<int> ids, bool includeDeleted = false)
        {
            if (!ids?.Any() ?? true)
                return new List<TEntity>();

            async Task<IList<TEntity>> getByIdsAsync()
            {
                var query = AddDeletedFilter(Table, includeDeleted);

                //get entries
                var entries = await query.Where(entry => ids.Contains(entry.Id)).ToListAsync();

                //sort by passed identifiers
                var sortedEntries = new List<TEntity>();
                foreach (var id in ids)
                {
                    var sortedEntry = entries.FirstOrDefault(entry => entry.Id == id);
                    if (sortedEntry != null)
                        sortedEntries.Add(sortedEntry);
                }

                return sortedEntries;
            }

            return await getByIdsAsync();
        }

        public virtual async Task<TEntity> GetAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null, bool includeDeleted = false)
        {
            async Task<TEntity> getEntityAsync()
            {
                var query = AddDeletedFilter(Table, includeDeleted);
                query = func != null ? func(query) : query;
                return await query.FirstOrDefaultAsync();
            }

            return await getEntityAsync();
        }


        public virtual async Task<IList<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null,
            bool includeDeleted = false)
        {
            async Task<IList<TEntity>> getAllAsync()
            {
                var query = AddDeletedFilter(Table, includeDeleted);
                query = func != null ? func(query) : query;
                return await query.ToListAsync();
            }

            return await GetEntities(getAllAsync);
        }

        public virtual async Task<IList<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> func = null,
            bool includeDeleted = false)
        {
            async Task<IList<TEntity>> getAllAsync()
            {
                var query = AddDeletedFilter(Table, includeDeleted);
                query = func != null ? await func(query) : query;
                return await query.ToListAsync();
            }

            return await GetEntities(getAllAsync);
        }

        public virtual async Task<int> GetCountAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null, bool includeDeleted = false)
        {
            var query = AddDeletedFilter(Table, includeDeleted);
            query = func != null ? func(query) : query;
            return await query.CountAsync();
        }

        public virtual async Task InsertAsync(TEntity entity, bool publishEvent = true)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                await _table.AddAsync(entity);
                await _databaseContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be added: {ex.Message}");
            }
        }

        public virtual async Task InsertAsync(IList<TEntity> entities, bool publishEvent = true)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await _table.AddRangeAsync(entities);
            await _databaseContext.SaveChangesAsync();
            transaction.Complete();
        }

        public virtual async Task<TEntity> LoadOriginalCopyAsync(TEntity entity)
        {
            return await _table.FirstOrDefaultAsync(e => e.Id == Convert.ToInt32((entity.Id)));
        }

        public virtual void Update(TEntity entity, bool publishEvent = true)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                _table.Update(entity);
                _databaseContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {e.Message}");
            }
        }

        public virtual void Update(IList<TEntity> entities, bool publishEvent = true)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            if (!entities.Any())
                return;

            foreach (var entity in entities)
                Update(entity, publishEvent);
        }

        public virtual void Delete(TEntity entity, bool publishEvent = true)
        {
            switch (entity)
            {
                case null:
                    throw new ArgumentNullException(nameof(entity));

                case ISoftDeletedEntity softDeletedEntity:
                    softDeletedEntity.Deleted = true;
                    Update(entity);
                    break;
            }
        }

        public virtual void Delete(IList<TEntity> entities, bool publishEvent = true)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            if (entities.OfType<ISoftDeletedEntity>().Any())
            {
                foreach (var entity in entities)
                    if (entity is ISoftDeletedEntity softDeletedEntity)
                    {
                        softDeletedEntity.Deleted = true;
                        Update(entity);
                    }
            }
            else
            {
                _table.RemoveRange(entities);
                _databaseContext.SaveChanges();
            }
        }

        public virtual async Task<int> Delete(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var entities = await Table.Where(predicate).ToListAsync();
            Delete(entities);

            return entities.Count;
        }

        public virtual IDbContextTransaction BeginTransaction()
        {
            return _databaseContext.Database.BeginTransaction();
        }
    }
}
