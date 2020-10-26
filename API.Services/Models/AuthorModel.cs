using API.Services.Mapper.Interaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Models
{
    public class AuthorModel : IDataTransferObject
    {
        public int? AuthorId { get; set; }

        public string Name { get; set; }

        public List<BookModel> Books { get; set; }
    }
}
