using Birthday.Domain.shared;
using System;

namespace Birthday.Domain
{
    public sealed class Person:Entity<int>
    {
        public string Name { get; set; }
        public string SecondName { get; set; }

        //date will be nullable, if year is empty
        public DateTime? Date { get; set; } = null;

        //date for sort, default year is 0004(includes february, 29)
        public DateTime DateWithoutYear { get; set; }
        //public Image Photo { get; set; }

    }
}
