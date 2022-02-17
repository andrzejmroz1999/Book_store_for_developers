using book_store_for_developers.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace book_store_for_developers.Controllers
{
    public class BooksController : Controller
    {
        private BooksContext db = new BooksContext();
        // GET: Books
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(string categoryName, string searchQuery = null)
        {
            var category = db.Categories.Include("Books").Where(c => c.CategoryName.ToUpper() == categoryName.ToUpper()).Single();
            
            var books = category.Books.Where(a => (searchQuery == null ||
                                             a.BookTitle.ToLower().Contains(searchQuery.ToLower()) ||
                                             a.BookAuthor.ToLower().Contains(searchQuery.ToLower())) &&
                                             !a.Hidden);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_BooksList", books);
            }

                return View(books);
        }

        public ActionResult Details(int id)
        {
            
            var book = db.Books.Find(id);
            return View(book);
        }
        [ChildActionOnly]
        [OutputCache(Duration = 60000)]
        public ActionResult CategoriesMenu()
        {
            

            var categories = db.Categories.ToList();
            return PartialView("_CategoriesMenu",categories);
        }
        public ActionResult SameCategory(int id)
        {
            var numberOfCategeroii = db.Books.Where(b => b.BookId == id).Single();
            
                     
            var books = db.Books.Where(b => b.CategoryId == numberOfCategeroii.CategoryId && b.BookId != id).Take(2);
            
            return PartialView("_SameCategory", books);
        }

        public ActionResult BooksHints(string term)
        {
            var books = this.db.Books.Where(a => !a.Hidden && a.BookTitle.ToLower().Contains(term.ToLower())).Take(5).Select(a => new { label = a.BookTitle });
            return Json(books, JsonRequestBehavior.AllowGet);
        }

    }
}