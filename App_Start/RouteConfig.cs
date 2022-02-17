using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace book_store_for_developers
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "BooksDetails",
                url: "book-{id}.html",
                defaults: new { controller = "Books", action = "Details" });

            routes.MapRoute(
                name: "BooksList",
                url: "Category/{categoryName}",
                defaults: new { controller = "Books", action = "List" });

            routes.MapRoute(
                  name: "StaticPages",
                  url: "pages/{name}.html",
                  defaults: new { controller = "Home", action = "StaticPages" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
                     
        }
    }
}
