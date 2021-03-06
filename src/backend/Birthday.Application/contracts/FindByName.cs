using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Application.contracts
{
    public class FindByName
    {
        public sealed class Request : Paged.Request
        {
            public string SearchName { get; set; }
        }

        public sealed class Response : Paged.Response<Response.BirthdayResponse>
        {
            public sealed class BirthdayResponse
            {

                public int Id { get; set; }
                public string Name { get; set; }
                public string SecondName { get; set; }
                public DateTime Date { get; set; }              
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
