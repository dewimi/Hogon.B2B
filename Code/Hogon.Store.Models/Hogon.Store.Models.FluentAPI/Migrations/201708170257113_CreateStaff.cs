namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateStaff : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Accounts", newName: "Enterprise");
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        EnterpriseId = c.Guid(nullable: false),
                        PersonId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Enterprise", t => t.EnterpriseId)
                .ForeignKey("dbo.Person", t => t.PersonId)
                .Index(t => t.EnterpriseId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                        Password = c.String(maxLength: 50),
                        IsEnable = c.Boolean(nullable: false),
                        PhoneNumber = c.String(),
                        EmailAddress = c.String(),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Enterprise", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Enterprise", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.Staffs", "PersonId", "dbo.Person");
            DropForeignKey("dbo.Staffs", "EnterpriseId", "dbo.Enterprise");
            DropIndex("dbo.Staffs", new[] { "PersonId" });
            DropIndex("dbo.Staffs", new[] { "EnterpriseId" });
            DropTable("dbo.Person");
            DropTable("dbo.Staffs");
            RenameTable(name: "dbo.Enterprise", newName: "Accounts");
        }
    }
}
