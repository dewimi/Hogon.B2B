namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatefileupload : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FileUploads", "Url", c => c.String());
            DropColumn("dbo.FileUploads", "UploadDateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FileUploads", "UploadDateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.FileUploads", "Url");
        }
    }
}
