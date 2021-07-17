using Birthday.Application.contracts;
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
        // PUT: BirthdayController/Edit/5
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(
            [FromForm] BirthdayEditRequest request, 
            CancellationToken cancellationToken)
        {
            int year;
            if (request.Year == null)
            {
                year = 1;
            }
            else
            {
                year = (int)request.Year;
            }

            await _birthdayService.Edit(new EditBirthday.Request
            {
                Id = request.Id,
                Name = request.Name,
                SecondName = request.SecondName,
                Day = request.Day,
                Month = request.Month,
                Year = year

            }, cancellationToken);

            return Ok();
        }
        public sealed class BirthdayEditRequest
        {
            [Required]
            public int Id { get; set; }
            [Required, MinLength(2), MaxLength(25)]
            public string Name { get; set; }
            [MinLength(2), MaxLength(25)]
            public string SecondName { get; set; }
            [Required, Range(1, 31)]
            public int Day { get; set; }
            [Required, Range(1, 12)]
            public int Month { get; set; }

            [Range(1900, 9999)]
            public int? Year { get; set; }
            //public IFormFile Image { get; set; }
        }
    }
}
