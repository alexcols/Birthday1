using System;


namespace Birthday.Application.contracts
{
    public static class GetPagedBirthday
    {
        public sealed class Request : Paged.Request
        {          
        }
        public sealed class Response: Paged.Response<Response.BirthdayResponse>
        {
            public sealed class BirthdayResponse
            {

                public int Id { get; set; }
                public string Name { get; set; }
                public string SecondName { get; set; }
                public DateTime Date { get; set; }
                //public int Day { get; set; }
                //public int Month { get; set; }
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
