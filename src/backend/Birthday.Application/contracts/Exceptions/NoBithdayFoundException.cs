
using Birthday.Domain.Shared.Exceptions;


namespace Birthday.Application.contracts.Exceptions
{
    public sealed class NoBithdayFoundException : NotFoundException
    {
        public NoBithdayFoundException(int id) : base($"Birthday with id {id} not found")
        {
        }
    }
}
