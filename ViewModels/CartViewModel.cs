using book_store_for_developers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace book_store_for_developers.ViewModels
{
    public class CartViewModel
    {
        public List<CartItem> CartItem { get; set; }
        public decimal TotalPrice { get; set; }
    }
}