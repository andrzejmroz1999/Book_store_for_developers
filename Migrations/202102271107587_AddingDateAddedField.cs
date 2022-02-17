namespace book_store_for_developers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDateAddedField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "DateAdded", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Book", "DateAdded");
        }
    }
}
