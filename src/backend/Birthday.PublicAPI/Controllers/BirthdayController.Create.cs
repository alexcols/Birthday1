using Birthday.Application.contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Birthday.PublicAPI.Controllers
{
    public partial class BirthdayController
    {
        // POST: BirthdayController/Create
        [HttpPost]
        
        public async Task<IActionResult> Create(
            [FromForm] BithdayCreateRequest request,
            CancellationToken cancellationToken)
        {
            

            var response = await _birthdayService.Create(new CreateBirthday.Request
            {
                Name = request.Name,
                SecondName = request.SecondName,
                Day = request.Day,
                Month = request.Month,
                Year = request.Year,
                Photo = request.Image,
                Birthday = request.Birthday

            }, cancellationToken);

            return Created($"/api/birthday/{response.Id}", new { });

        }
        public sealed class BithdayCreateRequest
        {
            [Required, MinLength(2), MaxLength(25)]
            public string Name { get; set; }
            [MinLength(2), MaxLength(25)]
            public string SecondName { get; set; }
            public DateTime Birthday { get; set; }
            [Required, Range(1, 31)]
            public int Day { get; set; }
            [Required, Range(1, 12)]
            public int Month { get; set; }
            
            [Range(1, 9999)]
            public int? Year { get; set; }
            public IFormFile Image { get; set; }
        }


    }
}
