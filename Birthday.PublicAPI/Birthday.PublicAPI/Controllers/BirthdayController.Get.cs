using Birthday.Application.contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Birthday.PublicAPI.Controllers
{
    public partial class BirthdayController
    {
        
        [HttpGet]
        public async Task<IActionResult> GetPaged(
            [FromForm] GetPagedRequest request,
            CancellationToken cancellationToken
            )
        {
            return Ok(await _birthdayService.GetPaged(new GetPagedBirthday.Request
            {
                Limit = request.Limit,
                Offset = request.Offset
            }, cancellationToken));
        }
        public sealed class GetPagedRequest
        {
           
            // number of Dates
           
            public int Limit { get; set; } = 10;

            
            // offset of Dates
          
            public int Offset { get; set; } = 0;


        }
    }
}
