using API.Services.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace API.DomainServices.Commands
{
    public class DeleteBookCommand : IRequest<bool>
    {
        public int BookId { get; private set; }

        public DeleteBookCommand(int bookId)
        {
            BookId = bookId;
        }        
    }
}
