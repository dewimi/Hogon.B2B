namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateMenu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menus", "Icon", c => c.String());
            AddColumn("dbo.Menus", "CreatePerson", c => c.String());
            AddColumn("dbo.Menus", "CreateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Menus", "UpdatePerson", c => c.String());
            AddColumn("dbo.Menus", "UpdateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Menus", "CreatedTime");
            DropColumn("dbo.Menus", "UpdatedTime");
            DropColumn("dbo.Menus", "CreatorId");
            DropColumn("dbo.Menus", "UpdaterId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Menus", "UpdaterId", c => c.Int(nullable: false));
            AddColumn("dbo.Menus", "CreatorId", c => c.Int(nullable: false));
            AddColumn("dbo.Menus", "UpdatedTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Menus", "CreatedTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Menus", "UpdateTime");
            DropColumn("dbo.Menus", "UpdatePerson");
            DropColumn("dbo.Menus", "CreateTime");
            DropColumn("dbo.Menus", "CreatePerson");
            DropColumn("dbo.Menus", "Icon");
        }
    }
}
