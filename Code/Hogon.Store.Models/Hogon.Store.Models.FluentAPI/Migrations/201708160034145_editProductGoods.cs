namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editProductGoods : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "DisplaySpecParameterTemplateName", c => c.String());
            AddColumn("dbo.Goods", "FileUpload_Id", c => c.Guid());
            AddColumn("dbo.Goods", "Product_Id", c => c.Guid());
            AddColumn("dbo.Brands", "FileUpload_Id", c => c.Guid());
            AddColumn("dbo.Products", "SpecParameterTemplate", c => c.String());
            CreateIndex("dbo.Goods", "FileUpload_Id");
            CreateIndex("dbo.Goods", "Product_Id");
            CreateIndex("dbo.Brands", "FileUpload_Id");
            AddForeignKey("dbo.Brands", "FileUpload_Id", "dbo.FileUploads", "Id");
            AddForeignKey("dbo.Goods", "FileUpload_Id", "dbo.FileUploads", "Id");
            AddForeignKey("dbo.Goods", "Product_Id", "dbo.Products", "Id");
            AddColumn("dbo.Goods", "ServicerName", c => c.String());
            DropForeignKey("dbo.Goods", "Product_Id", "dbo.Products");
            DropIndex("dbo.Goods", new[] { "Product_Id" });
            RenameColumn(table: "dbo.Goods", name: "ProductGoodss_Id", newName: "ProductGoods_Id");
            RenameIndex(table: "dbo.Goods", name: "IX_ProductGoodss_Id", newName: "IX_ProductGoods_Id");
            DropColumn("dbo.Goods", "Product_Id");
            DropForeignKey("dbo.Goods", "ServiceGoodsId", "dbo.Goods");
            DropForeignKey("dbo.Goods", "ProductGoods_Id", "dbo.Goods");
            DropIndex("dbo.Goods", new[] { "ProductGoods_Id" });
            DropColumn("dbo.Goods", "ProductGoods_Id");
            AddColumn("dbo.Goods", "ServiceGoods_ProductId", c => c.Guid());
            CreateIndex("dbo.Goods", "ServiceGoods_ProductId");
            AddForeignKey("dbo.Goods", "ServiceGoods_ProductId", "dbo.Products", "Id");

            AddColumn("dbo.Products", "DiplaySpecType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "DiplaySpecType");
            DropForeignKey("dbo.Goods", "ServiceGoods_ProductId", "dbo.Products");
            DropIndex("dbo.Goods", new[] { "ServiceGoods_ProductId" });
            DropColumn("dbo.Goods", "ServiceGoods_ProductId");
            AddColumn("dbo.Goods", "ProductGoods_Id", c => c.Guid());
            CreateIndex("dbo.Goods", "ProductGoods_Id");
            AddForeignKey("dbo.Goods", "ProductGoods_Id", "dbo.Goods", "Id");
            AddForeignKey("dbo.Goods", "ServiceGoodsId", "dbo.Goods", "Id");
            AddColumn("dbo.Goods", "Product_Id", c => c.Guid());
            RenameIndex(table: "dbo.Goods", name: "IX_ProductGoods_Id", newName: "IX_ProductGoodss_Id");
            RenameColumn(table: "dbo.Goods", name: "ProductGoods_Id", newName: "ProductGoodss_Id");
            CreateIndex("dbo.Goods", "Product_Id");
            AddForeignKey("dbo.Goods", "Product_Id", "dbo.Products", "Id");
            DropColumn("dbo.Goods", "ServicerName");
            DropForeignKey("dbo.Goods", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Goods", "FileUpload_Id", "dbo.FileUploads");
            DropForeignKey("dbo.Brands", "FileUpload_Id", "dbo.FileUploads");
            DropIndex("dbo.Brands", new[] { "FileUpload_Id" });
            DropIndex("dbo.Goods", new[] { "Product_Id" });
            DropIndex("dbo.Goods", new[] { "FileUpload_Id" });
            DropColumn("dbo.Products", "SpecParameterTemplate");
            DropColumn("dbo.Brands", "FileUpload_Id");
            DropColumn("dbo.Goods", "Product_Id");
            DropColumn("dbo.Goods", "FileUpload_Id");
            DropColumn("dbo.Goods", "DisplaySpecParameterTemplateName");

        }
    }
}
