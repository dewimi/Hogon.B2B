namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateFreebie1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Freebies", "IsPublish", c => c.Boolean(nullable: false));
            AddColumn("dbo.Freebies", "FreebiSortNum", c => c.Int(nullable: false));
            AddColumn("dbo.Freebies", "LimitBuyAmount", c => c.Int(nullable: false));
            AddColumn("dbo.Freebies", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Freebies", "Description");
            DropColumn("dbo.Freebies", "LimitBuyAmount");
            DropColumn("dbo.Freebies", "FreebiSortNum");
            DropColumn("dbo.Freebies", "IsPublish");
        }
    }
}
