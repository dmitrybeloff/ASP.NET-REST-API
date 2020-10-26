using API.Services.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace API.DomainServices.Commands
{
    public class UpdateBookReviewCommand : IRequest<bool>
    {
        public int BookId { get; private set; }

        public int BookReviewId { get; private set; }

        public string Name { get; private set; }

        public int Stars { get; private set; }

        public string ReviewText { get; private set; }

        public UpdateBookReviewCommand(int bookId, int bookReviewId, string name, int stars, string reviewText)
        {
            BookId = bookId;
            BookReviewId = bookReviewId;
            Name = name;
            Stars = stars;
            ReviewText = reviewText;
        }
    }
}
