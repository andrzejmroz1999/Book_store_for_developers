using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace book_store_for_developers.Models
{
    public class CartItem
    {
        public Book book { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
    }
}