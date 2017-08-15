namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProductSpecType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "DiplaySpecType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "DiplaySpecType");
        }
    }
}
