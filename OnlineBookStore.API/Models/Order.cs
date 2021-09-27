using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineBookStore.API.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int? CartId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual User User { get; set; }
    }
}
