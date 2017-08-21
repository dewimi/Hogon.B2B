namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcurrentIdentity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
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
                        CurrentIdentity_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.CurrentIdentity_Id)
                .Index(t => t.CurrentIdentity_Id);
            
            AddColumn("dbo.Staffs", "Person_Id", c => c.Guid());
            CreateIndex("dbo.Staffs", "Person_Id");
            CreateIndex("dbo.Rela_Role_Person", "Account_Id");
            CreateIndex("dbo.Enterprise", "Id");
            CreateIndex("dbo.Person", "Id");
            AddForeignKey("dbo.Rela_Role_Person", "Account_Id", "dbo.Accounts", "Id");
            AddForeignKey("dbo.Staffs", "Person_Id", "dbo.Person", "Id");
            AddForeignKey("dbo.Enterprise", "Id", "dbo.Accounts", "Id");
            AddForeignKey("dbo.Person", "Id", "dbo.Accounts", "Id");
            DropColumn("dbo.Enterprise", "Name");
            DropColumn("dbo.Enterprise", "Password");
            DropColumn("dbo.Enterprise", "IsEnable");
            DropColumn("dbo.Enterprise", "PhoneNumber");
            DropColumn("dbo.Enterprise", "EmailAddress");
            DropColumn("dbo.Enterprise", "CreatePerson");
            DropColumn("dbo.Enterprise", "CreateTime");
            DropColumn("dbo.Enterprise", "UpdatePerson");
            DropColumn("dbo.Enterprise", "UpdateTime");
            DropColumn("dbo.Person", "Name");
            DropColumn("dbo.Person", "Password");
            DropColumn("dbo.Person", "IsEnable");
            DropColumn("dbo.Person", "PhoneNumber");
            DropColumn("dbo.Person", "EmailAddress");
            DropColumn("dbo.Person", "CreatePerson");
            DropColumn("dbo.Person", "CreateTime");
            DropColumn("dbo.Person", "UpdatePerson");
            DropColumn("dbo.Person", "UpdateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Person", "UpdateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Person", "UpdatePerson", c => c.String());
            AddColumn("dbo.Person", "CreateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Person", "CreatePerson", c => c.String());
            AddColumn("dbo.Person", "EmailAddress", c => c.String());
            AddColumn("dbo.Person", "PhoneNumber", c => c.String());
            AddColumn("dbo.Person", "IsEnable", c => c.Boolean(nullable: false));
            AddColumn("dbo.Person", "Password", c => c.String(maxLength: 50));
            AddColumn("dbo.Person", "Name", c => c.String(maxLength: 50));
            AddColumn("dbo.Enterprise", "UpdateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Enterprise", "UpdatePerson", c => c.String());
            AddColumn("dbo.Enterprise", "CreateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Enterprise", "CreatePerson", c => c.String());
            AddColumn("dbo.Enterprise", "EmailAddress", c => c.String());
            AddColumn("dbo.Enterprise", "PhoneNumber", c => c.String());
            AddColumn("dbo.Enterprise", "IsEnable", c => c.Boolean(nullable: false));
            AddColumn("dbo.Enterprise", "Password", c => c.String(maxLength: 50));
            AddColumn("dbo.Enterprise", "Name", c => c.String(maxLength: 50));
            DropForeignKey("dbo.Person", "Id", "dbo.Accounts");
            DropForeignKey("dbo.Enterprise", "Id", "dbo.Accounts");
            DropForeignKey("dbo.Staffs", "Person_Id", "dbo.Person");
            DropForeignKey("dbo.Rela_Role_Person", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "CurrentIdentity_Id", "dbo.Accounts");
            DropIndex("dbo.Person", new[] { "Id" });
            DropIndex("dbo.Enterprise", new[] { "Id" });
            DropIndex("dbo.Rela_Role_Person", new[] { "Account_Id" });
            DropIndex("dbo.Accounts", new[] { "CurrentIdentity_Id" });
            DropIndex("dbo.Staffs", new[] { "Person_Id" });
            DropColumn("dbo.Staffs", "Person_Id");
            DropTable("dbo.Accounts");
        }
    }
}
