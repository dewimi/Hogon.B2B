namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "Weight", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Brands", "Url", c => c.String());
            AddColumn("dbo.Instructions", "FileUpload_Id", c => c.Guid());
            AddColumn("dbo.AppCases", "FileUpload_Id", c => c.Guid());
            CreateIndex("dbo.Instructions", "FileUpload_Id");
            CreateIndex("dbo.AppCases", "FileUpload_Id");
            AddForeignKey("dbo.Instructions", "FileUpload_Id", "dbo.FileUploads", "Id");
            AddForeignKey("dbo.AppCases", "FileUpload_Id", "dbo.FileUploads", "Id");
            DropColumn("dbo.Goods", "IsAvailable");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Goods", "IsAvailable", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.AppCases", "FileUpload_Id", "dbo.FileUploads");
            DropForeignKey("dbo.Instructions", "FileUpload_Id", "dbo.FileUploads");
            DropIndex("dbo.AppCases", new[] { "FileUpload_Id" });
            DropIndex("dbo.Instructions", new[] { "FileUpload_Id" });
            DropColumn("dbo.AppCases", "FileUpload_Id");
            DropColumn("dbo.Instructions", "FileUpload_Id");
            DropColumn("dbo.Brands", "Url");
            DropColumn("dbo.Goods", "Weight");
        }
    }
}
