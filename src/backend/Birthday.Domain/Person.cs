using Birthday.Domain.shared;
using System;

namespace Birthday.Domain
{
    public sealed class Person:Entity<int>
    {
        public string Name { get; set; }
        public string SecondName { get; set; }

        
        public DateTime Date { get; set; }

        //date for sort, default year is 0004(includes february, 29)
        public DateTime DateWithoutYear { get; set; }

        public Guid PhotoGuid { get; set; }
        public string PhotoName { get; set; }
        public string PhotoType { get; set; }
                
        public byte[] PhotoContent { get; set; }

    }
}
