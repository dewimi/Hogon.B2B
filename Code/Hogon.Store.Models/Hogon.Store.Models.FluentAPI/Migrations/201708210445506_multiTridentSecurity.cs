namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class multiTridentSecurity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rela_Role_Person", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Rela_Role_Person", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Rela_Role_Person", "User_Id", "dbo.Users");
            DropIndex("dbo.Rela_Role_Person", new[] { "Account_Id" });
            DropIndex("dbo.Rela_Role_Person", new[] { "Role_Id" });
            DropIndex("dbo.Rela_Role_Person", new[] { "User_Id" });
            DropIndex("dbo.Users", "UserNameIndexer");
            DropTable("dbo.Rela_Role_Person");
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        NickName = c.String(maxLength: 20),
                        Password = c.String(maxLength: 50),
                        Email = c.String(maxLength: 100),
                        IsEnable = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        UpdatedTime = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        UpdaterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rela_Role_Person",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        UpdatedTime = c.DateTime(nullable: false),
                        Email = c.String(),
                        CreatorId = c.Int(nullable: false),
                        UpdaterId = c.Int(nullable: false),
                        Account_Id = c.Guid(),
                        Role_Id = c.Guid(),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Users", "Name", unique: true, name: "UserNameIndexer");
            CreateIndex("dbo.Rela_Role_Person", "User_Id");
            CreateIndex("dbo.Rela_Role_Person", "Role_Id");
            CreateIndex("dbo.Rela_Role_Person", "Account_Id");
            AddForeignKey("dbo.Rela_Role_Person", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Rela_Role_Person", "Role_Id", "dbo.Roles", "Id");
            AddForeignKey("dbo.Rela_Role_Person", "Account_Id", "dbo.Accounts", "Id");
        }
    }
}
