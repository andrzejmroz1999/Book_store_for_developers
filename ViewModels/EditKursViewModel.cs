using book_store_for_developers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace book_store_for_developers.ViewModels
{
    public class EditKursViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public bool? Confirmation { get; set; }
    }
}