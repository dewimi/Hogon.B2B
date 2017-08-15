namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateAccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "CreatePerson", c => c.String());
            AddColumn("dbo.Accounts", "CreateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Accounts", "UpdatePerson", c => c.String());
            AddColumn("dbo.Accounts", "UpdateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "UpdateTime");
            DropColumn("dbo.Accounts", "UpdatePerson");
            DropColumn("dbo.Accounts", "CreateTime");
            DropColumn("dbo.Accounts", "CreatePerson");
        }
    }
}
