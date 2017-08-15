namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Brand_Area : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Brands", "Nation", c => c.String());
            AddColumn("dbo.Brands", "Country", c => c.String());
            AddColumn("dbo.Brands", "City", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Brands", "City");
            DropColumn("dbo.Brands", "Country");
            DropColumn("dbo.Brands", "Nation");
        }
    }
}
