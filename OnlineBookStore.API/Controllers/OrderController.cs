using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookStore.API.Controllers
{
    [Route("Order")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {

        OnlineBookStoreDBContext _dbContext = new OnlineBookStoreDBContext();

        [HttpPost]
        public void AddOrder(Order ord)
        {
            if (ord != null)
            {
                _dbContext.Orders.Add(ord);
                _dbContext.SaveChanges();
            }

        }
    }
}
