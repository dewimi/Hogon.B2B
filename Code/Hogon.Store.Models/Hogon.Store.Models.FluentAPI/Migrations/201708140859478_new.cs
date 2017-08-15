namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Goods", "Product_Id", "dbo.Products");
            DropIndex("dbo.Goods", new[] { "Product_Id" });
            RenameColumn(table: "dbo.Goods", name: "ProductGoodss_Id", newName: "ProductGoods_Id");
            RenameIndex(table: "dbo.Goods", name: "IX_ProductGoodss_Id", newName: "IX_ProductGoods_Id");
            DropColumn("dbo.Goods", "Product_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Goods", "Product_Id", c => c.Guid());
            RenameIndex(table: "dbo.Goods", name: "IX_ProductGoods_Id", newName: "IX_ProductGoodss_Id");
            RenameColumn(table: "dbo.Goods", name: "ProductGoods_Id", newName: "ProductGoodss_Id");
            CreateIndex("dbo.Goods", "Product_Id");
            AddForeignKey("dbo.Goods", "Product_Id", "dbo.Products", "Id");
        }
    }
}
