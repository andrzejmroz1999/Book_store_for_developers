using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace book_store_for_developers.ViewModels
{
    public class CartRemovalViewModel
    {
        public decimal CartTotalPrice { get; set; }
        public int CartQuantityItem { get; set; }
        public int NumberItemsToRemove { get; set; }
        public int IdItemsToRemove { get; set; }
    }
}