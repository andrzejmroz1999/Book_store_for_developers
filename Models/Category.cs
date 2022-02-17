using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace book_store_for_developers.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Enter a name for the category")]
        [StringLength(100)]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Enter a description for the category")]
        public string CategoryDescyption{ get; set; }
        public string IconFileName { get; set; }
        
        
        public virtual ICollection<Book> Books { get; set; }
    }
}