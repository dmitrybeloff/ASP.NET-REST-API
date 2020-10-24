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
    public class BooksController : ControllerBase
    {
        private readonly IMediator mediator;

        public BooksController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBookAsync([FromBody]CreateBookCommand createBookCommand)
        {
            var result = await mediator.Send(createBookCommand);

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
