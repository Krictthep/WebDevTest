using System;
using System.Collections.Generic;

namespace WebDevTest.Models.db
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public Guid AuthorId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? PenName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
