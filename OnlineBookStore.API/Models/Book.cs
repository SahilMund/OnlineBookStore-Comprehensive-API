using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineBookStore.API.Models
{
    public partial class Book
    {
        public Book()
        {
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
        }

        public int BookId { get; set; }
        public string BookName { get; set; }
        public string AuthortName { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? Review { get; set; }
        public string BookImg { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
