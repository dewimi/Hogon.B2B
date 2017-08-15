namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServiceGoodsProductId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "ServiceGoods_ProductId", c => c.Guid());
            CreateIndex("dbo.Goods", "ServiceGoods_ProductId");
            AddForeignKey("dbo.Goods", "ServiceGoods_ProductId", "dbo.Products", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Goods", "ServiceGoods_ProductId", "dbo.Products");
            DropIndex("dbo.Goods", new[] { "ServiceGoods_ProductId" });
            DropColumn("dbo.Goods", "ServiceGoods_ProductId");
        }
    }
}
