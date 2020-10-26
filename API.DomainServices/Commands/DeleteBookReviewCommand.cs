using API.Services.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace API.DomainServices.Commands
{
    public class DeleteBookReviewCommand : IRequest<bool>
    {
        public int BookId { get; private set; }

        public int BookReviewId { get; private set; }

        public DeleteBookReviewCommand(int bookId, int bookReviewId)
        {
            BookId = bookId;
            BookReviewId = bookReviewId;
        }        
    }
}
