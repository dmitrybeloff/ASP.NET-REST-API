using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DomainServices.Commands;
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

        public AuthorsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAuthorAsync([FromBody] CreateAuthorCommand createAuthorCommand)
        {
            var result = await mediator.Send(createAuthorCommand);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
