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
    [Route("User")]
    [ApiController]

    public class UserController : ControllerBase
    {
        OnlineBookStoreDBContext _dbContext = new OnlineBookStoreDBContext();
        [HttpGet]
   
        public ActionResult<IEnumerable<User>> Get()
        {
            return _dbContext.Users.ToList();
        }

        [HttpPost]
        public void AddUser(User user)
        {

          
            if (user != null)
            {

                var isduplicate = _dbContext.Users.Count(s => s.Email == user.Email);
                if (isduplicate < 1)
                {
                    _dbContext.Users.Add(user);
                    _dbContext.SaveChanges();
                }
                //else
                //{
                   
                //}
              
            }

        }


        [HttpPatch]
        [Route("{email}")]
        public ActionResult UpdatePassword(string email, JsonPatchDocument userPassword)
        {

            var user = _dbContext.Users.First(s => s.Email == email);

            if (user == null)
            {
                return NotFound(new { Message = "User Not Found!! Invalid EmailId" });
            }

            userPassword.ApplyTo(user);
            _dbContext.SaveChanges();

            return Ok(new { Message = "Password Updated Succesfully" });
        }


        //        [
        //    {
        //        "op":"replace",
        //        "path":"Password",
        //        "value":"ak11"
        //    }

        //]

    }
}
