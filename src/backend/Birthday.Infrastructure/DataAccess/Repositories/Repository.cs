using Birthday.Application.repositories;
using Birthday.Domain.shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Birthday.Infrastructure.DataAccess.Repositories
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : Entity<TId>
    {
        private readonly DatabaseContext _dbContext;

        public Repository(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public async Task<int> Count(CancellationToken cancellationToken)
        {
            return await _dbContext
                .Set<TEntity>()
                .CountAsync(cancellationToken);
        }

        public async Task<TEntity> FindById(TId id, CancellationToken cancellationToken)
        {
            return await _dbContext
                .FindAsync<TEntity>(new object[] { id }, cancellationToken: cancellationToken);
        }

        public async Task<TEntity> FindWhere(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken)
        {
            return await _dbContext
                .Set<TEntity>()
                .Where(predicate)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            return await _dbContext
                .Set<TEntity>()
                .OrderBy(e => e.Id)
                .Skip(offset)
                .Take(limit)

                .ToListAsync(cancellationToken);
        }

        public async Task Remove(TEntity entity,
             CancellationToken cancellationToken)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Save(TEntity entity, CancellationToken cancellationToken)
        {
            var entry = _dbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                await _dbContext.AddAsync(entity, cancellationToken);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
