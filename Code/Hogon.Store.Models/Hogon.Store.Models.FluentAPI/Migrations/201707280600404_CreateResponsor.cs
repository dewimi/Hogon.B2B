namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateResponsor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 20),
                        Password = c.String(maxLength: 20),
                        ResponsorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Responsors", t => t.ResponsorId)
                .Index(t => t.ResponsorId);
            
            CreateTable(
                "dbo.Responsors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        IsEnable = c.Boolean(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "ResponsorId", "dbo.Responsors");
            DropIndex("dbo.Accounts", new[] { "ResponsorId" });
            DropTable("dbo.Responsors");
            DropTable("dbo.Accounts");
        }
    }
}
