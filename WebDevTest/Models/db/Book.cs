using System;
using System.Collections.Generic;

namespace WebDevTest.Models.db
{
    public partial class Book
    {
        public Guid BookId { get; set; }
        public string Title { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        public DateTime? PublicationYear { get; set; }
        public double? Price { get; set; }
        public string? Isbn { get; set; }
        public Guid AuthorId { get; set; }

        public virtual Author Author { get; set; } = null!;
    }
}
