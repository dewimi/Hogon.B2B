using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hogon.Framework.Utilities.SmartList;
using Hogon.Framework.Web.Controller;
using Hogon.Store.Models.Dto.GoodsMan;
using Hogon.Store.Models.Entities.GoodsMan;
using Hogon.Store.Services.ApplicationServices.GoodsManContext;
using Hogon.Store.UserInterface.Admin.Areas.GoodsMan.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using Hogon.Framework.Core.Owin;
using Hogon.Framework.Core.Common;

namespace Hogon.Store.UserInterface.Admin.Areas.GoodsMan.Controllers
{
    public class ProductTypeCategoryController : SmartListController<ProductTypeCategoryViewModel>
    {
        ProductTypeCategoryApplicationService productTypeCategorySvc = new ProductTypeCategoryApplicationService();

        public ProductTypeCategoryController(BaseViewRender<ProductTypeCategoryViewModel> listRender) : base(listRender)
        {

        }

        // GET: GoodsMan/ProductTypeCategory        
        public ActionResult index()
        {
            return View();
        }
        /// <summary>
        /// 修改页
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 修改页
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            return View();
        }

        /// <summary>
        /// 详情页
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail()
        {
            return View();
        }

        /// <summary>
        /// 所属分类二级菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProductTypeCategoryNameForDrop()
        {
            var productTypeCategory = productTypeCategorySvc.GetAllProductTypeCategory();
            return Json(productTypeCategory);
        }

        /// <summary>
        /// 根据ProductTypeCategoryName查询是否重复
        /// </summary>
        /// <param name="productTypeCategory"></param>
        /// <returns></returns>
        public ActionResult GetRepetitionForProductTypeCategoryName(ProductTypeCategory productTypeCategory)
        {
            int num = productTypeCategorySvc.GetProductTypeCategoryByProductTypeCategoryName(productTypeCategory);

            return Json(num);
        }

        /// <summary>
        /// 保存产品分类
        /// </summary>
        /// <param name="dtoproductTypeCategory"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(DtoProductCategory dtoproductTypeCategory)
        {
            dtoproductTypeCategory.CreatePerson = UserState.Current.UserName;
            dtoproductTypeCategory.UpdatePerson = UserState.Current.UserName;
            var productTypeCategory = productTypeCategorySvc.SaveProductTypeCategory(dtoproductTypeCategory);
            return Json(productTypeCategory);
        }

        /// <summary>
        /// 根据Id查询数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult GetProductTypeCategoryById(Guid Id)
        {
            var productTypeCategorys = productTypeCategorySvc.GetProductTypeCategoryById(Id);
            return Json(productTypeCategorys);
        }

        /// <summary>
        /// 删除产品分类
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Remove(Guid Id)
        {
            bool prompt = productTypeCategorySvc.RemoveProductTypeCategory(Id);

            if (!prompt)
            {
                throw new UserFriendlyException("请先删除该分类下的子分类");
            }

            return Json(new { message = "" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllCategoryNodes()
        {
            var categoryNodes = productTypeCategorySvc.GetAllCategoryNodes();

            return Json(categoryNodes);
        }

        /// <summary>
        /// 获取所有产品分类
        /// </summary>
        /// <returns></returns>
        protected override IQueryable<ProductTypeCategoryViewModel> GetAllModels()
        {
            var dtoProductTypeCategory = productTypeCategorySvc.GetAllProductTypeCategory().OrderByDescending(t => t.UpdateTime);

            Mapper.Initialize(cfg => cfg.CreateMap<DtoProductCategory, ProductTypeCategoryViewModel>());

            var viewModels = dtoProductTypeCategory.ProjectTo<ProductTypeCategoryViewModel>(dtoProductTypeCategory);

            return viewModels;

        }
    }
}
