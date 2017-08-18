namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GoodsTypeAddIcon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GoodsTypes", "Icon", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GoodsTypes", "Icon");
        }
    }
}
