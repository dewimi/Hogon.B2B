namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateResponsor1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Responsors", "PersonName", c => c.String());
            DropColumn("dbo.Responsors", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Responsors", "Name", c => c.String());
            DropColumn("dbo.Responsors", "PersonName");
        }
    }
}
