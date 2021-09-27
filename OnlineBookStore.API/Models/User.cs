using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineBookStore.API.Models
{
    public partial class User
    {
        public User()
        {
            Books = new HashSet<Book>();
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
