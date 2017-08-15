namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class news : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Goods", "ServiceGoodsId", "dbo.Goods");
            DropForeignKey("dbo.Goods", "ProductGoods_Id", "dbo.Goods");
            DropIndex("dbo.Goods", new[] { "ProductGoods_Id" });
            DropColumn("dbo.Goods", "ProductGoods_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Goods", "ProductGoods_Id", c => c.Guid());
            CreateIndex("dbo.Goods", "ProductGoods_Id");
            AddForeignKey("dbo.Goods", "ProductGoods_Id", "dbo.Goods", "Id");
            AddForeignKey("dbo.Goods", "ServiceGoodsId", "dbo.Goods", "Id");
        }
    }
}
