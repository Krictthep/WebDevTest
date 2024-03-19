using ApiDevTest.Models;
using ApiDevTest.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace ApiDevTest.Controllers
{
    /// <summary>
    /// [Route("api/[controller]")]
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        //[Route("api/book/getbooks")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookResults>>> GetBooks(string id)
        {

            object authorid;
            if (id != "all" && id != null)
            {
                authorid = id;


            }
            else
            {

                authorid = DBNull.Value;
            }

            var param = new SqlParameter("@AuthorID", authorid);


            var products = await _context.Books.FromSqlRaw("BookReport_GetReport_By_AuthorID @AuthorID", param).ToListAsync();
                
            



            return Ok(products);

        }


        [HttpPost]
        public async Task<ActionResult<IEnumerable<BookResults>>> PostBooks(string id)
        {

            object authorid;
            if (id != "all" && id != null)
            {
                authorid = id;


            }
            else
            {

                authorid = DBNull.Value;
            }

            var param = new SqlParameter("@AuthorID", authorid);


            var products = await _context.Books.FromSqlRaw("BookReport_GetReport_By_AuthorID @AuthorID", param).ToListAsync();





            return Ok(products);

        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorResults>>> GetAuthors()
        {
       
            var products = await _context.Authors.FromSqlRaw("BookReport_AuthorNames").ToListAsync();
            return Ok(products);


        }

    }
}
