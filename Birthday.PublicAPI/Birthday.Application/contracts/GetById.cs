﻿using Birthday.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Application.contracts
{
    public static class GetById
    {
        public sealed class Request
        {
            public int Id { get; set; }
        }
        public sealed class Response
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string SecondName { get; set; }
            //public DateTime DateWithoutYear { get; set; }
            public int Day { get; set; }
            public int Month { get; set; }
            public int? Age { get; set; }

            public Guid PhotoGuid { get; set; }
            public string PhotoName { get; set; }
            public string PhotoType { get; set; }
            public byte[] PhotoContent { get; set; }
            //public Image Photo { get; set; }
        }

    }
}
