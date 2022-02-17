using book_store_for_developers.DAL;
using book_store_for_developers.Models;
using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace book_store_for_developers.Infrastructure
{
    public class CategoriesDetailsDynamicNodeProvider : DynamicNodeProviderBase
    {
        private BooksContext db = new BooksContext();
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode nodee)
        {
            var returnValue = new List<DynamicNode>();
            foreach (Category category in db.Categories)
            {
                DynamicNode node = new DynamicNode();
                node.Title = category.CategoryName;
                node.Key = "Category_" + category.CategoryId;            
                node.RouteValues.Add("categoryName" ,category.CategoryName);
                returnValue.Add(node);
            }

            return returnValue;
        }
    }
}