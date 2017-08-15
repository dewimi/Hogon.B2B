using AutoMapper;
using Hogon.Framework.Core.Common;
using Hogon.Framework.Core.UnitOfWork;
using Hogon.Store.Models.Dto.GoodsMan;
using Hogon.Store.Models.Entities.GoodsMan;
using Hogon.Store.Repositories.GoodsMan;
using System;
using System.Linq;
using Hogon.Framework.Core.Owin;
using System.Collections.Generic;
using Hogon.Store.Models.Dto.Common;

namespace Hogon.Store.Services.ApplicationServices.GoodsManContext
{
    public class ProductTypeApplicationService : BaseApplicationService
    {

        ProductTypeRepository productTypeReps = new ProductTypeRepository();
        ProductTypeCategoryRepository proTypeCategoryReps = new ProductTypeCategoryRepository();
        SpecTypeRepository spectypeReps = new SpecTypeRepository();


        /// <summary>
        /// 查询所有产品类型
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoProductType> FindAllProductType()
        {
            var productTypes = productTypeReps.FindAll();

            var dtoproductType = productTypes.Select(m => new DtoProductType()
            {
                Id = m.Id,
                TypeName = m.TypeName,
                ProductTypeCategoryName = m.ProductTypeCategory.ProductTypeCategoryName,
                CategoryId = m.ProductTypeCategory.Id,
                CreateTime = m.CreateTime,
                CreatePerson = m.CreatePerson,
                UpdatePerson = m.UpdatePerson,
                UpdateTime = m.UpdateTime,

            });

            return dtoproductType;
        }

        /// <summary>
        /// 根据Id查询产品类型
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DtoProductType FindProductTypeById(Guid Id)
        {
            //var productType = productTypeReps.FindBy(t => t.Id == Id).First();
            var productType = productTypeReps.FindBy
                (t => t.Id == Id).Select(m => new DtoProductType()
                {
                    Id = m.Id,
                    TypeName = m.TypeName,
                    ProductTypeCategoryName = m.ProductTypeCategory.ProductTypeCategoryName,
                    CategoryId = m.ProductTypeCategory.Id,
                    CreateTime = m.CreateTime,
                    CreatePerson = m.CreatePerson,
                    UpdatePerson = m.UpdatePerson,
                    UpdateTime = m.UpdateTime,

                }).First();
            //Mapper.Initialize(cfg => cfg.CreateMap<ProductType, DtoProductType>());
            //var dtoProductType = Mapper.Map<DtoProductType>(productType);

            return productType;
        }

        /// <summary>
        /// 根据TypeName查询数据是否存在
        /// </summary>
        /// <returns></returns>
        public int GetProductTypeByProductTypeName(ProductType roductType)
        {
            var productTypes = productTypeReps.FindAll().Where(p => p.TypeName == roductType.TypeName);

            var dtoProductTypes = productTypes.ConvertTo<ProductType, DtoProductType>();

            return dtoProductTypes.Count();
        }

        /// <summary>
        /// 查询规格类型赋值给下拉框
        /// </summary>
        /// <returns></returns>
        public SpecType GetSpecTypeForDrop()
        {
            return null;
        }

        ///<summary>
        ///保存产品类型
        ///</summary>
        ///<param name ="dtoProductType"></param>
        ///<returns></returns>
        public Guid SaveProductType(DtoProductType dtoProductType, Guid categoryId)
        {
            if (dtoProductType.Id == new Guid())
            {
                 //判断数据库是否存在
                var proType = productTypeReps.FindBy(m => m.TypeName == dtoProductType.TypeName
                 && m.ProductTypeCategory.Id == categoryId).FirstOrDefault();
                if (proType != null)
                {
                    return new Guid();
                }
                else
                {
                    //id为空进行添加
                    Mapper.Initialize(cfg => cfg.CreateMap<DtoProductType, ProductType>());
                    var productType = Mapper.Map<ProductType>(dtoProductType);
                    productType.ProductTypeCategory = proTypeCategoryReps.FindBy(m => m.Id == categoryId).First();
                    productTypeReps.Add(productType);
                    Commit();
                    return productType.Id;
                } }
            else
            {
                //id 不为空进行修改
                var productType = productTypeReps.FindBy(t => t.Id == dtoProductType.Id).First();

                Mapper.Initialize(cfg => cfg.CreateMap<DtoProductType, ProductType>());
                Mapper.Map(dtoProductType, productType);
                Commit();
                return productType.Id;
            }
        }

        /// <summary>
        /// 删除产品类型
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public void DeleteProductType(Guid Id)
        {
            var productType = productTypeReps.FindBy(t => t.Id == Id).First();
            productTypeReps.Remove(productType);

            Commit();

        }

        /// <summary>
        /// 根据产品类型Id查询规格类型
        /// </summary>
        public IQueryable<DtoSpecType> GetSpecTypeByProTypeId(Guid productId)
        {
            var specTypes = spectypeReps.FindBy(m => m.ProductType.Id == productId);

            var dtospecTypes = specTypes.Select(m => new DtoSpecType()
            {
                Id = m.Id,
                CreateTime = m.CreateTime,
                CreatePerson = m.CreatePerson,
                UpdatePerson = m.UpdatePerson,
                UpdateTime = m.UpdateTime,
                SpecName = m.SpecName,
                SpecCatalog = m.SpecCatalog,
                SpecRemark = m.SpecRemark,
                SpecSecondName = m.SpecSecondName,
                DisplayName = m.DisplayName,
                DisplayMode = m.DisplayMode,
                ProductTypeId = m.ProductType.Id
            });
            //var dtospecTypes = specTypes.ConvertTo<SpecType, DtoSpecType>();

            return dtospecTypes;
        }

        public List<DtoTreeNode> BindingCategory()
        {
            return null;
        }
    }
}
