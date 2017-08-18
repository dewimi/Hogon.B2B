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
                "dbo.Staffs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        EnterpriseId = c.Guid(nullable: false),
                        PersonId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Enterprise", t => t.EnterpriseId)
                .ForeignKey("dbo.Person", t => t.PersonId)
                .Index(t => t.EnterpriseId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Rela_Role_Account",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        CreatedTime = c.DateTime(nullable: false),
                        UpdatedTime = c.DateTime(nullable: false),
                        Email = c.String(),
                        CreatorId = c.Int(nullable: false),
                        UpdaterId = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.RoleId)
                .Index(t => t.User_Id);
            
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
                        EnterpriseId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Enterprise", t => t.EnterpriseId)
                .Index(t => t.EnterpriseId);
            
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
                        Code = c.String(nullable: false, maxLength: 50),
                        Name = c.String(maxLength: 20),
                        URL = c.String(maxLength: 60),
                        Icon = c.String(),
                        Sort = c.Int(nullable: false),
                        IsEnable = c.Boolean(nullable: false),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menus", t => t.ParentId)
                .Index(t => t.ParentId)
                .Index(t => t.Code, unique: true, name: "MenuCodeIndexer");
            
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
                "dbo.FreebieCatalogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FreebieCatalogName = c.String(),
                        Sort = c.Int(nullable: false),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Freebies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsPublish = c.Boolean(nullable: false),
                        FreebiSortNum = c.Int(nullable: false),
                        LimitBuyAmount = c.Int(nullable: false),
                        Description = c.String(),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        FreebieCatalogId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FreebieCatalogs", t => t.FreebieCatalogId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.FreebieCatalogId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.FreebieLines",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Quota = c.Int(nullable: false),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        FreebieId = c.Guid(nullable: false),
                        ProductGoodsId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Freebies", t => t.FreebieId, cascadeDelete: true)
                .ForeignKey("dbo.Goods", t => t.ProductGoodsId)
                .Index(t => t.FreebieId)
                .Index(t => t.ProductGoodsId);
            
            CreateTable(
                "dbo.Goods",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GoodsName = c.String(maxLength: 20),
                        GoodsCode = c.String(nullable: false, maxLength: 50),
                        GoodsDesription = c.String(maxLength: 300),
                        GoodsAlias = c.String(maxLength: 20),
                        SalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsAvailable = c.Boolean(nullable: false),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        SearchKeywords = c.String(),
                        IsDefaultGoods = c.Boolean(),
                        Weight = c.Decimal(precision: 18, scale: 2),
                        DisplaySpecParameterTemplateName = c.String(),
                        ServicerName = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        ServiceGoods_ProductId = c.Guid(),
                        FileUpload_Id = c.Guid(),
                        ProductGoodsId = c.Guid(),
                        ProductId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ServiceGoods_ProductId)
                .ForeignKey("dbo.FileUploads", t => t.FileUpload_Id)
                .ForeignKey("dbo.Goods", t => t.ProductGoodsId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.GoodsCode, unique: true, name: "GoodsCodeIndexer")
                .Index(t => t.ServiceGoods_ProductId)
                .Index(t => t.FileUpload_Id)
                .Index(t => t.ProductGoodsId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.FileUploads",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FileName = c.String(),
                        FileType = c.String(),
                        FileSize = c.String(),
                        Url = c.String(),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        ProductGoods_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileUploads", t => t.FileId)
                .ForeignKey("dbo.SpecTypes", t => t.SpecType_Id)
                .ForeignKey("dbo.Goods", t => t.ProductGoods_Id)
                .Index(t => t.FileId)
                .Index(t => t.SpecType_Id)
                .Index(t => t.ProductGoods_Id);
            
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
                "dbo.GoodsTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 20),
                        ParentId = c.Guid(),
                        Describe = c.String(),
                        Order = c.Int(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        Icon = c.String(),
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
                "dbo.Rela_Brand_GoodsType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BrandId = c.Guid(nullable: false),
                        GoodsTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.GoodsTypes", t => t.GoodsTypeId, cascadeDelete: true)
                .Index(t => t.BrandId)
                .Index(t => t.GoodsTypeId);
            
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BrandName = c.String(),
                        BrandLogo = c.String(),
                        BrandAlias = c.String(),
                        Url = c.String(),
                        Nation = c.String(),
                        Country = c.String(),
                        City = c.String(),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        FileUpload_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileUploads", t => t.FileUpload_Id)
                .Index(t => t.FileUpload_Id);
            
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
                        DiplaySpecType = c.String(),
                        SpecParameterTemplate = c.String(),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        BrandId = c.Guid(nullable: false),
                        ProductType_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.ProductTypes", t => t.ProductType_Id)
                .Index(t => t.BrandId)
                .Index(t => t.ProductType_Id);
            
            CreateTable(
                "dbo.Rel_Promotion_Goods",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GoodsId = c.Guid(nullable: false),
                        PromotionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Goods", t => t.GoodsId, cascadeDelete: true)
                .ForeignKey("dbo.PromotinRules", t => t.PromotionId, cascadeDelete: true)
                .Index(t => t.GoodsId)
                .Index(t => t.PromotionId);
            
            CreateTable(
                "dbo.PromotinRules",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rela_Goods_GoodsType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        GoodsId = c.Guid(nullable: false),
                        GoodsTypeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Goods", t => t.GoodsId, cascadeDelete: true)
                .ForeignKey("dbo.GoodsTypes", t => t.GoodsTypeId, cascadeDelete: true)
                .Index(t => t.GoodsId)
                .Index(t => t.GoodsTypeId);
            
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
                        FileUpload_Id = c.Guid(),
                        Product_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileUploads", t => t.FileUpload_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.FileUpload_Id)
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
                        FileUpload_Id = c.Guid(),
                        Product_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileUploads", t => t.FileUpload_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.FileUpload_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Enterprise",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                        Password = c.String(maxLength: 50),
                        IsEnable = c.Boolean(nullable: false),
                        PhoneNumber = c.String(),
                        EmailAddress = c.String(),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                        ConnectPeopleName = c.String(),
                        Department = c.String(),
                        EnterpriseName = c.String(),
                        EnterpriseAddress = c.String(),
                        EnterpriseType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                        Password = c.String(maxLength: 50),
                        IsEnable = c.Boolean(nullable: false),
                        PhoneNumber = c.String(),
                        EmailAddress = c.String(),
                        CreatePerson = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UpdatePerson = c.String(),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppCases", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.AppCases", "FileUpload_Id", "dbo.FileUploads");
            DropForeignKey("dbo.Instructions", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Instructions", "FileUpload_Id", "dbo.FileUploads");
            DropForeignKey("dbo.Freebies", "ProductId", "dbo.Products");
            DropForeignKey("dbo.FreebieLines", "ProductGoodsId", "dbo.Goods");
            DropForeignKey("dbo.SpecParameterTemplates", "ProductGoods_Id", "dbo.Goods");
            DropForeignKey("dbo.Goods", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Goods", "ProductGoodsId", "dbo.Goods");
            DropForeignKey("dbo.Goods", "FileUpload_Id", "dbo.FileUploads");
            DropForeignKey("dbo.SpecParameterTemplates", "SpecType_Id", "dbo.SpecTypes");
            DropForeignKey("dbo.SpecTypes", "ProductTypeId", "dbo.ProductTypes");
            DropForeignKey("dbo.ProductTypes", "CategoryId", "dbo.ProductTypeCategories");
            DropForeignKey("dbo.ProductTypeCategories", "ParentId", "dbo.ProductTypeCategories");
            DropForeignKey("dbo.Rela_Brand_GoodsType", "GoodsTypeId", "dbo.GoodsTypes");
            DropForeignKey("dbo.Rela_Brand_GoodsType", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Rela_Goods_GoodsType", "GoodsTypeId", "dbo.GoodsTypes");
            DropForeignKey("dbo.Rela_Goods_GoodsType", "GoodsId", "dbo.Goods");
            DropForeignKey("dbo.Rel_Promotion_Goods", "PromotionId", "dbo.PromotinRules");
            DropForeignKey("dbo.Rel_Promotion_Goods", "GoodsId", "dbo.Goods");
            DropForeignKey("dbo.Goods", "ServiceGoods_ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductType_Id", "dbo.ProductTypes");
            DropForeignKey("dbo.Products", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Brands", "FileUpload_Id", "dbo.FileUploads");
            DropForeignKey("dbo.GoodsTypes", "ProductTypeId", "dbo.ProductTypes");
            DropForeignKey("dbo.GoodsTypes", "ParentId", "dbo.GoodsTypes");
            DropForeignKey("dbo.SpecParameterTemplates", "FileId", "dbo.FileUploads");
            DropForeignKey("dbo.FreebieLines", "FreebieId", "dbo.Freebies");
            DropForeignKey("dbo.Freebies", "FreebieCatalogId", "dbo.FreebieCatalogs");
            DropForeignKey("dbo.Rela_Role_Account", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Staffs", "PersonId", "dbo.Person");
            DropForeignKey("dbo.Staffs", "EnterpriseId", "dbo.Enterprise");
            DropForeignKey("dbo.Rela_Role_Account", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Roles", "EnterpriseId", "dbo.Enterprise");
            DropForeignKey("dbo.Authorities", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Authorities", "MenuId", "dbo.Menus");
            DropForeignKey("dbo.Rela_Authority_Function", "FunctionId", "dbo.Functions");
            DropForeignKey("dbo.Rela_Authority_Function", "AuthorityId", "dbo.Authorities");
            DropForeignKey("dbo.Functions", "MenuId", "dbo.Menus");
            DropForeignKey("dbo.Menus", "ParentId", "dbo.Menus");
            DropIndex("dbo.AppCases", new[] { "Product_Id" });
            DropIndex("dbo.AppCases", new[] { "FileUpload_Id" });
            DropIndex("dbo.Instructions", new[] { "Product_Id" });
            DropIndex("dbo.Instructions", new[] { "FileUpload_Id" });
            DropIndex("dbo.ProductTypeCategories", new[] { "ParentId" });
            DropIndex("dbo.ProductTypeCategories", "IndexTypeCategoryName");
            DropIndex("dbo.Rela_Goods_GoodsType", new[] { "GoodsTypeId" });
            DropIndex("dbo.Rela_Goods_GoodsType", new[] { "GoodsId" });
            DropIndex("dbo.Rel_Promotion_Goods", new[] { "PromotionId" });
            DropIndex("dbo.Rel_Promotion_Goods", new[] { "GoodsId" });
            DropIndex("dbo.Products", new[] { "ProductType_Id" });
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropIndex("dbo.Brands", new[] { "FileUpload_Id" });
            DropIndex("dbo.Rela_Brand_GoodsType", new[] { "GoodsTypeId" });
            DropIndex("dbo.Rela_Brand_GoodsType", new[] { "BrandId" });
            DropIndex("dbo.GoodsTypes", new[] { "ProductTypeId" });
            DropIndex("dbo.GoodsTypes", new[] { "ParentId" });
            DropIndex("dbo.ProductTypes", new[] { "CategoryId" });
            DropIndex("dbo.SpecTypes", new[] { "ProductTypeId" });
            DropIndex("dbo.SpecParameterTemplates", new[] { "ProductGoods_Id" });
            DropIndex("dbo.SpecParameterTemplates", new[] { "SpecType_Id" });
            DropIndex("dbo.SpecParameterTemplates", new[] { "FileId" });
            DropIndex("dbo.Goods", new[] { "ProductId" });
            DropIndex("dbo.Goods", new[] { "ProductGoodsId" });
            DropIndex("dbo.Goods", new[] { "FileUpload_Id" });
            DropIndex("dbo.Goods", new[] { "ServiceGoods_ProductId" });
            DropIndex("dbo.Goods", "GoodsCodeIndexer");
            DropIndex("dbo.FreebieLines", new[] { "ProductGoodsId" });
            DropIndex("dbo.FreebieLines", new[] { "FreebieId" });
            DropIndex("dbo.Freebies", new[] { "ProductId" });
            DropIndex("dbo.Freebies", new[] { "FreebieCatalogId" });
            DropIndex("dbo.Users", "UserNameIndexer");
            DropIndex("dbo.Rela_Authority_Function", new[] { "FunctionId" });
            DropIndex("dbo.Rela_Authority_Function", new[] { "AuthorityId" });
            DropIndex("dbo.Functions", new[] { "MenuId" });
            DropIndex("dbo.Menus", "MenuCodeIndexer");
            DropIndex("dbo.Menus", new[] { "ParentId" });
            DropIndex("dbo.Authorities", new[] { "RoleId" });
            DropIndex("dbo.Authorities", new[] { "MenuId" });
            DropIndex("dbo.Roles", new[] { "EnterpriseId" });
            DropIndex("dbo.Rela_Role_Account", new[] { "User_Id" });
            DropIndex("dbo.Rela_Role_Account", new[] { "RoleId" });
            DropIndex("dbo.Staffs", new[] { "PersonId" });
            DropIndex("dbo.Staffs", new[] { "EnterpriseId" });
            DropTable("dbo.Person");
            DropTable("dbo.Enterprise");
            DropTable("dbo.AppCases");
            DropTable("dbo.Instructions");
            DropTable("dbo.ProductTypeCategories");
            DropTable("dbo.Rela_Goods_GoodsType");
            DropTable("dbo.PromotinRules");
            DropTable("dbo.Rel_Promotion_Goods");
            DropTable("dbo.Products");
            DropTable("dbo.Brands");
            DropTable("dbo.Rela_Brand_GoodsType");
            DropTable("dbo.GoodsTypes");
            DropTable("dbo.ProductTypes");
            DropTable("dbo.SpecTypes");
            DropTable("dbo.SpecParameterTemplates");
            DropTable("dbo.FileUploads");
            DropTable("dbo.Goods");
            DropTable("dbo.FreebieLines");
            DropTable("dbo.Freebies");
            DropTable("dbo.FreebieCatalogs");
            DropTable("dbo.Users");
            DropTable("dbo.Rela_Authority_Function");
            DropTable("dbo.Functions");
            DropTable("dbo.Menus");
            DropTable("dbo.Authorities");
            DropTable("dbo.Roles");
            DropTable("dbo.Rela_Role_Account");
            DropTable("dbo.Staffs");
            DropTable("dbo.Information");
            DropTable("dbo.TestUsers");
        }
    }
}
