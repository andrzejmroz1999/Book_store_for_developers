namespace book_store_for_developers.Migrations
{
    using book_store_for_developers.DAL;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

   public sealed class Configuration : DbMigrationsConfiguration<book_store_for_developers.DAL.BooksContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(book_store_for_developers.DAL.BooksContext context)
        {
            BooksInitializer.SeedBooksData(context);
            BooksInitializer.SeedUsers(context);
            
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
