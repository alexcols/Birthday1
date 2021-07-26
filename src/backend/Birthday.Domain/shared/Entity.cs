

namespace Birthday.Domain.shared
{
    public abstract class Entity<TId>
    {
        public TId Id { get; set; }
    }
}
