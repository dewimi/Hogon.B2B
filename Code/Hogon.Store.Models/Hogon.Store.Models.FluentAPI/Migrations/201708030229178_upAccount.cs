namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upAccount : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Accounts", "ResponsorId", "dbo.Responsors");
            DropIndex("dbo.Accounts", new[] { "ResponsorId" });
            AddColumn("dbo.Accounts", "IsEnable", c => c.Boolean(nullable: false));
            AddColumn("dbo.Accounts", "PhoneNumber", c => c.String());
            AddColumn("dbo.Accounts", "EmailAddress", c => c.String());
            AddColumn("dbo.Accounts", "ConnectPeopleName", c => c.String());
            AddColumn("dbo.Accounts", "Department", c => c.String());
            AddColumn("dbo.Accounts", "EnterpriseName", c => c.String());
            AddColumn("dbo.Accounts", "EnterpriseAddress", c => c.String());
            AddColumn("dbo.Accounts", "EnterpriseType", c => c.String());
            AddColumn("dbo.Accounts", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Accounts", "ResponsorId");
            DropTable("dbo.Responsors");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Responsors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PersonName = c.String(),
                        IsEnable = c.Boolean(nullable: false),
                        PhoneNumber = c.String(),
                        EmailAddress = c.String(),
                        Department = c.String(),
                        EnterpriseName = c.String(),
                        EnterpriseAddress = c.String(),
                        EnterpriseType = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Accounts", "ResponsorId", c => c.Guid(nullable: false));
            DropColumn("dbo.Accounts", "Discriminator");
            DropColumn("dbo.Accounts", "EnterpriseType");
            DropColumn("dbo.Accounts", "EnterpriseAddress");
            DropColumn("dbo.Accounts", "EnterpriseName");
            DropColumn("dbo.Accounts", "Department");
            DropColumn("dbo.Accounts", "ConnectPeopleName");
            DropColumn("dbo.Accounts", "EmailAddress");
            DropColumn("dbo.Accounts", "PhoneNumber");
            DropColumn("dbo.Accounts", "IsEnable");
            CreateIndex("dbo.Accounts", "ResponsorId");
            AddForeignKey("dbo.Accounts", "ResponsorId", "dbo.Responsors", "Id");
        }
    }
}
