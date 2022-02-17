using book_store_for_developers.DAL;
using book_store_for_developers.Models;
using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace book_store_for_developers.Infrastructure
{
    public class BooksDetailsDynamicNodeProvider : DynamicNodeProviderBase
    {
        private BooksContext db = new BooksContext();
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode nodee)
        {
            var returnValue = new List<DynamicNode>();
            foreach (Book book in db.Books)
            {
                DynamicNode node = new DynamicNode();
                node.Title = book.BookTitle;
                node.Key = "Book_" + book.BookId;
                node.ParentKey = "Category_" + book.CategoryId;
                node.RouteValues.Add("id", book.BookId);
                returnValue.Add(node);
            }

            return returnValue;
        }
    }
}