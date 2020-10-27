using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domains.Aggregates.AuthorAggregate;
using API.DomainServices.Commands;
using API.Services.Models;
using API.Services.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IGenericReadService readService;

        public AuthorsController(IMediator mediator, IGenericReadService readService)
        {
            this.mediator = mediator;
            this.readService = readService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthorsAsync([FromQuery] int start = 0, int limit = 10)
        {
            var authors = await readService.ReadManyAsync<Author, AuthorModel>(start, limit, new string[] { });

            if (authors != null)
            {
                return Ok(authors);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthorAsync([FromBody] CreateAuthorCommand createAuthorCommand)
        {
            var result = await mediator.Send(createAuthorCommand);

            if (result != null && result.AuthorId.HasValue)
            {
                return Created($@"{Request.Path}{result.AuthorId}", result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
