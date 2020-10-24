using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class AuthorModel
    {
        public int? AuthorId { get; set; }

        public string Name { get; set; }

        public List<BookModel> Books { get; set; }
    }
}
