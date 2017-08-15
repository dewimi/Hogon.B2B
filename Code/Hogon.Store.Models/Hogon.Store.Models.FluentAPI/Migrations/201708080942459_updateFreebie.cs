namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateFreebie : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FreebieLines",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Quota = c.Int(nullable: false),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        FreebieId = c.Guid(nullable: false),
                        ProductGoodsId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Freebies", t => t.FreebieId, cascadeDelete: true)
                .ForeignKey("dbo.Goods", t => t.ProductGoodsId)
                .Index(t => t.FreebieId)
                .Index(t => t.ProductGoodsId);
            
            AddColumn("dbo.Freebies", "ProductId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Freebies", "ProductId");
            AddForeignKey("dbo.Freebies", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Freebies", "ProductId", "dbo.Products");
            DropForeignKey("dbo.FreebieLines", "ProductGoodsId", "dbo.Goods");
            DropForeignKey("dbo.FreebieLines", "FreebieId", "dbo.Freebies");
            DropIndex("dbo.FreebieLines", new[] { "ProductGoodsId" });
            DropIndex("dbo.FreebieLines", new[] { "FreebieId" });
            DropIndex("dbo.Freebies", new[] { "ProductId" });
            DropColumn("dbo.Freebies", "ProductId");
            DropTable("dbo.FreebieLines");
        }
    }
}
