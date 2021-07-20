using Birthday.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Birthday.PublicAPI.Controllers
{

    [Route("api/birthday")]
    public partial class BirthdayController : ControllerBase
    {
        private readonly IBirthdayService _birthdayService;

        public BirthdayController(IBirthdayService birthdayService)
        {
            _birthdayService = birthdayService;
        }

        
    }
}
