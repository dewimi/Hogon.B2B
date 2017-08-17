namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class menuupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menus", "Sort", c => c.Int(nullable: false));
            AlterColumn("dbo.Menus", "Code", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Menus", "Code", unique: true, name: "MenuCodeIndexer");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Menus", "MenuCodeIndexer");
            AlterColumn("dbo.Menus", "Code", c => c.String(maxLength: 50));
            DropColumn("dbo.Menus", "Sort");
        }
    }
}
