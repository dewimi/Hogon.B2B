namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Accounts", "Password", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "Password", c => c.String(maxLength: 20));
            AlterColumn("dbo.Accounts", "Name", c => c.String(maxLength: 20));
        }
    }
}
