using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace API.DomainServices.Commands
{
    public class AddBookReviewCommand : IRequest<bool>
    {
        
        public int BookId { get; private set; }

        public string Name { get; private set; }

        public int Stars { get; private set; }

        public string ReviewText { get; private set; }

        public AddBookReviewCommand(int bookId, string name, int stars, string reviewText)
        {
            BookId = bookId;
            Name = name;
            Stars = stars;
            ReviewText = reviewText;
        }
    }
}
