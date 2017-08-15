namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateProductGoods : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpecParameterTemplates", "ProductGoods_Id", c => c.Guid());
            AddColumn("dbo.Goods", "ProductId", c => c.Guid());
            CreateIndex("dbo.SpecParameterTemplates", "ProductGoods_Id");
            CreateIndex("dbo.Goods", "ProductId");
            AddForeignKey("dbo.Goods", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SpecParameterTemplates", "ProductGoods_Id", "dbo.Goods", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SpecParameterTemplates", "ProductGoods_Id", "dbo.Goods");
            DropForeignKey("dbo.Goods", "ProductId", "dbo.Products");
            DropIndex("dbo.Goods", new[] { "ProductId" });
            DropIndex("dbo.SpecParameterTemplates", new[] { "ProductGoods_Id" });
            DropColumn("dbo.Goods", "ProductId");
            DropColumn("dbo.SpecParameterTemplates", "ProductGoods_Id");
        }
    }
}
