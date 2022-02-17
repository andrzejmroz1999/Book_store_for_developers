using book_store_for_developers.DAL;
using book_store_for_developers.Infrastructure;
using book_store_for_developers.Models;
using book_store_for_developers.ViewModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace book_store_for_developers.Controllers
{
    public class HomeController : Controller
    {
        private BooksContext db = new BooksContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public ActionResult Index()
        {
            logger.Info("Jesteś na stronie głównej");
            ICasheProvider cashe = new DefaultCasheProvider();

            List<Category> categories;
            if (cashe.IsSet(Consts.CategoriesCasheKey))
            {
                categories = cashe.Get(Consts.CategoriesCasheKey) as List<Category>;
            }
            else
            {
                categories = db.Categories.ToList();               
                cashe.Set(Consts.CategoriesCasheKey, categories, 60);
            }

            List<Book> latests;
            if (cashe.IsSet(Consts.LatestsCaheKey))
            {
                latests = cashe.Get(Consts.LatestsCaheKey) as List<Book>;
            }
            else
            {
                latests = db.Books.Where(a => !a.Hidden).OrderByDescending(a => a.DateAdded).Take(3).ToList();
                cashe.Set(Consts.LatestsCaheKey, latests, 60);
            }

            List<Book> bestsellers;
            if (cashe.IsSet(Consts.BestsellersCasheKey))
            {
                bestsellers = cashe.Get(Consts.BestsellersCasheKey) as List<Book>;
            }
            else
            {
                bestsellers = db.Books.Where(a => !a.Hidden && a.Bestseller).OrderBy(a => Guid.NewGuid()).Take(3).ToList();
                cashe.Set(Consts.BestsellersCasheKey, bestsellers, 60);
            }


             

            var vm = new HomeViewModel()
            {
                Cagegories = categories,
                Latests = latests,
                Bestsellers = bestsellers
            };
           

            return View(vm);
        }
        public ActionResult StaticPages(string name)
        {
            return View(name);
        }
    } 
}