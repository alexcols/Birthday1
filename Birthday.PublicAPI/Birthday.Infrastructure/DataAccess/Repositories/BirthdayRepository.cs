using Birthday.Application.interfaces;
using Birthday.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Birthday.Infrastructure.DataAccess.Repositories
{
    public class BirthdayRepository : IBirthdayRepository
    {
        public Task<int> Count(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Person> FindById(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Person> FindWhere(Expression<Func<Person, bool>> predicate, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Person>> GetPaged(int offset, int limit, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Remove(Person entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task Save(Person entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
