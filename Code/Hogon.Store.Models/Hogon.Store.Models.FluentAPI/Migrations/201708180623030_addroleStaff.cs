namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addroleStaff : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Person", "RoleId", "dbo.Roles");
            DropIndex("dbo.Person", new[] { "RoleId" });
            AddColumn("dbo.Staffs", "RoleId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Staffs", "RoleId");
            AddForeignKey("dbo.Staffs", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
            DropColumn("dbo.Person", "RoleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Person", "RoleId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.Staffs", "RoleId", "dbo.Roles");
            DropIndex("dbo.Staffs", new[] { "RoleId" });
            DropColumn("dbo.Staffs", "RoleId");
            CreateIndex("dbo.Person", "RoleId");
            AddForeignKey("dbo.Person", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
        }
    }
}
