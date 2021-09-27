using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookStore.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookStore.API.Controllers
{
    [Route("Cart")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {

        OnlineBookStoreDBContext _dbContext = new OnlineBookStoreDBContext();
        [HttpGet]
        public ActionResult<IEnumerable<Cart>> GetAllCart()
        {

            var cart = _dbContext.Carts.ToList();
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }


        [HttpGet]
        [Route("{id}")]
        public ActionResult<Cart> GetCartByUserid(int id)
        {
            
            //var cartData = _dbContext.Carts.Where(s => s.UserId == id);
            var cartData = (from p in _dbContext.Carts
                            join e in _dbContext.Books
                            on p.BookId equals e.BookId
                            where p.UserId == id
                            select new
                            {
                               BookId = e.BookId,
                               BookName = e.BookName,
                               Price = e.Price,
                               Quantity = p.Quantity,
                               SumTotal= p.SumTotal
                            }).ToList();


            if (cartData == null )
            {
                return NotFound();
            }
            return Ok(cartData);
        }

        [HttpPost]
        public void AddCart(Cart cart)
        {
            if (cart != null)
            {
                _dbContext.Carts.Add(cart);
                _dbContext.SaveChanges();
            }

        }



        [HttpDelete]
        [Route("{bookId}")]
        public void DeleteCartData(int bookId)
        {
            var cartDetails = _dbContext.Carts.First(s => s.BookId == bookId);
            if (cartDetails == null)
            {
                throw new ArgumentNullException();
            }
            _dbContext.Entry(cartDetails).State = EntityState.Deleted;
            _dbContext.SaveChanges();
        }


    }
}
