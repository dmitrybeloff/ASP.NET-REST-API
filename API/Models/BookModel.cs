using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class BookModel
    {
        public int? BookId { get; set; }

        public string Title { get; set; }

        public List<BookReview> BookReviews { get; set; }

        public List<AuthorModel> Authors { get; set; }
    }
}
