using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookStore.API.Controllers
{
    [Route("Book")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        OnlineBookStoreDBContext _dbContext = new OnlineBookStoreDBContext();
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAllBook()
        {

            var book = _dbContext.Books.ToList();
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        //Get book by category
        //later adding
        [Route("bookCategories")]
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetCategories()
        {

            var LatestUploads = _dbContext.Books.OrderByDescending(s => s.BookId).Take(3);

           
            var BestSellers = (from b in _dbContext.Books
                            join o in _dbContext.Orders
                            on b.BookId equals o.BookId
                           
                            select new
                            {
                                BookId = b.BookId,
                                BookName = b.BookName,
                                BookImg=b.BookImg,
                                Descrption=b.Description,
                                AuthorName = b.AuthortName
                            
                            }).Take(1);

            var TrendingBooks = _dbContext.Books.Where(s => s.Review >= 3).Take(3);
         
            if (LatestUploads == null && TrendingBooks ==null && BestSellers == null)
            {
                return NotFound();
            }
            return Ok(new { LatestUploads = LatestUploads , TrendingBooks = TrendingBooks , BestSellers = BestSellers });
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            var bookbyid = _dbContext.Books.Find(id);
            if (bookbyid == null)
            {
                return NotFound();
            }
            return Ok(bookbyid);
        }

        [HttpPost]
        public void AddEmp(Book book)
        {
            if (book != null)
            {
                _dbContext.Books.Add(book);
                _dbContext.SaveChanges();
            }

        }


        [HttpPatch]
        [Route("{bookid}")]
        public ActionResult UpdateReview(int bookid, JsonPatchDocument reviewData)
        {

            var user = _dbContext.Books.Find(bookid);

            if (user == null)
            {
                return NotFound(new { Message = "Not Found" });
            }

            reviewData.ApplyTo(user);
            _dbContext.SaveChanges();

            return Ok(new { Message = "Review Added Succesfully" });
        }








    }
}
