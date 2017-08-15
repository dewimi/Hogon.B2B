namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addServicerName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "ServicerName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goods", "ServicerName");
        }
    }
}
