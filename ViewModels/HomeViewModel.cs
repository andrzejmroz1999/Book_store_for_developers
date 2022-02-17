using book_store_for_developers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace book_store_for_developers.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Category> Cagegories { get; set; }
        public IEnumerable<Book> Latests { get; set; }
        public IEnumerable<Book> Bestsellers { get; set; }
        
        

    }
}