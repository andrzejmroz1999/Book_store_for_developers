using Hangfire;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
[assembly: OwinStartupAttribute(typeof(book_store_for_developers.Startup))]
namespace book_store_for_developers
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            GlobalConfiguration.Configuration
               .UseSqlServerStorage("BooksContext");

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}