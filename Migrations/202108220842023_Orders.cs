namespace book_store_for_developers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Orders : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Order", name: "ApplicationUser_Id", newName: "UserId");
            RenameIndex(table: "dbo.Order", name: "IX_ApplicationUser_Id", newName: "IX_UserId");
            AddColumn("dbo.Order", "Address", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "UserData_PhoneNumber", c => c.String());
            AlterColumn("dbo.Order", "PhoneNumber", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Order", "Email", c => c.String(nullable: false));
            DropColumn("dbo.Order", "Street");
            DropColumn("dbo.AspNetUsers", "UserData_Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserData_Phone", c => c.String());
            AddColumn("dbo.Order", "Street", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Order", "Email", c => c.String());
            AlterColumn("dbo.Order", "PhoneNumber", c => c.String());
            DropColumn("dbo.AspNetUsers", "UserData_PhoneNumber");
            DropColumn("dbo.Order", "Address");
            RenameIndex(table: "dbo.Order", name: "IX_UserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Order", name: "UserId", newName: "ApplicationUser_Id");
        }
    }
}
