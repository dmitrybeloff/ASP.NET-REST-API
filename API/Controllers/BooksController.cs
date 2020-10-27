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

        [HttpGet]
        public async Task<IActionResult> GetBooksAsync([FromQuery] int start = 0, int limit = 10)
        {
            var books = await readService.ReadManyAsync<Book, BookModel>(start, limit, new string[] { });

            if (books != null)
            {
                return Ok(books);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookAsync([FromBody]CreateBookCommand createBookCommand)
        {
            var result = await mediator.Send(createBookCommand);

            if (result != null && result.BookId.HasValue)
            {
                return Created($"{Request.Path}{result.BookId}", result);
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
                return NotFound();
            }
        }

        [HttpGet("{bookId}/detailed")]
        public async Task<IActionResult> GetBookDetailedAsync(int bookId)
        {
            var book = await readService.ReadAsync<Book, BookModel>(x => x.BookId == bookId, new string[] { "Authors.Author" });

            if (book != null)
            {
                return Ok(book);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBookAsync(int bookId, [FromBody] UpdateBookCommand updateBookCommand)
        {
            if (bookId != updateBookCommand.BookId)
            {
                return BadRequest("Ids do not match.");
            }

            var result = await mediator.Send(updateBookCommand);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBookAsync(int bookId)
        {
            var result = await mediator.Send(new DeleteBookCommand(bookId));

            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{bookId}/reviews/")]
        public async Task<IActionResult> GetBookReviewsAsync(int bookId, [FromQuery] int start = 0, int limit = 10)
        {
            var reviews = await readService.ReadManyAsync<BookReview, BookReviewModel>(start, limit, new string[] { }, x => x.Book.BookId == bookId);

            if (reviews != null)
            {
                return Ok(reviews);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("{bookId}/reviews/")]
        public async Task<IActionResult> AddBookReviewAsync(int bookId, [FromBody] AddBookReviewCommand addBookReviewCommand)
        {
            if (bookId != addBookReviewCommand.BookId)
            {
                return BadRequest("Ids do not match.");
            }

            var result = await mediator.Send(addBookReviewCommand);

            if (result != null && result.BookReviewId.HasValue)
            {
                return Created($"{Request.Path}{result.BookReviewId}", result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{bookId}/reviews/{bookReviewId}")]
        public async Task<IActionResult> GetBookReviewAsync(int bookId, int reviewId)
        {
            var review = await readService.ReadAsync<BookReview, BookReviewModel>(x => x.Book.BookId == bookId && x.BookReviewId == reviewId, new string[] { });

            if (review != null)
            {
                return Ok(review);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{bookId}/reviews/{bookReviewId}")]
        public async Task<IActionResult> UpdateBookReviewAsync(int bookId, int bookReviewId, [FromBody] UpdateBookReviewCommand updateBookReviewCommand)
        {
            if (bookId != updateBookReviewCommand.BookId || bookReviewId != updateBookReviewCommand.BookReviewId)
            {
                return BadRequest("Ids do not match.");
            }

            var result = await mediator.Send(updateBookReviewCommand);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{bookId}/reviews/{bookReviewId}")]
        public async Task<IActionResult> DeleteBookReviewAsync(int bookId, int bookReviewId)
        {
            var result = await mediator.Send(new DeleteBookReviewCommand(bookId, bookReviewId));

            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{bookId}/authors/{bookAuthorId}")]
        public async Task<IActionResult> AddBookAuthorAsync(int bookId, int bookAuthorId)
        {
            var result = await mediator.Send(new AddBookAuthorCommand(bookId, bookAuthorId));

            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{bookId}/authors/{bookAuthorId}")]
        public async Task<IActionResult> DeleteBookAuthorAsync(int bookId, int bookAuthorId)
        {
            var result = await mediator.Send(new DeleteBookAuthorCommand(bookId, bookAuthorId));

            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
