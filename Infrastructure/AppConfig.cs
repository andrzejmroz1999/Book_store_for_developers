using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace book_store_for_developers.Infrastructure
{
    public class AppConfig
    {
        private static string __relativeFolderCategoryIcons = ConfigurationManager.AppSettings["FolderCategoryIcons"];
        public static string __RelativeFolderCategoryIcons
        {
            get
            {
                return __relativeFolderCategoryIcons;
            }
        }

        private static string __relativeBooksFolder = ConfigurationManager.AppSettings["BooksFolder"];
        public static string __RelativeBooksFolder
        {
            get
            {
                return __relativeBooksFolder;
            }
        }

    }
}