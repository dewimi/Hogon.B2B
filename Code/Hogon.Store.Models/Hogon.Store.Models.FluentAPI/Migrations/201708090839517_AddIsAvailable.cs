namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsAvailable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "IsAvailable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goods", "IsAvailable");
        }
    }
}
