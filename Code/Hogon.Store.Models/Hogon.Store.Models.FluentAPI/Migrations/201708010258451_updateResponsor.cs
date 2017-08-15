namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateResponsor : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Responsors", "ConnectPeopleName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Responsors", "ConnectPeopleName", c => c.String());
        }
    }
}
