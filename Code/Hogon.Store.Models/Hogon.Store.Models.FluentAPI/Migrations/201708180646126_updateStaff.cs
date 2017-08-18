namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateStaff : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Staffs", "RoleId", "dbo.Roles");
            DropIndex("dbo.Staffs", new[] { "RoleId" });
            AlterColumn("dbo.Staffs", "RoleId", c => c.Guid());
            CreateIndex("dbo.Staffs", "RoleId");
            AddForeignKey("dbo.Staffs", "RoleId", "dbo.Roles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staffs", "RoleId", "dbo.Roles");
            DropIndex("dbo.Staffs", new[] { "RoleId" });
            AlterColumn("dbo.Staffs", "RoleId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Staffs", "RoleId");
            AddForeignKey("dbo.Staffs", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
        }
    }
}
