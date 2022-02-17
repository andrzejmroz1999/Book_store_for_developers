using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace book_store_for_developers.Infrastructure
{
    public class DefaultCasheProvider : ICasheProvider
    {
        private Cache cashe { get { return HttpContext.Current.Cache; } }
        public object Get(string key)
        {
            return cashe[key];
        }

        public void Invalidate(string key)
        {
            cashe.Remove(key);
        }

        public bool IsSet(string key)
        {
            return (cashe[key] != null);
        }

        public void Set(string key, object data, int casheTime)
        {
            var expirationTime = DateTime.Now + TimeSpan.FromMinutes(casheTime);
            cashe.Insert(key, data,null,expirationTime,Cache.NoSlidingExpiration);
        }
    }
}