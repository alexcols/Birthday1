

using Microsoft.AspNetCore.Http;

namespace Birthday.Application.contracts
{
    public static class CreateBirthday
    {
        public sealed class Request
        {
            public string Name { get; set; }
            public string SecondName { get; set; }
            public int Day { get; set; }
            public int Month { get; set; }
            public int? Year { get; set; }
            public IFormFile Photo { get; set; }
        }

        public sealed class Response
        {
            public int Id { get; set; }
        }

    }
}
