using Birthday.Domain.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Birthday.Application.repositories
{
    public interface IRepository<TEntity, TId>
       where TEntity:Entity<TId>
    {
        Task Save(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> FindById(TId id, CancellationToken cancellationToken);
        Task<TEntity> FindWhere(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<int> Count(CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> GetPaged(int offset, int limit, CancellationToken cancellationToken);
        Task Remove(TEntity entity, CancellationToken cancellationToken);
    }
}
