using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace book_store_for_developers.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Enter the title of the book")] 
        [StringLength(100)]
        public string BookTitle { get; set; }
        [Required(ErrorMessage = "Enter the author of the book")]
        [StringLength(100)]
        public string BookAuthor { get; set; }
        public string PublishingHouse { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime ReleaseDate { get; set; }
        [StringLength(100)]
        public string ImageFileName { get; set; }
        public string BookDescryption { get; set; }
        public int NumberOfPages { get; set; }
        public decimal BookPrice { get; set; }
        public bool Bestseller { get; set; }
        public bool Hidden { get; set; }
        public bool Ebook { get; set; }
        


        public virtual Category category { get; set; }


    }
}