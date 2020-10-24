using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class BookReview
    {
        public int? BookReviewId { get; private set; }

        public string Name { get; private set; }

        public int Stars { get; private set; }

        public string ReviewText { get; private set; }
    }
}
