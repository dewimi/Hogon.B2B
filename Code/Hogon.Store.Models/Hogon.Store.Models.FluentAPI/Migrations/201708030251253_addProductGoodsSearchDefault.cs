namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProductGoodsSearchDefault : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Goods", name: "ServiceGoodsId", newName: "ProductGoodss_Id");
            RenameIndex(table: "dbo.Goods", name: "IX_ServiceGoodsId", newName: "IX_ProductGoodss_Id");
            AddColumn("dbo.Goods", "SearchKeywords", c => c.String());
            AddColumn("dbo.Goods", "IsDefaultGoods", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goods", "IsDefaultGoods");
            DropColumn("dbo.Goods", "SearchKeywords");
            RenameIndex(table: "dbo.Goods", name: "IX_ProductGoodss_Id", newName: "IX_ServiceGoodsId");
            RenameColumn(table: "dbo.Goods", name: "ProductGoodss_Id", newName: "ServiceGoodsId");
        }
    }
}
