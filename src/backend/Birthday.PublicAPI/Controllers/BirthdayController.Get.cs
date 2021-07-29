using Birthday.Application.contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Birthday.PublicAPI.Controllers
{
    public partial class BirthdayController
    {
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetById.Response), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            return Ok(await _birthdayService.GetById(new GetById.Request
            {
                Id = id
            }, cancellationToken));

        }


        [HttpGet]
        public async Task<IActionResult> GetPaged(
            [FromQuery] GetPagedRequest request,
            CancellationToken cancellationToken
            )
        {
            return Ok(await _birthdayService.GetPaged(new GetPagedBirthday.Request
            {
                Limit = request.Limit,
                Offset = request.Offset
            }, cancellationToken));
        }

        [HttpGet("next/")]
        //[ProducesResponseType(typeof(GetPagedBirthday.Response), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetNext(
            [FromQuery] int limit,
            CancellationToken cancellationToken)
        {
            return Ok(await _birthdayService.GetNext(new GetNext.Request
            {
                Limit = limit
            }, cancellationToken));
        }

        [HttpGet("find/")]
        public async Task<IActionResult> FindByName(
            FindByNameRequest request,
            CancellationToken cancellationToken)
        {
            return Ok(await _birthdayService.FindByName(new FindByName.Request
            {
                SearchName = request.SearchName,
                Limit = request.Limit,
                Offset = request.Offset
            }, 
            cancellationToken));
        }

        public sealed class GetPagedRequest
        {

            // number of Dates

            public int Limit { get; set; } = 10;


            // offset of Dates

            public int Offset { get; set; } = 0;

        }

        public sealed class FindByNameRequest
        {
            [Required, MinLength(3)]
            public string SearchName { get; set; }
            public int Limit { get; set; } = 100;
            public int Offset { get; set; } = 0;
        }


    }
}
