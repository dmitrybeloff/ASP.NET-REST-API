using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace API.DomainServices.Commands
{
    [DataContract]
    public class CreateBookCommand : IRequest<bool>
    {
        [DataMember]
        public string Title { get; private set; }
        [DataMember]
        public List<int> AuthorIds { get; private set; }

        public CreateBookCommand(string title, List<int> authorIds)
        {
            Title = title;
            AuthorIds = authorIds;
        }        
    }
}
