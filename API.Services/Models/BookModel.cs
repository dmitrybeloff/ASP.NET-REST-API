using API.Services.Mapper.Interaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Models
{
    /// <summary>
    /// Book View Model
    /// </summary>
    public class BookModel: IDataTransferObject
    {
        /// <summary>
        /// Id of the Book Entity.
        /// </summary>
        public int? BookId { get; set; }

        /// <summary>
        /// Title of the book.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// List of reviews of the book.
        /// </summary>
        public List<BookReviewModel> BookReviews { get; set; }

        /// <summary>
        /// List of authors of the book.
        /// </summary>
        public List<AuthorModel> Authors { get; set; }
    }
}
