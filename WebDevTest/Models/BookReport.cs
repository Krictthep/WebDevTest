using WebDevTest.Models.db;
using System.Collections.Immutable;
using WebDevTest.Models;

namespace WebDevTest.Models
{
    public class BookReport
    {
        public IEnumerable<AuthorResults>? GetAuthors { get; set; }
        public IEnumerable<BookResults>? GetBooks { get; set; }

        public DataPager.PaginatedList<BookResults>? bookResults { get; set; }


    }


    


}
