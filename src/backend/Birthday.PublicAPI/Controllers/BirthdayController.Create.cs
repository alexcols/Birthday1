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

            var parse = DateTime.TryParse(request.Birthday, out DateTime bday);
            if (!parse)
            {
                throw new Exception("Date is not correct");
            }
            
            
            var response = await _birthdayService.Create(new CreateBirthday.Request
            {
                Name = request.Name,
                SecondName = request.SecondName,
                
                Photo = request.Image,
                Birthday = bday

            }, cancellationToken);

            return Created($"/api/birthday/{response.Id}", new { });

        }
        public sealed class BithdayCreateRequest
        {
            [Required, MinLength(2), MaxLength(25)]
            public string Name { get; set; }
            [MinLength(2), MaxLength(25)]
            public string SecondName { get; set; }
            public string Birthday { get; set; }                      
            public IFormFile Image { get; set; }
        }


    }
}
