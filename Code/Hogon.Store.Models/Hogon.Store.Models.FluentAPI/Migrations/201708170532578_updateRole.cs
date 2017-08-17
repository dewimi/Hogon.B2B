namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "Enterprise_Id", c => c.Guid());
            CreateIndex("dbo.Roles", "Enterprise_Id");
            AddForeignKey("dbo.Roles", "Enterprise_Id", "dbo.Enterprise", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Roles", "Enterprise_Id", "dbo.Enterprise");
            DropIndex("dbo.Roles", new[] { "Enterprise_Id" });
            DropColumn("dbo.Roles", "Enterprise_Id");
        }
    }
}
