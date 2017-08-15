using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hogon.Framework.Core.Common;
using Hogon.Framework.Utilities.SmartList;
using Hogon.Framework.Web.Controller;
using Hogon.Store.Models.Dto.GoodsMan;
using Hogon.Store.Models.Entities.GoodsMan;
using Hogon.Store.Services.ApplicationServices.GoodsManContext;
using Hogon.Store.UserInterface.Admin.Areas.GoodsMan.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Hogon.Store.UserInterface.Admin.Areas.GoodsMan.Controllers
{
    public class ProductTypeController : SmartListController<ProductTypeViewModel>
    {
        ProductTypeApplicationService productTypeSvc = new ProductTypeApplicationService();
        SpecTypeApplicationService specTypeSvc = new SpecTypeApplicationService();

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

        public ProductTypeController(BaseViewRender<ProductTypeViewModel> listRender) : base(listRender)
        {

        }

        // GET: GoodsMan/ProductTypeCategory        
        public ActionResult Index()
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

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult GetSpecTypeForDrop()
        {
            //var specType=productTypeSvc
            return Json("");
        }

        /// <summary>
        /// 根据ProductTypeName查询是否重复
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        public ActionResult GetRepetitionForProductTypeName(ProductType productType)
        {
            int num = productTypeSvc.GetProductTypeByProductTypeName(productType);

            return Json(num);
        }

        /// <summary>
        /// 保存产品类型
        /// </summary>
        /// <param name="dtoTypeMan">产品类型</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(DtoProductType dtoproductType, Guid categoryId)
        {
            var productType = productTypeSvc.SaveProductType(dtoproductType, categoryId);
            if (productType == new Guid())
            {
                throw new UserFriendlyException("数据已存在");
            }
            return Json(productType);
        }

        /// <summary>
        /// 根据Id查询数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult GetProductTypeById(Guid Id)
        {
            var productTypes= productTypeSvc.FindProductTypeById(Id);

            Mapper.Initialize(cfg => cfg.CreateMap<DtoProductType, ProductTypeViewModel>());
            var viewModels = Mapper.Map<ProductTypeViewModel>(productTypes);
            
            return Json(viewModels);
        }

        /// <summary>
        /// 删除产品类型
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Remove(Guid Id)
        {
            productTypeSvc.DeleteProductType(Id);

            return Json("");
        }

        /// <summary>
        /// 查询所有产品类型
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllProductType()
        {

          var productTypeData = productTypeSvc.FindAllProductType();

            return Json(productTypeData);
        }

        /// <summary>
        /// 根据产品类型Id查询规格类型
        /// </summary>
        public ActionResult GetSpecTypeByProTypeId(Guid productId)
        {
            var dtoSpecTypes = productTypeSvc.GetSpecTypeByProTypeId(productId);

            Mapper.Initialize(cfg => cfg.CreateMap<DtoSpecType, SpecTypeViewModel>());
            var viewModels = dtoSpecTypes.ProjectTo<SpecTypeViewModel>(dtoSpecTypes);

            return Json(viewModels);
        }

        /// <summary>
        /// 获取所有产品
        /// </summary>
        /// <returns></returns>
        protected override IQueryable<ProductTypeViewModel> GetAllModels()
        {
            var dtoProductType = productTypeSvc.FindAllProductType().OrderByDescending(t => t.UpdateTime);

            Mapper.Initialize(cfg => cfg.CreateMap<DtoProductType, ProductTypeViewModel>());
            
            var viewModels = dtoProductType.ProjectTo<ProductTypeViewModel>(dtoProductType);

            return viewModels;
        }
    }
}