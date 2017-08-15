namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateMarketing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FreebieCatalogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Freebies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        FreebieCatalogId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FreebieCatalogs", t => t.FreebieCatalogId, cascadeDelete: true)
                .Index(t => t.FreebieCatalogId);
            
            CreateTable(
                "dbo.Rel_Promotion_Goods",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GoodsId = c.Guid(nullable: false),
                        PromotionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Goods", t => t.GoodsId, cascadeDelete: true)
                .ForeignKey("dbo.PromotinRules", t => t.PromotionId, cascadeDelete: true)
                .Index(t => t.GoodsId)
                .Index(t => t.PromotionId);
            
            CreateTable(
                "dbo.PromotinRules",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Goods", "ProductGoodsId", c => c.Guid());
            CreateIndex("dbo.Goods", "ProductGoodsId");
            AddForeignKey("dbo.Goods", "ProductGoodsId", "dbo.Goods", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Goods", "ProductGoodsId", "dbo.Goods");
            DropForeignKey("dbo.Rel_Promotion_Goods", "PromotionId", "dbo.PromotinRules");
            DropForeignKey("dbo.Rel_Promotion_Goods", "GoodsId", "dbo.Goods");
            DropForeignKey("dbo.Freebies", "FreebieCatalogId", "dbo.FreebieCatalogs");
            DropIndex("dbo.Rel_Promotion_Goods", new[] { "PromotionId" });
            DropIndex("dbo.Rel_Promotion_Goods", new[] { "GoodsId" });
            DropIndex("dbo.Goods", new[] { "ProductGoodsId" });
            DropIndex("dbo.Freebies", new[] { "FreebieCatalogId" });
            DropColumn("dbo.Goods", "ProductGoodsId");
            DropTable("dbo.PromotinRules");
            DropTable("dbo.Rel_Promotion_Goods");
            DropTable("dbo.Freebies");
            DropTable("dbo.FreebieCatalogs");
        }
    }
}
