namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServiceGoodsNew : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceGoodsTemps", "GoodsTypeId", "dbo.GoodsTypes");
            DropIndex("dbo.ServiceGoodsTemps", new[] { "GoodsTypeId" });
            AddColumn("dbo.Goods", "GoodsDesription", c => c.String(maxLength: 300));
            AddColumn("dbo.Goods", "IsAvailable", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Goods", "ServicePrice");
            DropColumn("dbo.Goods", "ServiceDescription");
            DropTable("dbo.ServiceGoodsTemps");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ServiceGoodsTemps",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ServiceName = c.String(maxLength: 20),
                        GoodsNum = c.String(maxLength: 20),
                        ServicePrice = c.Decimal(nullable: false, precision: 10, scale: 2),
                        ServiceSupplyName = c.String(maxLength: 20),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        GoodsTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Goods", "ServiceDescription", c => c.String(maxLength: 300));
            AddColumn("dbo.Goods", "ServicePrice", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Goods", "IsAvailable");
            DropColumn("dbo.Goods", "GoodsDesription");
            CreateIndex("dbo.ServiceGoodsTemps", "GoodsTypeId");
            AddForeignKey("dbo.ServiceGoodsTemps", "GoodsTypeId", "dbo.GoodsTypes", "Id", cascadeDelete: true);
        }
    }
}
