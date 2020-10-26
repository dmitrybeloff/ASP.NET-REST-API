using API.Services.Mapper.Interaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Models
{
    public class BookModel: IDataTransferObject
    {
        public int? BookId { get; set; }

        public string Title { get; set; }

        public List<BookReviewModel> BookReviews { get; set; }

        public List<AuthorModel> Authors { get; set; }
    }
}
