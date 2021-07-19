using Birthday.Application.interfaces;
using Birthday.Application.repositories;
using Birthday.Domain;
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
    public class BirthdayRepository : Repository<Person, int>, IBirthdayRepository
    {
       
        private readonly DatabaseContext _dbContext;

        public BirthdayRepository(DatabaseContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Person>> GetPagedBirthdays(Expression<Func<Person, bool>> predicate, int limit, CancellationToken cancellationToken)
        {
            
            
            return await _dbContext
               .Set<Person>()
               .Where(predicate)
               .OrderBy(e => e.DateWithoutYear)
               .Take(limit)              
               .ToListAsync(cancellationToken);
        }

     

        public async Task<IEnumerable<Person>> GetPagedByName(int offset, int limit, CancellationToken cancellationToken)
        {
            return await _dbContext
                .Set<Person>()
                .OrderBy(e => e.Name)
                .Skip(offset)
                .Take(limit)

                .ToListAsync(cancellationToken);
        }

       
    }
}
