using Hogon.Framework.Utilities.SmartList;
using Hogon.Framework.Web.Controller;
using Hogon.Store.Services.ApplicationServices.GoodsManContext;
using Hogon.Store.UserInterface.Admin.Areas.GoodsMan.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Hogon.Store.Models.Dto.GoodsMan;
using AutoMapper.QueryableExtensions;

namespace Hogon.Store.UserInterface.Admin.Areas.GoodsMan.Controllers
{
    public class GoodsController : SmartListController<ProductGoodsViewModel>
    {
        GoodsApplicationService GoodsSvc = new GoodsApplicationService();

        public GoodsController(BaseViewRender<ProductGoodsViewModel> listRender) : base(listRender)
        {
            
        }

        /// <summary>
        /// GET: GoodsMan/Goods/Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GET: GoodsMan/Goods/Add
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// GET: GoodsMan/Goods/Edit
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            return View();
        }

        /// <summary>
        /// 保存商品信息
        /// </summary>
        /// <param name="ProductId">产品Id</param>
        /// <returns></returns>
        public ActionResult Save(DtoProduct ProductId)
        {
            var Id = GoodsSvc.SaveProduct(ProductId);
            return Json(Id);
        }

        /// <summary>
        /// 删除商品信息
        /// </summary>
        /// <param name="ProductId">产品Id</param>
        /// <returns></returns>
        public ActionResult Remove(Guid ProductId)
        {
            GoodsSvc.RemoveProduct(ProductId);

            return Json(" ");
        }

        /// <summary>
        /// 获取所有商品信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllProduct()
        {
            var Product = GoodsSvc.GetAllProductInfo().ToList();
            return Json(Product);
        }

        /// <summary>
        /// 获取所有产品分类
        /// </summary>
        /// <returns></returns>
        protected override IQueryable<ProductGoodsViewModel> GetAllModels()
        {
            var dtoProduct = GoodsSvc.GetAllProductInfo().OrderByDescending(t => t.UpdateTime);

            Mapper.Initialize(cfg => cfg.CreateMap<DtoProduct, ProductGoodsViewModel>());

            var viewModels = dtoProduct.ProjectTo<ProductGoodsViewModel>(dtoProduct);

            return viewModels;
        }
    }
}