namespace book_store_for_developers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEbookFieldToBookClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "Ebook", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Book", "Ebook");
        }
    }
}
