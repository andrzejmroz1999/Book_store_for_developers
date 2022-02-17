using book_store_for_developers.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using static book_store_for_developers.Models.IdentityModels;

namespace book_store_for_developers.DAL
{
    public class BooksContext : IdentityDbContext<ApplicationUser>
    {
        public BooksContext() : base("BooksContext")
        {

        }

        static BooksContext()
        {
            Database.SetInitializer<BooksContext>(new BooksInitializer());
        }
        public static BooksContext Create()
        {
            return new BooksContext();
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
        }


    }
}