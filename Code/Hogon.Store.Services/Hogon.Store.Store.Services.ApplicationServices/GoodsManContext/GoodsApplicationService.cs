using AutoMapper;
using Hogon.Framework.Core.Common;
using Hogon.Framework.Core.UnitOfWork;
using Hogon.Store.Models.Dto.GoodsMan;
using Hogon.Store.Models.Entities.GoodsMan;
using Hogon.Store.Repositories.GoodsMan;
using System;
using System.Linq;
using System.Collections.Generic;
using Hogon.Store.Repositories.Common;
using Hogon.Framework.Core.Owin;
using AutoMapper.QueryableExtensions;
using Hogon.Store.Models.Dto.Common;
using Hogon.Store.Models.Entities.Common;

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

        FileUploadRepository fileUploadReps = new FileUploadRepository();

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
            var brands = brandReps.FindAll();
            //var dtoBrands = brands.ConvertTo<Brand, DtoBrand>();
            var dtoBrands = brands.Select(m => new DtoBrand()
            {
                BrandAlias = m.BrandAlias,
                BrandLogo = m.BrandLogo,
                BrandName = m.BrandName,
                City = m.City,
                Country = m.Country,
                CreatePerson = m.CreatePerson,
                CreateTime = m.CreateTime,
                Id = m.Id,
                Nation = m.Nation,
                UpdatePerson = m.UpdatePerson,
                UpdateTime = m.UpdateTime,
                Url = m.Url,
            });
            return dtoBrands;
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
        /// 根据产品类型查找规格类型
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <returns></returns>
        public IQueryable<DtoSpecType> FindSpecTypeByProductTypeId(Guid productTypeId)
        {
            if (productTypeId != Guid.Empty)
            {
                var specType = specTypeReps.FindAll().Where(m => m.ProductType.Id == productTypeId);

                var dtoSpecType = specType.Select(m => new DtoSpecType()
                {
                    Id = m.Id,
                    CreatePerson = m.CreatePerson,
                    CreateTime = m.CreateTime,
                    DisplayMode = m.DisplayMode,
                    DisplayName = m.DisplayName,
                    ProductTypeId = productTypeId,
                    SpecName = m.SpecName,
                    SpecRemark = m.SpecRemark,
                    SpecSecondName = m.SpecSecondName,
                    UpdatePerson = m.UpdatePerson,
                    UpdateTime = m.UpdateTime,
                });
                return dtoSpecType;
            }
            return null;
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
                BrandName = m.Brand.BrandName,
                ProductTypeName = m.ProductType.TypeName,
                SpecParameterTemplate = m.SpecParameterTemplate,
                DiplaySpecType = m.DiplaySpecType,
            }).First();

            return dtoProduct;
        }

        /// <summary>
        /// 产品信息添加规格类型，规格参数Json
        /// </summary>
        /// <param name="dtoProductGoodsS"></param>
        /// <param name="dtoProduct"></param>
        public void ProductAddSepcTypePara(ICollection<DtoProductGoods> dtoProductGoodsS, DtoProduct dtoProduct)
        {
            if (dtoProduct.Id != Guid.Empty)
            {
                var product = productReps.FindBy(m => m.Id == dtoProduct.Id).First();
                foreach (var item in dtoProductGoodsS)
                {
                    var para = item.SpecParameterS;
                    var num = para.IndexOf(':');
                    var parameter = para.Substring((num + 1), num);
                    var specType = para.Substring(0, num);
                    if (product.DiplaySpecType != null)
                    {
                        foreach (var productSpecType in product.DiplaySpecType.Split(';'))
                        {
                            if (productSpecType == specType)
                            { continue; }
                            product.DiplaySpecType += specType;
                        }
                    }
                    else
                    {
                        product.DiplaySpecType += specType;
                    }
                    if (product.SpecParameterTemplate != null)
                    {
                        foreach (var productPara in product.SpecParameterTemplate.Split(';'))
                        {
                            if (productPara == parameter)
                            {
                                continue;
                            }
                            product.SpecParameterTemplate += parameter;
                        }
                    }
                    else
                    {
                        product.SpecParameterTemplate += parameter;
                    }
                }
                Commit();
            }
        }

        /// <summary>
        /// 根据产品名称查询指定产品列表
        /// </summary>
        /// <param name="ProductName">产品名称</param>
        /// <returns></returns>
        public int FindProductByRepetition(DtoProduct dtoProduct)
        {
            int count = 0;
            if (dtoProduct.Id != Guid.Empty)
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

        /// <summary>
        /// 产品商品关联服务商品
        /// </summary>
        /// <param name="id"></param>
        /// <param name="serviceGoodsIds"></param>
        /// <returns></returns>
        public bool EditServiceGoodsInProductGoods(Guid id, ICollection<Guid> serviceGoodsIds)
        {
            if (id != Guid.Empty)
            {
                var product = productReps.FindBy(m => m.Id == id).First();
                foreach (var serviceGoodsId in serviceGoodsIds)
                {
                    var serviceGoods = serviceGoodsReps.FindBy(m => m.Id == serviceGoodsId).First();

                    serviceGoods.Product = product;

                    Mapper.Initialize(cfg => cfg.CreateMap<ServiceGoods, ServiceGoods>());
                    Mapper.Map<ServiceGoods>(serviceGoods);
                }
                Commit();
                return true;
            }
            else
            {
                return false;
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
        /// 删除产品中的服务商品
        /// </summary>
        /// <param name="ServiceGoodsId"></param>
        /// <param name="productId"></param>
        public void RemoveServiceGoodsInProduct(Guid ServiceGoodsId, Guid productId)
        {
            var product = productReps.Include("ServiceGoods").Where(m => m.Id == productId).First();
            var serviceGoods = product.ServiceGoods.Where(m => m.Id == ServiceGoodsId).First();

            product.ServiceGoods.Remove(serviceGoods);

            Commit();
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
                if (id != Guid.Empty)
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
        /// 根据产品Id查询服务商品
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IQueryable<DtoServiceGoods> FindServiceGoodsByProductId(Guid Id)
        {
            var product = productReps.FindBy(m => m.Id == Id).First();
            var serviceGoods = serviceGoodsReps.FindAll().Where(m => m.Product.Id == product.Id);
            var dtoServiceGoods = serviceGoods.Select(s => new DtoServiceGoods()
            {
                Id = s.Id,
                CreatePerson = s.CreatePerson,
                CreateTime = s.CreateTime,
                FIleUploadId = s.FileUpload.Id,
                FileUploadName = s.FileUpload.FileName,
                GoodsAlias = s.GoodsAlias,
                GoodsCode = s.GoodsCode,
                GoodsDesription = s.GoodsDesription,
                GoodsName = s.GoodsName,
                IsAvailable = s.IsAvailable,
                SalePrice = s.SalePrice,
                UpdatePerson = s.UpdatePerson,
                UpdateTime = s.UpdateTime,
            });
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
                serviceGoods.FileUpload = fileUploadReps.FindBy(m => m.Id == dtoServiceGoods.FIleUploadId).First();
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
        public void RemoveServiceGoods(Guid Id)
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
        /// 根据产品查找商品数据
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IQueryable<DtoProductGoods> FindProductGoodsByProduct(Guid productId)
        {
            if (productId != Guid.Empty)
            {
                var productGoods = productGoodsReps.FindBy(m => m.Product.Id == productId);

                var dtoProductGooods = productGoods.Select(m => new DtoProductGoods()
                {
                    CreatePerson = m.CreatePerson,
                    CreateTime = m.CreateTime,
                    DisplaySpecParameterTemplateName = m.DisplaySpecParameterTemplateName,
                    GoodsAlias = m.GoodsAlias,
                    GoodsCode = m.GoodsCode,
                    GoodsDesription = m.GoodsDesription,
                    GoodsName = m.GoodsName,
                    Id = m.Id,
                    IsAvailable = m.IsAvailable,
                    IsDefaultGoods = m.IsDefaultGoods,
                    SalePrice = m.SalePrice,
                    SearchKeywords = m.SearchKeywords,
                    UpdatePerson = m.UpdatePerson,
                    UpdateTime = m.UpdateTime,
                    Weight = m.Weight,
                    ProductName = m.Product.ProductType.TypeName,
                    SpecParameterS = m.DisplaySpecParameterTemplateName,
                });

                return dtoProductGooods;
            }
            return null;
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

            dtoProductGoods.GoodsName = listProductGoods[0];//商品名
            var typename = listProductGoods[1];//产品类型名
            var product = productReps.FindBy(m => m.ProductType.TypeName == typename).First();//产品

            Mapper.Initialize(cfg => cfg.CreateMap<Product, DtoProduct>());
            var productData= Mapper.Map<DtoProduct>(product);

            dtoProductGoods.Product = productData;

            List <string> listSpecTypeParameter = new List<string>();
            foreach (var item in listProductGoods[2].Split(';'))
            {
                if (item != "")
                {
                    listSpecTypeParameter.Add(item);//规格类型：规格参数模板
                }
            }
            List<string> listParameter = new List<string>();
            foreach (var item in listSpecTypeParameter)
            {
                var startIndex = item.IndexOf(":") + 1;
                var parameter = item.Substring(startIndex, (item.Length - startIndex));//规格参数模板
                if (parameter != "")
                {
                    var dtoSpecParameterTemplate = specParameterTemplateReps.FindAll()
                        .Where(m => m.ParameterName == parameter).First();//规格参数模板

                    Mapper.Initialize(cfg => cfg.CreateMap<SpecParameterTemplate, DtoSpecParameterTemplate>());
                    var specParams = Mapper.Map<DtoSpecParameterTemplate>(dtoSpecParameterTemplate);

                    dtoProductGoods.DtoSpecParameterTemplateS.Add(specParams);
                }
            }
            dtoProductGoods.IsAvailable = bool.Parse(listProductGoods[3]);//是否上架
            dtoProductGoods.IsDefaultGoods = bool.Parse(listProductGoods[4]);//是否为默认商品
            dtoProductGoods.GoodsCode = listProductGoods[5];//商品编码
            dtoProductGoods.GoodsAlias = listProductGoods[6];//商品别名
            dtoProductGoods.SalePrice = decimal.Parse(listProductGoods[7]);//销售价
            dtoProductGoods.Weight = decimal.Parse(listProductGoods[8]);//计量单位
            dtoProductGoods.DisplaySpecParameterTemplateName = listProductGoods[2];//该商品的规格类型：规格参数
            return dtoProductGoods;
        }

        ///<summary>
        /// 保存商品信息
        ///</summary>
        ///<param name ="dtoProductGooods">商品DTO</param>
        ///<returns></returns>
        public Guid SaveProductGoods(DtoProductGoods dtoProductGooods)
        {
            if (dtoProductGooods.Id == Guid.Empty)
            {
                //id为空, 对商品信息进行添加操作
                Mapper.Initialize(cfg => cfg.CreateMap<DtoProductGoods, ProductGoods>());
                var productGoods = Mapper.Map<ProductGoods>(dtoProductGooods);
                productGoods.DisplaySpecParameterTemplateName = dtoProductGooods.DisplaySpecParameterTemplateName;

                productGoodsReps.Add(productGoods);
                Commit();

                return productGoods.Id;
            }
            else
            {
                //id不为空,对商品信息进行修改工作
                var productGoods = productGoodsReps.FindBy(m => m.Id == dtoProductGooods.Id)
                    .OrderBy(m => m.UpdateTime).First();

                Mapper.Initialize(cfg => cfg.CreateMap<DtoProductGoods, ProductGoods>());
                Mapper.Map(dtoProductGooods, productGoods);
                productGoods.DisplaySpecParameterTemplateName = dtoProductGooods.DisplaySpecParameterTemplateName;

                Commit();
                return productGoods.Id;
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
        public DtoAppCase FindAppCaseById(Guid AppCaseId)
        {
            var dtoAppCase = appCaseReps.FindBy(m => m.Id == AppCaseId).Select(m => new DtoAppCase()
            {
                Usage = m.Usage,
                UpdateTime = m.UpdateTime,
                UpdatePerson = m.UpdatePerson,
                Subject = m.Subject,
                AppIndustry = m.AppIndustry,
                Author = m.Author,
                CreatePerson = m.CreatePerson,
                CreateTime = m.CreateTime,
                FileUpLoadId = m.FileUpload.Id,
                FileUpLoadName = m.FileUpload.FileName,
                ProductId = m.Product.Id,
                Id = m.Id,
            }).First();

            return dtoAppCase;
        }

        /// <summary>
        /// 根据产品Id查询应用案例
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IQueryable<AppCase> FindAppCaseByProductId(Guid productId)
        {
            var product = productReps.FindBy(m => m.Id == productId).First();
            var appcases = appCaseReps.FindBy(m => m.Product.Id == product.Id);
            return appcases;
        }

        /// <summary>
        /// 应用案例模糊查询
        /// </summary>
        /// <param name="dtoAppCase"></param>
        /// <returns></returns>
        public IQueryable<AppCase> FindAppCaseByLike(DtoAppCase dtoAppCase)
        {

            var appCases = appCaseReps.FindAll().Where(m => m.Author.Contains(dtoAppCase.Author)
            || m.AppIndustry.Contains(dtoAppCase.AppIndustry) || m.Subject.Contains(dtoAppCase.Subject));

            return appCases;
        }

        ///<summary>
        /// 保存应用案例信息
        ///</summary>
        ///<param name ="dtoAppCase">应用案例DTO</param>
        ///<param name ="AppCaseId">应用案例Id</param>
        ///<returns></returns>
        public AppCase SaveAppCase(DtoAppCase dtoAppCase)
        {
            if (dtoAppCase.Id == Guid.Empty)
            {
                //id为空, 对应用案例信息进行添加操作
                Mapper.Initialize(cfg => cfg.CreateMap<DtoAppCase, AppCase>());
                var appCase = Mapper.Map<AppCase>(dtoAppCase);

                appCase.FileUpload = fileUploadReps.FindBy(m => m.Id == dtoAppCase.FileUpLoadId).First();
                appCase.Author = UserState.Current.UserName;
                appCase.Product = productReps.FindBy(m => m.Id == dtoAppCase.ProductId).First();

                appCaseReps.Add(appCase);
                Commit();
                return appCase;
            }
            else
            {
                //id不为空,对应用案例信息进行修改工作
                var appCase = appCaseReps.FindBy(i => i.Id == dtoAppCase.Id).OrderBy(m => m.UpdateTime).First();

                Mapper.Initialize(cfg => cfg.CreateMap<DtoAppCase, AppCase>());
                Mapper.Map(dtoAppCase, appCase);

                appCase.Author = UserState.Current.UserName;
                appCase.FileUpload = fileUploadReps.FindBy(m => m.Id == dtoAppCase.FileUpLoadId).First();

                Commit();
                return appCase;
            }

        }

        /// <summary>
        /// 删除指定应用案例
        /// </summary>
        /// <param name="appCaseId">应用案例Id</param>
        /// <returns></returns>
        public void RemoveAppCase(Guid appCaseId)
        {
            var AppCase = appCaseReps.FindBy(t => t.Id == appCaseId).First();

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
        public DtoInstruction FindInstructionById(Guid InstructionId)
        {

            Mapper.Initialize(cfg => cfg.CreateMap<FileUpload, DtoFileUpload>());

            var dtoInstruction = instructionReps.FindAll().Select(m => new DtoInstruction()
            {
                AppIndustry = m.AppIndustry,
                Author = m.Author,
                CreatePerson = m.CreatePerson,
                CreateTime = m.CreateTime,
                FileUpload = Mapper.Map<DtoFileUpload>(m.FileUpload),
                FileUpLoadId = m.FileUpload.Id,
                Id = m.Id,
                ProductId = m.Product.Id,
                Subject = m.Subject,
                UpdatePerson = m.UpdatePerson,
                UpdateTime = m.UpdateTime,
                Usage = m.Usage,
                FileUploadName = m.FileUpload.FileName,
            }).First() ;

            return dtoInstruction;
        }

        /// <summary>
        /// 根据产品Id查询使用说明
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IQueryable<Instruction> FindInstructionByProductId(Guid productId)
        {
            var product = productReps.FindBy(m => m.Id == productId).First();
            var instructions = instructionReps.FindBy(m => m.Product.Id == product.Id);
            return instructions;
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
        public Instruction SaveInstruction(DtoInstruction dtoInstruction)
        {

            if (dtoInstruction.Id == Guid.Empty)
            {
                //id为空, 对使用说明信息进行添加操作
                Mapper.Initialize(cfg => cfg.CreateMap<DtoInstruction, Instruction>());
                var instruction = Mapper.Map<Instruction>(dtoInstruction);

                instruction.Author = UserState.Current.UserName;
                instruction.FileUpload = fileUploadReps.FindBy(m => m.Id == dtoInstruction.FileUpLoadId).First();
                instruction.Product = productReps.FindBy(m => m.Id == dtoInstruction.ProductId).First();

                instructionReps.Add(instruction);

                Commit();
                return instruction;
            }
            else
            {
                //id不为空,对使用说明信息进行修改操作
                var instruction = instructionReps.FindBy(i => i.Id == dtoInstruction.Id).OrderBy(m => m.UpdateTime).First();

                Mapper.Initialize(cfg => cfg.CreateMap<DtoInstruction, Instruction>());
                Mapper.Map(dtoInstruction, instruction);

                instruction.Author = UserState.Current.UserName;
                instruction.FileUpload = fileUploadReps.FindBy(m => m.Id == dtoInstruction.FileUpLoadId).First();
                instruction.Product = productReps.FindBy(m => m.Id == dtoInstruction.ProductId).First();

                Commit();
                return instruction;
            }

        }

        /// <summary>
        /// 删除指定应用案例
        /// </summary>
        /// <param name="InstructionId">使用说明Id</param>
        /// <returns></returns>
        public void RemoveInstruction(Guid InstructionId)
        {
            var Instruction = instructionReps.FindBy(m => m.Id == InstructionId).First();

            instructionReps.Remove(Instruction);
            Commit();
        }
        #endregion
    }
}


