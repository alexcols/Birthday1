using Birthday.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Application.contracts
{
    public static class GetNext
    {
        public sealed class Request
        {
            public int Limit { get; set; } = 5;
        }
        public sealed class Response : Paged.Response<Response.BirthdayNextResponse>

        {
            public sealed class BirthdayNextResponse
            {

                public int Id { get; set; }
                public string Name { get; set; }
                public string SecondName { get; set; }
                //public DateTime DateWithoutYear { get; set; }
                public int Day { get; set; }
                public int Month { get; set; }
                public int? Age { get; set; }

                public string PhotoGuid { get; set; }
                public string PhotoName { get; set; }
                public string PhotoType { get; set; }
                public byte[] PhotoContent { get; set; }
                //public Image Photo { get; set; }
            }
        }

    }
}
