namespace LibraryLite.UI.Web.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seedsuperuser : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.User", "UserNameIndex");
            AlterColumn("dbo.User", "Email", c => c.String());
            AlterColumn("dbo.User", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "UserName", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.User", "Email", c => c.String(maxLength: 256));
            CreateIndex("dbo.User", "UserName", unique: true, name: "UserNameIndex");
        }
    }
}
