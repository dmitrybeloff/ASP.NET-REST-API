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

        [HttpPost]
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

        [HttpPost("{bookId}")]
        public async Task<IActionResult> AddBookReviewAsync(int bookId, [FromBody] AddBookReviewCommand addBookReviewCommand)
        {
            if (bookId != addBookReviewCommand.BookId)
            {
                return BadRequest("Ids do not match.");
            }

            var result = await mediator.Send(addBookReviewCommand);

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
