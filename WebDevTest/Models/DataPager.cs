using System;
using WebDevTest.Models.db;
using static WebDevTest.Models.DataPager;


namespace WebDevTest.Models
{
    public class DataPager
    {
        #region

        //public DataPager()
        //{

        //}


        //public DataPager(int totalrows, int pages, int pagesize = 10)
        //{
        //    int currentpaage = pages;
        //    int totalpages = (int)Math.Ceiling((decimal)totalrows / pagesize);

        //    int startpage = currentpaage - 5;
        //    int endpage = currentpaage + 4;

        //    if (startpage <= 0)
        //    {
        //        endpage = endpage - (startpage - 1);
        //        startpage = 1;
        //    }


        //    if (endpage > totalpages)
        //    {
        //        endpage = totalpages;
        //    }

        //    StartPage = startpage;
        //    EndPage = endpage;
        //    TotalPage = totalpages;

        //    CurrentPage = currentpaage;
        //    PageSize = pagesize;
        //    TotalRows = totalrows;


        //}

        //public int StartPage { get; set; }
        //public int EndPage { get; set; }
        //public int TotalPage { get; set; }

        //public int CurrentPage { get; set; }
        //public int PageSize { get; set; }
        //public int TotalRows { get; set; }



        #endregion


  

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
              
                CountRow = source.Count;       

                var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                return new PaginatedList<T>(items, count, pageIndex, pageSize);

            }







            public int TotalRows = CountRow;

        }

    }






}
