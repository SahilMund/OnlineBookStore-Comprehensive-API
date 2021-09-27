using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineBookStore.API.Models
{
    public partial class Cart
    {
        public Cart()
        {
            Orders = new HashSet<Order>();
        }

        public int CartId { get; set; }
        public int Quantity { get; set; }
        public int SumTotal { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }

        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
