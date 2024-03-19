using Microsoft.EntityFrameworkCore;

namespace ApiDevTest.Models
{
    [Keyless]
    public class BookResults
    {

        public string? Title { get; set; }

        public string? AuthorName { get; set; }

        public int AuthorCount { get; set; }

    }

}
