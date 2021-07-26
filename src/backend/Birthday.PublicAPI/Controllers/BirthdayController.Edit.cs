using Birthday.Application.contracts;
using Birthday.PublicAPI.Controllers.Exceptions;
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
        // PUT: BirthdayController/Edit/5
        [HttpPut]
        public async Task<ActionResult> Edit(
            [FromForm] BirthdayEditRequest request,
            CancellationToken cancellationToken)
        {

            var parse = DateTime.TryParse(request.Birthday, out DateTime bday);
            if (!parse)
            {
                throw new InvalidDateFormatException(); ;
            }

            await _birthdayService.Edit(new EditBirthday.Request
            {
                Id = request.Id,
                Name = request.Name,
                SecondName = request.SecondName,
                Birthday = bday,
                Photo = request.Image

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
            public string Birthday { get; set; }
            public IFormFile Image { get; set; }
        }
    }
}
