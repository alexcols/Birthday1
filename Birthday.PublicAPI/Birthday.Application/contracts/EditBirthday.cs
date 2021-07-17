using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            public int Day { get; set; }
            public int Month { get; set; }
            public int? Year { get; set; }
            //public IFormFile Photo { get; set; }
        }
    }
}
