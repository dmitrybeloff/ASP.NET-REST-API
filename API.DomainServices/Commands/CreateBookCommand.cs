using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace API.DomainServices.Commands
{
    public class CreateBookCommand : IRequest<bool>
    {
        public string Title { get; private set; }

        public List<int> AuthorIds { get; private set; }

        public CreateBookCommand(string title, List<int> authorIds)
        {
            Title = title;
            AuthorIds = authorIds;
        }        
    }
}
