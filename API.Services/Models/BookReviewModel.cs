using API.Services.Mapper.Interaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Models
{
    /// <summary>
    /// Book Review View Model
    /// </summary>
    public class BookReviewModel: IDataTransferObject
    {
        /// <summary>
        /// Id of the BookReview Entity.
        /// </summary>
        public int? BookReviewId { get; private set; }

        /// <summary>
        /// Name of the author of the review.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Review rating.
        /// </summary>
        public int Stars { get; private set; }

        /// <summary>
        /// Text of the review.
        /// </summary>
        public string ReviewText { get; private set; }
    }
}
