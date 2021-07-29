using Birthday.Application.repositories;
using Birthday.Domain;
using Birthday.Domain.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Birthday.Application.interfaces
{
    public interface IBirthdayRepository:IRepository<Person, int>
    {
        Task<List<Person>> GetPagedBirthdays(Expression<Func<Person, bool>> predicate, int limit, CancellationToken cancellationToken);
        Task<List<Person>> GetBirthdaysOrderedByDate(int limit, int offset, CancellationToken cancellationToken);
        Task<IEnumerable<Person>> GetPagedSortByName(int offset, int limit, CancellationToken cancellationToken);
        Task<IEnumerable<Person>> FindByName(string searchName, int offset, int limit, CancellationToken cancellationToken);
        Task<int> Count(Expression<Func<Person, bool>> predicate, CancellationToken cancellationToken);

    }
}
