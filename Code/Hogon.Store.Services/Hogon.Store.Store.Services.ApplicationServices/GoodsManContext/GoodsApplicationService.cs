using AutoMapper;
using Hogon.Framework.Core.Common;
using Hogon.Framework.Core.UnitOfWork;
using Hogon.Store.Models.Dto.GoodsMan;
using Hogon.Store.Models.Entities.GoodsMan;
using Hogon.Store.Repositories.GoodsMan;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Hogon.Store.Services.ApplicationServices.GoodsManContext
{
    public class GoodsApplicationService : BaseApplicationService
    {
        GoodsRepository goodsReps = new GoodsRepository();

        ProductRepository productReps = new ProductRepository();

        ProductTypeRepository productTypeReps = new ProductTypeRepository();

        ProductGoodsRepository productGoodsReps = new ProductGoodsRepository();

        SpecTypeRepository specTypeReps = new SpecTypeRepository();

        SpecParameterTemplateRepository specParameterTemplateReps = new SpecParameterTemplateRepository();


        ServiceGoodsRepository serviceGoodsReps = new ServiceGoodsRepository();

        GoodsTypeRepository goodsTypeReps = new GoodsTypeRepository();

        BrandRepository brandReps = new BrandRepository();
        AppCaseRepository appCaseReps = new AppCaseRepository();
        InstructionRepository instructionReps = new InstructionRepository();

        /// <summary>
        /// 根据Code查询是否重复
        /// </summary>
        /// <param name="goodsCode"></param>
        /// <returns></returns>
        public int FindGoodsByCode(string goodsCode)
        {
            var goods = goodsReps.FindBy(m => m.GoodsCode == goodsCode);
            return goods.Count();
        }

        #region 下拉框数据
        /// <summary>
        /// 查询所有品牌
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoBrand> FindAllBrand()
        {
            var brandS = brandReps.FindAll();
            var dtoBrandS = brandS.ConvertTo<Brand, DtoBrand>();
            return dtoBrandS;
        }

        /// <summary>
        /// 查询所有产品类型
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoProductType> FindAllProductType()
        {
            var productTypeS = productTypeReps.FindAll();
            var dtoProductTypeS = productTypeS.ConvertTo<ProductType, DtoProductType>();
            return dtoProductTypeS;
        }

        /// <summary>
        /// 查询所有商品类型
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoGoodsType> FindAllGoodsType()
        {
            var goodsType = goodsTypeReps.FindAll();
            var dtoGoodsType = goodsType.ConvertTo<GoodsType, DtoGoodsType>();
            return dtoGoodsType;
        }

        /// <summary>
        /// 查询所有规格类型
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoSpecType> FindAllSpecType()
        {
            var specTypeS = specTypeReps.FindAll().Where(m => m.SpecParameterTemplate.Count > 0);
            var dtoSpecTypeS = specTypeS.ConvertTo<SpecType, DtoSpecType>();
            return dtoSpecTypeS;
        }

        /// <summary>
        /// 根据规格类型Id查询规格参数
        /// </summary>
        /// <param name="dtoSpecType"></param>
        /// <returns></returns>
        public IQueryable<DtoSpecTypeParameter> FindAllSpecTypeParameter(DtoSpecType dtoSpecType)
        {
            if (dtoSpecType.Ids != null)
            {
                if (dtoSpecType.Ids.Count() > 0)
                {
                    List<DtoSpecTypeParameter> dtoSpecTypeParameterS = new List<DtoSpecTypeParameter>();
                    foreach (var item in dtoSpecType.Ids)
                    {
                        var specType = specTypeReps.FindAll().Where(m => m.Id == item).First();

                        var dtoSpecTypeParameter = specType.SpecParameterTemplate.Select(m => new DtoSpecTypeParameter()
                        {
                            Id = m.Id,
                            ParameterName = m.ParameterName,
                        }).ToList();
                        foreach (var SpecTypeParameter in dtoSpecTypeParameter)
                        {
                            dtoSpecTypeParameterS.Add(SpecTypeParameter);
                        }
                    }

                    return dtoSpecTypeParameterS.AsQueryable();
                }
                else
                { return null; }
            }
            return null;
        }
        #endregion

        #region 产品操作
        /// <summary>
        /// 查询所有产品
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoProduct> GetAllProductInfo()
        {
            var Product = productReps.FindAll().OrderBy(t => t.UpdateTime);
            var dtoGoods = Product.Select(m => new DtoProduct()
            {
                Id = m.Id,
                ProductName = m.ProductName,
                SearchPrimaryKey = m.SearchPrimaryKey,
                ProductDescription = m.ProductDescription,
                SalerBasicPrice = m.SalerBasicPrice,
                SalerMinPrice = m.SalerMinPrice,
                CreatePerson = m.CreatePerson,
                CreateTime = m.CreateTime,
                UpdatePerson = m.UpdatePerson,
                UpdateTime = m.UpdateTime,
                ProductTypeName = m.ProductType.TypeName,
                BrandName = m.Brand.BrandName,
                ProductCode = m.ProductCode,
            });

            return dtoGoods;
        }

        /// <summary>
        /// 根据Id查询指定产品
        /// </summary>
        /// <param name="ProductId">产品Id</param>
        /// <returns></returns>
        public DtoProduct FindProductById(Guid ProductId)
        {
            var product = productReps.FindBy(m => m.Id == ProductId);

            var dtoProduct = product.Select(m => new DtoProduct()
            {
                Id = m.Id,
                ProductName = m.ProductName,
                SearchPrimaryKey = m.SearchPrimaryKey,
                ProductDescription = m.ProductDescription,
                SalerBasicPrice = m.SalerBasicPrice,
                SalerMinPrice = m.SalerMinPrice,
                CreatePerson = m.CreatePerson,
                CreateTime = m.CreateTime,
                UpdatePerson = m.UpdatePerson,
                UpdateTime = m.UpdateTime,
                IsEffectMinPrice = m.IsEffectMinPrice,
                IsEnableMinPriceWarning = m.IsEnableMinPriceWarning,
                BrandId = m.Brand.Id,
                ProductTypeId = m.ProductType.Id,
                ProductCode = m.ProductCode,
            }).First();

            return dtoProduct;
        }

        /// <summary>
        /// 根据产品名称查询指定产品列表
        /// </summary>
        /// <param name="ProductName">产品名称</param>
        /// <returns></returns>
        public int FindProductByRepetition(DtoProduct dtoProduct)
        {
            int count = 0;
            if (dtoProduct.Id != null)
            {
                //修改时当前产品名字可以不更改
                count = productReps.FindAll().Where(m => m.Id != dtoProduct.Id && m.ProductName == dtoProduct.ProductName).Count();
            }
            else
            {
                //新增时产品名称不可重复
                count = productReps.FindAll().Where(p => p.ProductName == dtoProduct.ProductName).Count();
            }
            if (count > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }


        ///<summary>
        /// 保存产品信息
        ///</summary>
        ///<param name ="dtoProduct">产品DTO</param>
        ///<param name ="ProductId">产品Id</param>
        ///<returns></returns>
        public Guid SaveProduct(DtoProduct dtoProduct)
        {
            if (dtoProduct.Id == Guid.Empty)
            {
                //id为空, 对产品信息进行添加操作
                Mapper.Initialize(cfg => cfg.CreateMap<DtoProduct, Product>());
                var product = Mapper.Map<Product>(dtoProduct);

                product.Brand = brandReps.FindBy(m => m.Id == dtoProduct.BrandId).First();

                product.ProductType = productTypeReps.FindBy(m => m.Id == dtoProduct.ProductTypeId).First();

                productReps.Add(product);
                Commit();
                return product.Id;
            }
            else
            {
                //id不为空,对产品信息进行修改工作

                var product = productReps.FindBy(m => m.Id == dtoProduct.Id).OrderBy(m => m.UpdateTime).First();

                Mapper.Initialize(cfg => cfg.CreateMap<DtoProduct, Product>());
                Mapper.Map(dtoProduct, product);

                Commit();
                return product.Id;
            }
        }


        /// <summary>
        /// 删除指定产品
        /// </summary>
        /// <param name="ProductId">产品Id</param>
        /// <returns></returns>
        public void RemoveProduct(Guid ProductId)
        {
            var Product = productReps.FindBy(t => t.Id == ProductId).First();

            productReps.Remove(Product);
            Commit();
        }
        #endregion

        #region 服务商品操作
        /// <summary>
        /// 查询所有服务商品
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoServiceGoods> FindAllServiceGoods()
        {
            var serviceGoods = goodsReps.FindAll().OfType<ServiceGoods>();

            var dtoServiceGoods = serviceGoods.Select(s => new DtoServiceGoods()
            {
                SalePrice = s.SalePrice,
                CreatePerson = s.CreatePerson,
                CreateTime = s.CreateTime,
                GoodsAlias = s.GoodsAlias,
                GoodsCode = s.GoodsCode,
                GoodsDesription = s.GoodsDesription,
                GoodsName = s.GoodsName,
                Id = s.Id,
                //IsAvailable = s.IsAvailable,
                UpdatePerson = s.UpdatePerson,
                UpdateTime = s.UpdateTime,
                GoodsTypeNames = s.Rela_Goods_GoodsType.Select(m => m.GoodsType.Name),
                GoodsTypeId = s.Rela_Goods_GoodsType.Select(m => m.GoodsType.Id),
            }).ToList();

            return dtoServiceGoods.AsQueryable();
        }

        /// <summary>
        /// 根据Id集合查询服务商品
        /// </summary>
        /// <param name="ListId"></param>
        /// <returns></returns>
        public IQueryable<DtoServiceGoods> FindServiceGoodsByListId(ICollection<Guid> ListId)
        {
            ICollection<DtoServiceGoods> dtoServiceGoods = new List<DtoServiceGoods>();
            foreach (var id in ListId)
            {
                var serviceGoods = serviceGoodsReps.FindBy(m => m.Id == id).Select(s => new DtoServiceGoods()
                {
                    GoodsCode = s.GoodsCode,
                    GoodsDesription = s.GoodsDesription,
                    GoodsName = s.GoodsName,
                    GoodsTypeId = s.Rela_Goods_GoodsType.Select(m => m.GoodsType.Id),
                    SalePrice = s.SalePrice,
                    Id = s.Id,
                    GoodsTypeNames = s.Rela_Goods_GoodsType.Select(m => m.GoodsType.Name),
                    //IsAvailable=s.IsAvailable,                    
                }).First();
                dtoServiceGoods.Add(serviceGoods);
            }
            return dtoServiceGoods.AsQueryable();
        }

        /// <summary>
        /// 根据Id查询服务商品
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DtoServiceGoods FindServiceGoodsById(Guid Id)
        {
            var serviceGoods = goodsReps.FindBy(g => g.Id == Id).OfType<ServiceGoods>();

            var dtoServiceGoods = serviceGoods.Select(s => new DtoServiceGoods()
            {
                SalePrice = s.SalePrice,
                CreatePerson = s.CreatePerson,
                CreateTime = s.CreateTime,
                GoodsAlias = s.GoodsAlias,
                GoodsCode = s.GoodsCode,
                GoodsDesription = s.GoodsDesription,
                GoodsName = s.GoodsName,
                Id = s.Id,
                //IsAvailable = s.IsAvailable,
                UpdatePerson = s.UpdatePerson,
                UpdateTime = s.UpdateTime,
                GoodsTypeId = s.Rela_Goods_GoodsType.Select(m => m.GoodsType.Id),
                GoodsTypeNames = s.Rela_Goods_GoodsType.Select(m => m.GoodsType.Name),
            }).First();

            return dtoServiceGoods;
        }

        /// <summary>
        /// 根据GoodsCode查询是否重复
        /// </summary>
        /// <param name="dtoServiceGoods"></param>
        /// <returns></returns>
        public int FindServiceGoodsByGoodsCode(DtoServiceGoods dtoServiceGoods)
        {
            if (dtoServiceGoods.Id != null)
            {
                var serviceGoods = goodsReps.FindAll().OfType<ServiceGoods>().Where(g => g.GoodsCode == dtoServiceGoods.GoodsCode && g.Id != dtoServiceGoods.Id);

                return serviceGoods.Count();
            }
            else
            {
                var serviceGoods = goodsReps.FindBy(g => g.GoodsCode == dtoServiceGoods.GoodsCode).OfType<ServiceGoods>();

                return serviceGoods.Count();
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="dtoServiceGoods"></param>
        /// <returns></returns>
        public Guid SaveServiceGoods(DtoServiceGoods dtoServiceGoods)
        {
            if (dtoServiceGoods.Id == Guid.Empty)
            {
                //Id为空时进行添加
                ServiceGoods serviceGoods = new ServiceGoods();
                Mapper.Initialize(cfg => cfg.CreateMap<DtoServiceGoods, ServiceGoods>());
                Mapper.Map<DtoServiceGoods, ServiceGoods>(dtoServiceGoods, serviceGoods);

                if (dtoServiceGoods.GoodsTypeId.Count() != 0)
                {
                    foreach (Guid goodsTypeIds in dtoServiceGoods.GoodsTypeId)
                    {
                        Rela_Goods_GoodsType rela_Goods_GoodsType = new Rela_Goods_GoodsType();
                        rela_Goods_GoodsType.Goods = serviceGoods;

                        var goodsType = goodsTypeReps.FindBy(p => p.Id == goodsTypeIds).First();

                        rela_Goods_GoodsType.GoodsType = goodsType;
                        rela_Goods_GoodsType.Id = Guid.NewGuid();
                        serviceGoods.Rela_Goods_GoodsType.Add(rela_Goods_GoodsType);
                    }
                }
                goodsReps.Add(serviceGoods);

                Commit();

                return serviceGoods.Id;
            }
            else
            {
                //Id不为空时进行修改
                var serviceGoods = goodsReps.FindBy(p => p.Id == dtoServiceGoods.Id)
                .OfType<ServiceGoods>().First();

                Mapper.Initialize(cfg => cfg.CreateMap<DtoServiceGoods, ServiceGoods>());
                Mapper.Map<DtoServiceGoods, ServiceGoods>(dtoServiceGoods, serviceGoods);

                #region
                //修改商品------商品分类关系表
                //var rela_Goods_GoodsTypeS = rela_Goods_GoodsTypeReps.FindBy(s => s.Goods.Id == serviceGoods.Id);
                //if (rela_Goods_GoodsTypeS.Count() != 0)
                //{
                //    foreach (var item in rela_Goods_GoodsTypeS)
                //    {
                //        goodsReps.DeleteRela_Goods_GoodsType(item);
                //    }
                //}

                //    var goodsType = goodsTypeReps.FindBy(p => p.Id == item).First();
                //    Rela_Goods_GoodsType rela_Goods_GoodsType = new Rela_Goods_GoodsType();
                //    rela_Goods_GoodsType.Id = Guid.NewGuid();

                //    rela_Goods_GoodsType.GoodsType = goodsType;
                #endregion
                //删除商品-商品分类关系表对应数据
                if (serviceGoods.Rela_Goods_GoodsType.Count > 0)
                {
                    var rela_Goods_GoodsTypeS = goodsReps.FindBy(s => s.Id == dtoServiceGoods.Id).SelectMany(m => m.Rela_Goods_GoodsType);
                    foreach (var item in rela_Goods_GoodsTypeS)
                    {
                        goodsReps.DeleteRela_Goods_GoodsType(item);
                    }
                }
                //重建关系
                if (dtoServiceGoods.GoodsTypeId.Count() > 0)
                {
                    foreach (var item in dtoServiceGoods.GoodsTypeId)
                    {
                        Rela_Goods_GoodsType rela_Goods_GoodsType = new Rela_Goods_GoodsType();
                        rela_Goods_GoodsType.Goods = serviceGoods;

                        var goodsType = goodsTypeReps.FindBy(p => p.Id == item).First();

                        rela_Goods_GoodsType.GoodsType = goodsType;
                        rela_Goods_GoodsType.Id = Guid.NewGuid();

                        serviceGoods.Rela_Goods_GoodsType.Add(rela_Goods_GoodsType);
                    }
                }

                Commit();
                return serviceGoods.Id;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        public void Remove(Guid Id)
        {
            var goods = goodsReps.FindBy(p => p.Id == Id).First();

            goodsReps.Remove(goods);

            Commit();
        }
        #endregion

        #region 商品信息操作
        /// <summary>
        /// 查询所有商品信息
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoProductGoods> GetAllProductGoods()
        {
            var productGoods = productGoodsReps.FindAll().OrderBy(m => m.UpdateTime);

            var dtoproductGoods = productGoods.ConvertTo<ProductGoods, DtoProductGoods>();
            return dtoproductGoods;
        }

        /// <summary>
        /// 根据Id查询指定商品信息
        /// </summary>
        /// <param name="goodsManId">产品Id</param>
        /// <returns></returns>
        public DtoProductGoods FindProductGoodsById(Guid productGooodsId)
        {
            var productGooods = productGoodsReps.FindBy(m => m.Id == productGooodsId).OrderBy(m => m.UpdateTime).First();

            Mapper.Initialize(cfg => cfg.CreateMap<DtoProductGoods, ProductGoods>());
            var dtoProductGooods = Mapper.Map<DtoProductGoods>(productGooods);

            return dtoProductGooods;
        }

        /// <summary>
        /// 根据商品数据集合生成商品Model
        /// </summary>
        /// <param name="listProductGoods"></param>
        /// <returns></returns>
        public DtoProductGoods TransformForProductGoods(List<string> listProductGoods)
        {
            DtoProductGoods dtoProductGoods = new DtoProductGoods();
            dtoProductGoods.DtoSpecParameterTemplateS = new List<DtoSpecParameterTemplate>();
            dtoProductGoods.GoodsName = listProductGoods[0];
            var typename = listProductGoods[1];
            dtoProductGoods.Product = productReps.FindBy(m => m.ProductType.TypeName == typename).First();
            foreach (var item in listProductGoods[2].Split(';'))
            {
                if (item != "")
                {
                    var dtoSpecParameterTemplate = specParameterTemplateReps.FindAll().Where(m => m.ParameterName == item).First();

                    Mapper.Initialize(cfg => cfg.CreateMap<SpecParameterTemplate, DtoSpecParameterTemplate>());
                    var specParams = Mapper.Map<DtoSpecParameterTemplate>(dtoSpecParameterTemplate);

                    dtoProductGoods.DtoSpecParameterTemplateS.Add(specParams);
                }
            }
            dtoProductGoods.GoodsCode = listProductGoods[3];
            dtoProductGoods.GoodsAlias = listProductGoods[4];
            dtoProductGoods.SalePrice = decimal.Parse(listProductGoods[5]);
            return dtoProductGoods;
        }

        ///<summary>
        /// 保存商品信息
        ///</summary>
        ///<param name ="dtoProductGooods">商品DTO</param>
        ///<returns></returns>
        public ProductGoods SaveProductGoods(DtoProductGoods dtoProductGooods)
        {
            if (dtoProductGooods.Id == Guid.Empty)
            {
                //id为空, 对商品信息进行添加操作
                Mapper.Initialize(cfg => cfg.CreateMap<DtoProductGoods, ProductGoods>());
                var productGoods = Mapper.Map<ProductGoods>(dtoProductGooods);

                productGoodsReps.Add(productGoods);
                Commit();

                return productGoods;
            }
            else
            {
                //id不为空,对商品信息进行修改工作
                var productGoods = productGoodsReps.FindBy(m => m.Id == dtoProductGooods.Id)
                    .OrderBy(m => m.UpdateTime).First();

                Mapper.Initialize(cfg => cfg.CreateMap<DtoProductGoods, ProductGoods>());
                Mapper.Map(dtoProductGooods, productGoods);

                Commit();
                return productGoods;
            }
        }

        /// <summary>
        /// 删除指定商品信息
        /// </summary>
        /// <param name="productGoodsId">商品Id</param>
        /// <returns></returns>
        public void RemoveProductGoods(Guid productGoodsId)
        {
            var productGoods = productGoodsReps.FindBy(t => t.Id == productGoodsId).First();

            productGoodsReps.Remove(productGoods);
            Commit();
        }
        #endregion

        #region 应用案例信息操作
        /// <summary>
        /// 查询所有应用案例信息
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoAppCase> GetAllAppCase()
        {
            var appCase = appCaseReps.FindAll().OrderBy(m => m.UpdateTime);

            var dtoAppCase = appCase.ConvertTo<AppCase, DtoAppCase>();
            return dtoAppCase;
        }

        /// <summary>
        /// 根据Id查询指定应用案例信息
        /// </summary>
        /// <param name="AppCaseId">应用案例Id</param>
        /// <returns></returns>
        public DtoAppCase GetAppCaseById(Guid AppCaseId)
        {
            var appCase = appCaseReps.FindBy(m => m.Id == AppCaseId).OrderBy(m => m.UpdateTime);

            var dtoAppCase = appCase.ConvertTo<AppCase, DtoAppCase>().First();
            return dtoAppCase;
        }

        /// <summary>
        /// 根据主题查询指定应用案例列表
        /// </summary>
        /// <param name="Subject">主题</param>
        /// <returns></returns>
        public List<DtoAppCase> GetAppCaseInfoBySubject(string Subject)
        {
            var appCase = appCaseReps.FindAll().Where(m => m.Subject == Subject).OrderBy(m => m.UpdateTime);

            var appCaseList = appCase.ConvertTo<AppCase, DtoAppCase>().ToList();
            return appCaseList;
        }

        /// <summary>
        /// 根据作者查询指定应用案例列表
        /// </summary>
        /// <param name="Author">作者</param>
        /// <returns></returns>
        public List<DtoAppCase> GetAppCaseInfoByAuthor(string Author)
        {
            var appCase = appCaseReps.FindAll().Where(m => m.Author == Author).OrderBy(m => m.UpdateTime);

            var appCaseList = appCase.ConvertTo<AppCase, DtoAppCase>().ToList();
            return appCaseList;
        }

        /// <summary>
        /// 根据应用行业查询指定应用案例列表
        /// </summary>
        /// <param name="AppIndustry">应用行业</param>
        /// <returns></returns>
        public List<DtoAppCase> GetAppCaseInfoByAppIndustry(string AppIndustry)
        {
            var appCase = appCaseReps.FindAll().Where(m => m.AppIndustry == AppIndustry).OrderBy(m => m.UpdateTime);

            var appCaseList = appCase.ConvertTo<AppCase, DtoAppCase>().ToList();
            return appCaseList;
        }

        /// <summary>
        /// 根据用途查询指定应用案例列表
        /// </summary>
        /// <param name="Usage">用途</param>
        /// <returns></returns>
        public List<DtoAppCase> GetAppCaseInfoByAppUsage(string Usage)
        {
            var appCase = appCaseReps.FindAll().Where(m => m.Usage == Usage).OrderBy(m => m.UpdateTime);

            var appCaseList = appCase.ConvertTo<AppCase, DtoAppCase>().ToList();
            return appCaseList;
        }

        ///<summary>
        /// 保存应用案例信息
        ///</summary>
        ///<param name ="dtoAppCase">应用案例DTO</param>
        ///<param name ="AppCaseId">应用案例Id</param>
        ///<returns></returns>
        public Guid SaveAppCase(DtoAppCase dtoAppCase)
        {
            if (dtoAppCase.Id == new Guid())
            {
                //id为空, 对应用案例信息进行添加操作
                AppCase appCase = new AppCase();
                Mapper.Initialize(cfg => cfg.CreateMap<DtoAppCase, AppCase>());
                var appCaseData = Mapper.Map<AppCase>(appCase);

                appCaseReps.Add(appCaseData);
                Commit();
                return appCaseData.Id;
            }
            else
            {
                //id不为空,对应用案例信息进行修改工作
                var appCase = appCaseReps.FindBy(i => i.Id == dtoAppCase.Id).OrderBy(m => m.UpdateTime).First();

                Mapper.Initialize(cfg => cfg.CreateMap<DtoAppCase, AppCase>());
                Mapper.Map(dtoAppCase, appCase);

                Commit();
                return appCase.Id;
            }

        }

        /// <summary>
        /// 删除指定应用案例
        /// </summary>
        /// <param name="appCaseId">应用案例Id</param>
        /// <returns></returns>
        public void DeleteAppCase(Guid appCaseId)
        {
            var AppCase = appCaseReps.FindBy(t => t.Id == appCaseId).OrderBy(m => m.UpdateTime).First();

            appCaseReps.Remove(AppCase);
            Commit();
        }
        #endregion

        #region 使用说明信息操作
        /// <summary>
        /// 查询所有使用说明信息
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoInstruction> GetAllInstruction()
        {
            var instruction = instructionReps.FindAll().OrderBy(m => m.UpdateTime);

            var dtoInstruction = instruction.ConvertTo<Instruction, DtoInstruction>();
            return dtoInstruction;
        }

        /// <summary>
        /// 根据Id查询指定使用说明信息
        /// </summary>
        /// <param name="InstructionId">使用说明Id</param>
        /// <returns></returns>
        public DtoInstruction GetInstructionById(Guid InstructionId)
        {
            var instruction = instructionReps.FindBy(m => m.Id == InstructionId).OrderBy(m => m.UpdateTime);

            var dtoInstruction = instruction.ConvertTo<Instruction, DtoInstruction>().First();
            return dtoInstruction;
        }
        /// <summary>
        /// 使用说明模糊查询
        /// </summary>
        /// <param name="dtoInstruction"></param>
        /// <returns></returns>
        public IQueryable<Instruction> FindInstructionByLike(DtoInstruction dtoInstruction)
        {
            var dtoInstructions = instructionReps.FindAll().Where(m => m.Author.Contains(dtoInstruction.Author) || m.AppIndustry.Contains(dtoInstruction.AppIndustry) || m.Subject.Contains(dtoInstruction.Subject));

            return dtoInstructions;
        }

        /// <summary>
        /// 根据主题查询指定使用说明列表
        /// </summary>
        /// <param name="Subject">使用说明</param>
        /// <returns></returns>
        public List<DtoInstruction> GetInstructionInfoBySubject(string Subject)
        {
            var instruction = instructionReps.FindAll().Where(m => m.Subject == Subject).OrderBy(m => m.UpdateTime);

            var instructionList = instruction.ConvertTo<Instruction, DtoInstruction>().ToList();
            return instructionList;
        }

        /// <summary>
        /// 根据作者查询指定使用说明列表
        /// </summary>
        /// <param name="Author">作者</param>
        /// <returns></returns>
        public List<DtoInstruction> GetInstructionInfoByAuthor(string Author)
        {
            var instruction = instructionReps.FindAll().Where(m => m.Author == Author).OrderBy(m => m.UpdateTime);

            var instructionList = instruction.ConvertTo<Instruction, DtoInstruction>().ToList();
            return instructionList;
        }

        /// <summary>
        /// 根据应用行业查询指定使用说明列表
        /// </summary>
        /// <param name="AppIndustry">应用行业</param>
        /// <returns></returns>
        public List<DtoInstruction> GetInstructionInfoByAppIndustry(string AppIndustry)
        {
            var instruction = instructionReps.FindAll().Where(m => m.AppIndustry == AppIndustry).OrderBy(m => m.UpdateTime);

            var instructionList = instruction.ConvertTo<Instruction, DtoInstruction>().ToList();
            return instructionList;
        }

        /// <summary>
        /// 根据用途查询指定使用说明列表
        /// </summary>
        /// <param name="Usage">用途</param>
        /// <returns></returns>
        public List<DtoInstruction> GetInstructionInfoByAppUsage(string Usage)
        {
            var instruction = instructionReps.FindAll().Where(m => m.Usage == Usage).OrderBy(m => m.UpdateTime);

            var instructionList = instruction.ConvertTo<Instruction, DtoInstruction>().ToList();
            return instructionList;
        }

        ///<summary>
        /// 保存使用说明信息
        ///</summary>
        ///<param name ="dtoInstruction">使用说明DTO</param>
        ///<param name ="InstructionId">使用说明Id</param>
        ///<returns></returns>
        public Guid SaveInstruction(DtoInstruction dtoInstruction)
        {

            if (dtoInstruction.Id == new Guid())
            {
                //id为空, 对使用说明信息进行添加操作
                Instruction instruction = new Instruction();
                Mapper.Initialize(cfg => cfg.CreateMap<DtoInstruction, Instruction>());
                var instructionData = Mapper.Map<Instruction>(instruction);

                //给实体类赋值
                instructionData.Product = productReps.FindBy(m => m.Id == dtoInstruction.Id).OrderBy(m => m.UpdateTime).First();

                instructionReps.Add(instructionData);

                Commit();
                return instructionData.Id;
            }
            else
            {
                //id不为空,对使用说明信息进行修改操作
                var instruction = instructionReps.FindBy(i => i.Id == dtoInstruction.Id).OrderBy(m => m.UpdateTime).First();

                Mapper.Initialize(cfg => cfg.CreateMap<DtoInstruction, Instruction>());
                Mapper.Map(dtoInstruction, instruction);

                Commit();
                return instruction.Id;
            }

        }

        /// <summary>
        /// 删除指定应用案例
        /// </summary>
        /// <param name="InstructionId">使用说明Id</param>
        /// <returns></returns>
        public void DeleteInstruction(Guid InstructionId)
        {
            var Instruction = instructionReps.FindBy(m => m.Id == InstructionId).OrderBy(m => m.UpdateTime).First();

            instructionReps.Remove(Instruction);
            Commit();
        }
        #endregion
    }
}

