namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.TestUsers");
            DropTable("dbo.Information");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Information",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pname = c.String(maxLength: 20),
                        Ename = c.String(maxLength: 20),
                        Brand = c.String(maxLength: 20),
                        ProductModel = c.String(maxLength: 20),
                        ProducingArea = c.String(maxLength: 20),
                        Quote = c.Single(nullable: false),
                        UploadName = c.String(maxLength: 500),
                        OriginalFileName = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TestUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
