using Birthday.Domain.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Birthday.PublicAPI.Controllers.Exceptions
{
    public sealed class InvalidDateFormatException : DomainException
    {
        public InvalidDateFormatException() : base("Date is not correct")
        {
        }
    }
}
