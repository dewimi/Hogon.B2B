namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateGoods : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "GoodsClass_Id", "dbo.GoodsTypes");
            DropIndex("dbo.Products", new[] { "GoodsClass_Id" });
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BrandName = c.String(),
                        BrandLogo = c.String(),
                        BrandAlias = c.String(),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rela_Brand_GoodsType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BrandId = c.Guid(nullable: false),
                        GoodsTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.GoodsTypes", t => t.GoodsTypeId, cascadeDelete: true)
                .Index(t => t.BrandId)
                .Index(t => t.GoodsTypeId);
            
            CreateTable(
                "dbo.Rela_Goods_GoodsType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GoodsId = c.Guid(nullable: false),
                        GoodsTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Goods", t => t.GoodsId, cascadeDelete: true)
                .ForeignKey("dbo.GoodsTypes", t => t.GoodsTypeId, cascadeDelete: true)
                .Index(t => t.GoodsId)
                .Index(t => t.GoodsTypeId);
            
            AddColumn("dbo.Products", "BrandId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Products", "BrandId");
            AddForeignKey("dbo.Products", "BrandId", "dbo.Brands", "Id", cascadeDelete: true);
            DropColumn("dbo.Products", "GoodsClass_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "GoodsClass_Id", c => c.Guid());
            DropForeignKey("dbo.Products", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Rela_Brand_GoodsType", "GoodsTypeId", "dbo.GoodsTypes");
            DropForeignKey("dbo.Rela_Goods_GoodsType", "GoodsTypeId", "dbo.GoodsTypes");
            DropForeignKey("dbo.Rela_Goods_GoodsType", "GoodsId", "dbo.Goods");
            DropForeignKey("dbo.Rela_Brand_GoodsType", "BrandId", "dbo.Brands");
            DropIndex("dbo.Rela_Goods_GoodsType", new[] { "GoodsTypeId" });
            DropIndex("dbo.Rela_Goods_GoodsType", new[] { "GoodsId" });
            DropIndex("dbo.Rela_Brand_GoodsType", new[] { "GoodsTypeId" });
            DropIndex("dbo.Rela_Brand_GoodsType", new[] { "BrandId" });
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropColumn("dbo.Products", "BrandId");
            DropTable("dbo.Rela_Goods_GoodsType");
            DropTable("dbo.Rela_Brand_GoodsType");
            DropTable("dbo.Brands");
            CreateIndex("dbo.Products", "GoodsClass_Id");
            AddForeignKey("dbo.Products", "GoodsClass_Id", "dbo.GoodsTypes", "Id");
        }
    }
}
