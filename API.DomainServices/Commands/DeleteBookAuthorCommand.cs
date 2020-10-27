using API.Services.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace API.DomainServices.Commands
{
    public class DeleteBookAuthorCommand : IRequest<bool>
    {
        public int BookId { get; private set; }

        public int AuthorId { get; private set; }

        public DeleteBookAuthorCommand(int bookId, int authorId)
        {
            BookId = bookId;
            AuthorId = authorId;
        }
    }
}
