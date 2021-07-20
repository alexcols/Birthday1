using Advertisement.Domain.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Application.contracts.Exceptions
{
    public sealed class NoBithdayFoundException : NotFoundException
    {
        public NoBithdayFoundException(int id) : base($"Birthday with id {id} not found")
        {
        }
    }
}
