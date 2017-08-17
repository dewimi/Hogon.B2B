namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roleupdate : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Roles", new[] { "Enterprise_Id" });
            RenameColumn(table: "dbo.Roles", name: "Enterprise_Id", newName: "EnterpriseId");
            AlterColumn("dbo.Roles", "EnterpriseId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Roles", "EnterpriseId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Roles", new[] { "EnterpriseId" });
            AlterColumn("dbo.Roles", "EnterpriseId", c => c.Guid());
            RenameColumn(table: "dbo.Roles", name: "EnterpriseId", newName: "Enterprise_Id");
            CreateIndex("dbo.Roles", "Enterprise_Id");
        }
    }
}
