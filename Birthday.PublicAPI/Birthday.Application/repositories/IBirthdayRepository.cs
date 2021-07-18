using Birthday.Application.repositories;
using Birthday.Domain;
using Birthday.Domain.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Application.interfaces
{
    public interface IBirthdayRepository:IRepository<Person, int>
    {

    }
}
