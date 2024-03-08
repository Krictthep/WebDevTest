using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDevTest.Models.db;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System;
using WebDevTest.Models;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using WebDevTest.Migrations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Transactions;
using System.Drawing.Printing;
using Humanizer.Localisation.TimeToClockNotation;

namespace WebDevTest.Controllers
{
    public class BookController : Controller
    {
        private readonly DevTestContext _db;


        public BookController(DevTestContext db)
        {
            _db = db;
        }



        public  IActionResult Index()
        {  
            BookReport rpdt = new BookReport();
            try
            {
                int? pageNumber = null;


                rpdt.GetAuthors = _db.Authors.FromSqlRaw("BookReport_AuthorNames").ToList();

                var param = new SqlParameter("@AuthorID", DBNull.Value);

                int pagesize = 5;


                DataPager.PaginatedList<BookResults> B = DataPager.PaginatedList<BookResults>.Create(_db.BookResults.FromSqlRaw("BookReport_GetReport_By_AuthorID @AuthorID", param).ToList(), pageNumber ?? 1, pagesize);

                rpdt.bookResults = B;

                ModelState.Clear();
            }
            catch(Exception ex)
            {
                throw ex;
            }         
        

            return View(rpdt);


        }


        public int GetRemainingNode(int id)
        {
            int res = 0;

            int[] beforeNode = findbeforeNode(id);
            if(beforeNode.Length > 0 && beforeNode.Length == 1){
                res = beforeNode[0];
            }
            else
            {
                for (int i = 0; i < beforeNode.Length; i++)
                {
                    CheckProcessToUpdate(id, beforeNode[i]);

                    res = res + 1;
                }

            }

            return res;
        }
        public void CheckProcessToUpdate(int firstNode, int beforeNode)
        {
           
                    bool isActive = CheckIsActiveNode(beforeNode);
                    if(isActive){
                        updateProgress(firstNode);
                    }                
                
            
        }

        public int[] findbeforeNode(int id)
        {
            int[] nodes = checkrelationNode(id);
            List<int> nodeList = new List<int>();
            int count = 0;
            if(nodes.Length > 0){
                for(int i = 0; i < nodes.Length; i++){
                    bool isActive = CheckIsActiveNode(nodes[i]);
                    if(isActive){
                        count = count + 1;
                        updateProgress(id);
                    }
                    else{

                        int[] nodes2 = checkrelationNode(nodes[i]);
                        for(int b = 0; b < nodes2.Length; b++)
                        {
                            nodeList.Add(nodes2[b]);
                        }
                        
                    }
                }
            }

            int[] resNodes;
            if (nodeList.Count > 0)
            {
                 resNodes = nodeList.ToArray();
            }
            else
            {
                resNodes = new int[1];
                resNodes[0] = count;
            }
           
            return resNodes;
        }


        public int[] checkrelationNode(int id)
        {
            var param = new SqlParameter("@id", id);
            List<Relation> listRelation = _db.Relations.FromSqlRaw("Node_CheckRelation @id", param).ToList();
            List<int> listNodes = new List<int>();
            if(listRelation.Count > 0)
            {
                for(int i = 0; i < listRelation.Count; i++)
                {
                    listNodes.Add(listRelation[i].BeforeId);
                }
            }

            int[] nodes = listNodes.ToArray();

            return nodes;            
        }


        public bool CheckIsActiveNode(int id)
        {
            bool res = false;
            var param = new SqlParameter("@id", id);
            List<Node> listNode = _db.Nodes.FromSqlRaw("Node_CheckIsActive @id", param).ToList();
            List<int> listNodes = new List<int>();
            if (listNode.Count > 0)
            {
                res = listNode[0].IsEnabled;


            }          

            return res;
        }


        public void updateProgress(int id)
        {
            var param = new SqlParameter("@id", id);
            var d = _db.Database.ExecuteSqlRaw("Node_UpdateProgresslog @id", param);
            
        }


        [TempData]
        public string SelectID { get; set; }


        public IActionResult Paging(int? pageNumber, string? ID)
        {
           

            BookReport rpdt = new BookReport();
            try
            {

                if (ModelState.IsValid)
                {
                    rpdt.GetAuthors = _db.Authors.FromSqlRaw("BookReport_AuthorNames").ToList();

                    string id = ID;
                    object authorid;
                    if (id != "all" && id != null)
                    {
                        authorid = id;
                        ViewData["ID"] = id;
                    }
                    else
                    {
                        ViewData["ID"] = "all";
                        authorid = DBNull.Value;
                    }

                    var param = new SqlParameter("@AuthorID", authorid);

                    int pagesize = 5;

                    DataPager.PaginatedList<BookResults> B = DataPager.PaginatedList<BookResults>.Create(_db.BookResults.FromSqlRaw("BookReport_GetReport_By_AuthorID @AuthorID", param).ToList(), pageNumber ?? 1, pagesize);

                    rpdt.bookResults = B;

                   

                }
                   
            }
            catch(Exception ex)
            {
                throw ex;
            }



            ModelState.Clear();

            return View("Index", rpdt);


        }




        [HttpPost]       
        public IActionResult Select(string q = " ")
        {
            ModelState.Clear();

            BookReport rpdt = new BookReport();
            try
            {

                if (ModelState.IsValid)
                {
                    rpdt.GetAuthors = _db.Authors.FromSqlRaw("BookReport_AuthorNames").ToList();

                    object authorid;
                    if (q != "all" && q != null)
                    {
                        authorid = q;                       
                        //SelectID = q;
                        ViewData["ID"] = q;
                        
                    }
                    else
                    {                       
                        //SelectID = "all";
                        ViewData["ID"] = "all";
                    
                        authorid = DBNull.Value;
                    }

                    var param = new SqlParameter("@AuthorID", authorid);


                    int? pageNumber = null;
                    int pagesize = 5;

                    DataPager.PaginatedList<BookResults> B = DataPager.PaginatedList<BookResults>.Create(_db.BookResults.FromSqlRaw("BookReport_GetReport_By_AuthorID @AuthorID", param).ToList(), pageNumber ?? 1, pagesize);

                    rpdt.bookResults = B;


                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }      

            return View("Index", rpdt);


        }




        public List<BookResults> GetDataBooksDefualt()
        {
            List<BookResults> bookResultskList = new List<BookResults>();
     
           
            var param = new SqlParameter("@AuthorID", DBNull.Value);          

            bookResultskList = _db.BookResults.FromSqlRaw("BookReport_GetReport_By_AuthorID @AuthorID", param).ToList();


            return bookResultskList;
        }



        public async Task<IActionResult> AuthorSearch(string id)
        {
            if(id == null)
            {
                return NotFound();
            }


            var book = await _db.Books.ToListAsync();

            return View(book);

        }



    }
}
