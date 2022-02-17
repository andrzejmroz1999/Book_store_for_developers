using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace book_store_for_developers.Infrastructure
{
    interface ICasheProvider
    {
        object Get(string key);
        void Set(string key, object data, int casheTime);
        bool IsSet(string key);
        void Invalidate(string key);
    }
}
