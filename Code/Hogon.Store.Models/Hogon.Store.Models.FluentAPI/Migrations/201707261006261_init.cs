namespace Hogon.Store.Models.FluentAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.Authorities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MenuId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menus", t => t.MenuId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.MenuId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParentId = c.Guid(),
                        Code = c.String(maxLength: 50),
                        Name = c.String(maxLength: 20),
                        URL = c.String(maxLength: 60),
                        IsEnable = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        UpdatedTime = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        UpdaterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menus", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Functions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                        Code = c.String(),
                        IsEnable = c.Boolean(nullable: false),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        MenuId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menus", t => t.MenuId, cascadeDelete: true)
                .Index(t => t.MenuId);
            
            CreateTable(
                "dbo.Rela_Authority_Function",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AuthorityId = c.Guid(nullable: false),
                        FunctionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authorities", t => t.AuthorityId)
                .ForeignKey("dbo.Functions", t => t.FunctionId, cascadeDelete: true)
                .Index(t => t.AuthorityId)
                .Index(t => t.FunctionId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoleName = c.String(maxLength: 50),
                        IsEnable = c.Boolean(nullable: false),
                        IsAdministrator = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        UpdatedTime = c.DateTime(nullable: false),
                        CreatorId = c.Guid(nullable: false),
                        UpdaterId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rela_Role_User",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        UpdatedTime = c.DateTime(nullable: false),
                        Email = c.String(),
                        CreatorId = c.Int(nullable: false),
                        UpdaterId = c.Int(nullable: false),
                        RoleId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
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
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "UserNameIndexer");
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductName = c.String(maxLength: 20),
                        SearchPrimaryKey = c.String(maxLength: 20),
                        ProductDescription = c.String(maxLength: 20),
                        ProductCode = c.String(maxLength: 20),
                        SalerBasicPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalerMinPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalerMinPriceType = c.Int(nullable: false),
                        IsEffectMinPrice = c.Boolean(nullable: false),
                        IsEnableMinPriceWarning = c.Boolean(nullable: false),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        GoodsClass_Id = c.Guid(),
                        ProductType_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GoodsTypes", t => t.GoodsClass_Id)
                .ForeignKey("dbo.ProductTypes", t => t.ProductType_Id)
                .Index(t => t.GoodsClass_Id)
                .Index(t => t.ProductType_Id);
            
            CreateTable(
                "dbo.GoodsTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 20),
                        ParentId = c.Guid(),
                        Describe = c.String(),
                        Order = c.Int(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        ProductTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GoodsTypes", t => t.ParentId)
                .ForeignKey("dbo.ProductTypes", t => t.ProductTypeId, cascadeDelete: true)
                .Index(t => t.ParentId)
                .Index(t => t.ProductTypeId);
            
            CreateTable(
                "dbo.ServiceGoodsTemps",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ServiceName = c.String(maxLength: 20),
                        GoodsNum = c.String(maxLength: 20),
                        ServicePrice = c.Decimal(nullable: false, precision: 10, scale: 2),
                        ServiceSupplyName = c.String(maxLength: 20),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        GoodsTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GoodsTypes", t => t.GoodsTypeId, cascadeDelete: true)
                .Index(t => t.GoodsTypeId);
            
            CreateTable(
                "dbo.ProductTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TypeName = c.String(maxLength: 50),
                        CreatePerson = c.String(maxLength: 50),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(maxLength: 50),
                        UpdateTime = c.DateTime(nullable: false),
                        CategoryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductTypeCategories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.ProductTypeCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductTypeCategoryName = c.String(nullable: false, maxLength: 20),
                        Code = c.String(maxLength: 20),
                        ParentId = c.Guid(),
                        CreatePerson = c.String(maxLength: 20),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(maxLength: 20),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductTypeCategories", t => t.ParentId)
                .Index(t => t.ProductTypeCategoryName, unique: true, name: "IndexTypeCategoryName")
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.SpecTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SpecName = c.String(maxLength: 50),
                        SpecCatalog = c.String(maxLength: 20),
                        SpecRemark = c.String(maxLength: 200),
                        SpecSecondName = c.String(maxLength: 50),
                        DisplayName = c.String(maxLength: 50),
                        DisplayMode = c.String(maxLength: 50),
                        CreatePerson = c.String(maxLength: 50),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(maxLength: 50),
                        UpdateTime = c.DateTime(nullable: false),
                        ProductTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductTypes", t => t.ProductTypeId, cascadeDelete: true)
                .Index(t => t.ProductTypeId);
            
            CreateTable(
                "dbo.SpecParameterTemplates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParameterName = c.String(maxLength: 50),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        FileId = c.Guid(),
                        SpecType_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileUploads", t => t.FileId)
                .ForeignKey("dbo.SpecTypes", t => t.SpecType_Id)
                .Index(t => t.FileId)
                .Index(t => t.SpecType_Id);
            
            CreateTable(
                "dbo.FileUploads",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FileName = c.String(),
                        FileType = c.String(),
                        FileSize = c.String(),
                        UploadDateTime = c.DateTime(nullable: false),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Goods",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GoodsName = c.String(maxLength: 20),
                        GoodsCode = c.String(nullable: false, maxLength: 50),
                        GoodsAlias = c.String(maxLength: 20),
                        SalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        ServicePrice = c.Decimal(precision: 18, scale: 2),
                        ServiceDescription = c.String(maxLength: 300),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        ServiceGoodsId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Goods", t => t.ServiceGoodsId)
                .Index(t => t.GoodsCode, unique: true, name: "GoodsCodeIndexer")
                .Index(t => t.ServiceGoodsId);
            
            CreateTable(
                "dbo.Instructions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Subject = c.String(maxLength: 20),
                        Author = c.String(maxLength: 20),
                        AppIndustry = c.String(maxLength: 20),
                        Usage = c.String(maxLength: 20),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        Product_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.AppCases",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Subject = c.String(maxLength: 20),
                        Author = c.String(maxLength: 20),
                        AppIndustry = c.String(maxLength: 20),
                        Usage = c.String(maxLength: 20),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        Product_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppCases", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Instructions", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Goods", "ServiceGoodsId", "dbo.Goods");
            DropForeignKey("dbo.Products", "ProductType_Id", "dbo.ProductTypes");
            DropForeignKey("dbo.Products", "GoodsClass_Id", "dbo.GoodsTypes");
            DropForeignKey("dbo.GoodsTypes", "ProductTypeId", "dbo.ProductTypes");
            DropForeignKey("dbo.SpecParameterTemplates", "SpecType_Id", "dbo.SpecTypes");
            DropForeignKey("dbo.SpecParameterTemplates", "FileId", "dbo.FileUploads");
            DropForeignKey("dbo.SpecTypes", "ProductTypeId", "dbo.ProductTypes");
            DropForeignKey("dbo.ProductTypes", "CategoryId", "dbo.ProductTypeCategories");
            DropForeignKey("dbo.ProductTypeCategories", "ParentId", "dbo.ProductTypeCategories");
            DropForeignKey("dbo.ServiceGoodsTemps", "GoodsTypeId", "dbo.GoodsTypes");
            DropForeignKey("dbo.GoodsTypes", "ParentId", "dbo.GoodsTypes");
            DropForeignKey("dbo.Authorities", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Rela_Role_User", "UserId", "dbo.Users");
            DropForeignKey("dbo.Rela_Role_User", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Authorities", "MenuId", "dbo.Menus");
            DropForeignKey("dbo.Rela_Authority_Function", "FunctionId", "dbo.Functions");
            DropForeignKey("dbo.Rela_Authority_Function", "AuthorityId", "dbo.Authorities");
            DropForeignKey("dbo.Functions", "MenuId", "dbo.Menus");
            DropForeignKey("dbo.Menus", "ParentId", "dbo.Menus");
            DropIndex("dbo.AppCases", new[] { "Product_Id" });
            DropIndex("dbo.Instructions", new[] { "Product_Id" });
            DropIndex("dbo.Goods", new[] { "ServiceGoodsId" });
            DropIndex("dbo.Goods", "GoodsCodeIndexer");
            DropIndex("dbo.SpecParameterTemplates", new[] { "SpecType_Id" });
            DropIndex("dbo.SpecParameterTemplates", new[] { "FileId" });
            DropIndex("dbo.SpecTypes", new[] { "ProductTypeId" });
            DropIndex("dbo.ProductTypeCategories", new[] { "ParentId" });
            DropIndex("dbo.ProductTypeCategories", "IndexTypeCategoryName");
            DropIndex("dbo.ProductTypes", new[] { "CategoryId" });
            DropIndex("dbo.ServiceGoodsTemps", new[] { "GoodsTypeId" });
            DropIndex("dbo.GoodsTypes", new[] { "ProductTypeId" });
            DropIndex("dbo.GoodsTypes", new[] { "ParentId" });
            DropIndex("dbo.Products", new[] { "ProductType_Id" });
            DropIndex("dbo.Products", new[] { "GoodsClass_Id" });
            DropIndex("dbo.Users", "UserNameIndexer");
            DropIndex("dbo.Rela_Role_User", new[] { "UserId" });
            DropIndex("dbo.Rela_Role_User", new[] { "RoleId" });
            DropIndex("dbo.Rela_Authority_Function", new[] { "FunctionId" });
            DropIndex("dbo.Rela_Authority_Function", new[] { "AuthorityId" });
            DropIndex("dbo.Functions", new[] { "MenuId" });
            DropIndex("dbo.Menus", new[] { "ParentId" });
            DropIndex("dbo.Authorities", new[] { "RoleId" });
            DropIndex("dbo.Authorities", new[] { "MenuId" });
            DropTable("dbo.AppCases");
            DropTable("dbo.Instructions");
            DropTable("dbo.Goods");
            DropTable("dbo.FileUploads");
            DropTable("dbo.SpecParameterTemplates");
            DropTable("dbo.SpecTypes");
            DropTable("dbo.ProductTypeCategories");
            DropTable("dbo.ProductTypes");
            DropTable("dbo.ServiceGoodsTemps");
            DropTable("dbo.GoodsTypes");
            DropTable("dbo.Products");
            DropTable("dbo.Users");
            DropTable("dbo.Rela_Role_User");
            DropTable("dbo.Roles");
            DropTable("dbo.Rela_Authority_Function");
            DropTable("dbo.Functions");
            DropTable("dbo.Menus");
            DropTable("dbo.Authorities");
            DropTable("dbo.Information");
            DropTable("dbo.TestUsers");
        }
    }
}
