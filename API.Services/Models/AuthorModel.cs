using API.Services.Mapper.Interaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Models
{
    /// <summary>
    /// Author View Model
    /// </summary>
    public class AuthorModel : IDataTransferObject
    {
        /// <summary>
        /// Id of the Author Entity.
        /// </summary>
        public int? AuthorId { get; set; }

        /// <summary>
        /// Name of the author.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of the books of the author.
        /// </summary>
        public List<BookModel> Books { get; set; }
    }
}
