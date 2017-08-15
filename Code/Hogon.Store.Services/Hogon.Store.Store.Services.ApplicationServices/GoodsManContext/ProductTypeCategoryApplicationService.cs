using AutoMapper;
using Hogon.Framework.Core.Common;
using Hogon.Framework.Core.UnitOfWork;
using Hogon.Store.Models.Dto.Common;
using Hogon.Store.Models.Dto.GoodsMan;
using Hogon.Store.Models.Entities.GoodsMan;
using Hogon.Store.Repositories.GoodsMan;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hogon.Store.Services.ApplicationServices.GoodsManContext
{
    public class ProductTypeCategoryApplicationService : BaseApplicationService
    {

        ProductTypeCategoryRepository productTypeCategoryReps = new ProductTypeCategoryRepository();

        /// <summary>
        /// 查询所有类型
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoProductCategory> GetAllProductTypeCategory()
        {
            var productTypeCategorys = productTypeCategoryReps.FindAll().OrderBy(t => t.UpdateTime);

            var dtoProductTypeCategorys = productTypeCategorys.Select(m => new DtoProductCategory()
            {
                Id = m.Id,
                Code = m.Code,
                ProductTypeCategoryName = m.ProductTypeCategoryName,
                ParentId = m.ParentId,
                ParentName = m.Parent.ProductTypeCategoryName,
                CreatePerson = m.CreatePerson,
                CreateTime = m.CreateTime,
                UpdatePerson = m.UpdatePerson,
                UpdateTime = m.UpdateTime,
            });

            return dtoProductTypeCategorys;
        }

        /// <summary>
        /// 根据Id查询类型
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DtoProductCategory GetProductTypeCategoryById(Guid Id)
        {
            var productTypeCategorys = productTypeCategoryReps.FindBy(t => t.Id == Id);
            var dtoProductTypeCategorys = productTypeCategorys.Select(m => new DtoProductCategory()
            {
                Id = m.Id,
                Code = m.Code,
                ProductTypeCategoryName = m.ProductTypeCategoryName,
                ParentId = m.ParentId,
                ParentName = m.Parent.ProductTypeCategoryName,
                CreatePerson = m.CreatePerson,
                CreateTime = m.CreateTime,
                UpdatePerson = m.UpdatePerson,
                UpdateTime = m.UpdateTime,
            }).First();

            return dtoProductTypeCategorys;
        }

        /// <summary>
        /// 根据ProductTypeCategoryName查询是否存在
        /// </summary>
        /// <param name="productTypeCategory"></param>
        /// <returns></returns>
        public int GetProductTypeCategoryByProductTypeCategoryName(ProductTypeCategory productTypeCategory)
        {
            var productTypeCategorys = productTypeCategoryReps.FindAll().Where(p => p.ProductTypeCategoryName == productTypeCategory.ProductTypeCategoryName);

            var dtoproductTypeCategory = productTypeCategorys.ConvertTo<ProductTypeCategory, DtoProductCategory>();

            return dtoproductTypeCategory.Count();
        }

        ///<summary>
        ///保存产品类型
        ///</summary>
        ///<param name ="dtoProductTypeCategory"></param>
        ///<returns></returns>
        public Guid SaveProductTypeCategory(DtoProductCategory dtoProductTypeCategory)
        {
            if (dtoProductTypeCategory.Id == new Guid())
            {
                //id为空进行添加
                Mapper.Initialize(cfg => cfg.CreateMap<DtoProductCategory, ProductTypeCategory>());
                var dtoProductTypeCategorys = Mapper.Map<ProductTypeCategory>(dtoProductTypeCategory);
                dtoProductTypeCategorys.Id = Guid.NewGuid();
                dtoProductTypeCategorys.CreateTime = DateTime.Now;
                dtoProductTypeCategorys.UpdateTime = DateTime.Now;
                productTypeCategoryReps.Add(dtoProductTypeCategorys);
                Commit();
                return dtoProductTypeCategorys.Id;
            }
            else
            {
                //id 不为空进行修改
                var productTypeCategory = productTypeCategoryReps.FindBy(t => t.Id == dtoProductTypeCategory.Id).First();
                dtoProductTypeCategory.CreateTime = productTypeCategory.CreateTime;
                dtoProductTypeCategory.UpdateTime = DateTime.Now;
                Mapper.Initialize(cfg => cfg.CreateMap<DtoProductCategory, ProductTypeCategory>());
                Mapper.Map(dtoProductTypeCategory, productTypeCategory);
                Commit();
                return productTypeCategory.Id;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool RemoveProductTypeCategory(Guid Id)
        {
            var productTypeCategory = productTypeCategoryReps.FindBy(t => t.Id == Id).First();

            var dtoProductTypeCategory = productTypeCategoryReps.FindBy(t => t.ParentId == productTypeCategory.Id);
            //当存在子节点时不删除
            if (dtoProductTypeCategory.Count() > 0)
            {
                return false;
            }
            else
            {
                productTypeCategoryReps.Remove(productTypeCategory);
                Commit();
                return true;
            }
        }

        /// <summary>
        /// 遍历所有分类节点
        /// </summary>
        /// <returns></returns>
        public List<DtoTreeNode> GetAllCategoryNodes()
        {
            //查询所有分类
            var allCategorys = productTypeCategoryReps.FindAll().ToList();

            //获取遍历出来的分类节点
            var treeNodeLists = CreateNodeList(allCategorys.AsQueryable());

            return treeNodeLists;

        }

        /// <summary>
        /// 生成菜单集合
        /// </summary>
        /// <param name="allGoodsClasss"></param>
        /// <param name="goodsClasss"></param>
        /// <returns></returns>
        public List<DtoTreeNode> CreateNodeList(IQueryable<ProductTypeCategory> allCategorys)
        {

            List<DtoTreeNode> nodeList = new List<DtoTreeNode>();
            var parentCategorys = allCategorys.Where(m => m.ParentId == null);

            // 遍历一级菜单
            foreach (var parentCategory in parentCategorys)
            {
                var parentNode = new DtoTreeNode
                {
                    Id = parentCategory.Id,
                    ParentId = null,
                    text = parentCategory.ProductTypeCategoryName,
                };

                // 根据一级菜单Id查找二级菜单
                var children = allCategorys.Where(m => m.ParentId == parentCategory.Id);
                var childrenNodes = children.Select(m => new DtoTreeNode
                {
                    Id = m.Id,
                    ParentId = m.ParentId,
                    text = m.ProductTypeCategoryName,
                });
                parentNode.nodes = childrenNodes.ToList();


                nodeList.Add(parentNode);
            }
            return nodeList;
        }
    }
}
