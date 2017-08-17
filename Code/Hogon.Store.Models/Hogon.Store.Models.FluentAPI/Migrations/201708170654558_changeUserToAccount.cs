namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeUserToAccount : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Rela_Role_User", newName: "Rela_Role_Account");
            DropForeignKey("dbo.Rela_Role_User", "UserId", "dbo.Users");
            DropIndex("dbo.Rela_Role_Account", new[] { "UserId" });
            AddColumn("dbo.Rela_Role_Account", "User_Id", c => c.Guid());
            CreateIndex("dbo.Rela_Role_Account", "User_Id");
            AddForeignKey("dbo.Rela_Role_Account", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rela_Role_Account", "User_Id", "dbo.Users");
            DropIndex("dbo.Rela_Role_Account", new[] { "User_Id" });
            DropColumn("dbo.Rela_Role_Account", "User_Id");
            CreateIndex("dbo.Rela_Role_Account", "UserId");
            AddForeignKey("dbo.Rela_Role_User", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.Rela_Role_Account", newName: "Rela_Role_User");
        }
    }
}
