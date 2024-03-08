namespace WebDevTest.Models
{
    public class PaginatedList<T> : List<T>
    {


        public int PageIndex { get; set; }
        public int TotalPages { get; set; }



        public static int CountRow = 0;

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);


        }

        public bool HasPreviosPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;



        public static PaginatedList<T> Create(List<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count;

            if (CountRow <= 0)
            {
                CountRow = source.Count;
            }



            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedList<T>(items, count, pageIndex, pageSize);

        }







        public int TotalRows = CountRow;

    }
}