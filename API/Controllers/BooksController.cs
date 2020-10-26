using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domains.Aggregates.BookAggregate;
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
    public class BooksController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IGenericReadService readService;

        public BooksController(IMediator mediator, IGenericReadService readService)
        {
            this.mediator = mediator;
            this.readService = readService;
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

        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetBookAsync(int bookId)
        {
            var book = await readService.ReadAsync<Book, BookModel>(x => x.BookId == bookId, new string[] { });

            if (book != null)
            {
                return Ok(book);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
