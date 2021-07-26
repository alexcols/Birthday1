using System;
using Microsoft.AspNetCore.Http;

namespace Birthday.Application.contracts
{
    public static class EditBirthday
    {
        public sealed class Request
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string SecondName { get; set; }
            public DateTime Birthday { get; set; }
           
            public IFormFile Photo { get; set; }
        }
    }
}
