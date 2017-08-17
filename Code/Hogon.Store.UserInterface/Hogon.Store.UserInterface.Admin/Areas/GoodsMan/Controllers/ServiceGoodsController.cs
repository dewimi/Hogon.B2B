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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hogon.Store.UserInterface.Admin.Areas.GoodsMan.Controllers
{
    public class ServiceGoodsController : SmartListController<ServiceGoodsViewModel>
    {
        GoodsApplicationService serviceGoodsSvc = new GoodsApplicationService();

        GoodsTypeApplicationService goodsTypeSvc = new GoodsTypeApplicationService();

        GoodsApplicationService goodsSvc = new GoodsApplicationService();

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

        public ServiceGoodsController(BaseViewRender<ServiceGoodsViewModel> listRender) : base(listRender)
        {
        }

        // GET: GoodsMan/ServiceGoods
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Detail()
        {
            return View();
        }

        /// <summary>
        /// 商品分类下拉框绑值
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FindGoodsTypeForDrop()
        {
            var goodsType = goodsTypeSvc.FindGoodsTypeThreeNode();
            ViewBag.goodsType = goodsType;
            return Json(goodsType);
        }

        /// <summary>
        /// 通过Id查询服务商品
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FindServiceGoodsById(Guid Id)
        {
            var dtoServiceGoods = goodsSvc.FindServiceGoodsById(Id);
            return Json(dtoServiceGoods);
        }

        /// <summary>
        /// 根据GoodsCode查询是否重复
        /// </summary>
        /// <param name="dtoServiceGoods"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FindServiceGoodsByGoodsCode(DtoServiceGoods dtoServiceGoods)
        {
            var count = goodsSvc.FindServiceGoodsByGoodsCode(dtoServiceGoods);
            return Json(count);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="dtoServiceGoods"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(DtoServiceGoods dtoServiceGoods)
        {
            if (dtoServiceGoods != null)
            {
                var Id = goodsSvc.SaveServiceGoods(dtoServiceGoods);
                return Json(Id);
            }
            throw new UserFriendlyException("请将数据填写完整！");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Remove(Guid Id)
        {
            goodsSvc.RemoveServiceGoods(Id);
            return Json(" ");
        }

        /// <summary>
        /// 获取所有服务商品
        /// </summary>
        /// <returns></returns>
        protected override IQueryable<ServiceGoodsViewModel> GetAllModels()
        {

            var dtoServiceGoods = goodsSvc.FindAllServiceGoods().OrderBy(m => m.CreateTime);

            Mapper.Initialize(cfg => cfg.CreateMap<DtoServiceGoods, ServiceGoodsViewModel>());

            var viewModels = dtoServiceGoods.ProjectTo<ServiceGoodsViewModel>(dtoServiceGoods);

            return viewModels;

        }
    }
}
