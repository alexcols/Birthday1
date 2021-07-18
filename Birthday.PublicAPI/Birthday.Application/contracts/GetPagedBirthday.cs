using Birthday.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Application.contracts
{
    public static class GetPagedBirthday
    {
        public sealed class Request
        {
            public int Offset { get; set; } = 0;
            public int Limit { get; set; } = 10;
        }
        public sealed class Response
        {
            public string Name { get; set; }
            public string SecondName { get; set; }
            public DateTime DateWithoutYear { get; set; }
            public int Age { get; set; }
            //public Image Photo { get; set; }
        }

    }
}
