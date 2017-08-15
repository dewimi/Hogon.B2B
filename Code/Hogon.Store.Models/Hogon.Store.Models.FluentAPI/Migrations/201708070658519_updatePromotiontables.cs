namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatePromotiontables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FreebieCatalogs", "FreebieCatalogName", c => c.String());
            AddColumn("dbo.FreebieCatalogs", "Sort", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FreebieCatalogs", "Sort");
            DropColumn("dbo.FreebieCatalogs", "FreebieCatalogName");
        }
    }
}
