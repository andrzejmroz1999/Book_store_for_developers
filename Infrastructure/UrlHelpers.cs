using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace book_store_for_developers.Infrastructure
{
    public static class UrlHelpers
    {
        public static string PathIconCategories(this UrlHelper helper, string categoryIconName)
        {
            var FolderCategoryIcons = AppConfig.__RelativeFolderCategoryIcons;
            var patch = Path.Combine(FolderCategoryIcons, categoryIconName);
            var absolutePath = helper.Content(patch);

            return absolutePath;
        }

        public static string PathBooks(this UrlHelper helper, string bookName)
        {
            var BooksFolder = AppConfig.__RelativeBooksFolder;
            var patch = Path.Combine(BooksFolder, bookName);
            var absolutePath = helper.Content(patch);

            return absolutePath;
        }
    }
}