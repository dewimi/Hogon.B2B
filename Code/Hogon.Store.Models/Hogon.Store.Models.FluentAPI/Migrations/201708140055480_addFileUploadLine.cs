namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFileUploadLine : DbMigration
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
        }
        
        public override void Down()
        {
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
