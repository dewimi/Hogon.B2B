namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateResponser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Responsors", "PhoneNumber", c => c.String());
            AddColumn("dbo.Responsors", "EmailAddress", c => c.String());
            AddColumn("dbo.Responsors", "ConnectPeopleName", c => c.String());
            AddColumn("dbo.Responsors", "Department", c => c.String());
            AddColumn("dbo.Responsors", "EnterpriseName", c => c.String());
            AddColumn("dbo.Responsors", "EnterpriseAddress", c => c.String());
            AddColumn("dbo.Responsors", "EnterpriseType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Responsors", "EnterpriseType");
            DropColumn("dbo.Responsors", "EnterpriseAddress");
            DropColumn("dbo.Responsors", "EnterpriseName");
            DropColumn("dbo.Responsors", "Department");
            DropColumn("dbo.Responsors", "ConnectPeopleName");
            DropColumn("dbo.Responsors", "EmailAddress");
            DropColumn("dbo.Responsors", "PhoneNumber");
        }
    }
}
