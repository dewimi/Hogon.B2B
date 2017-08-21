using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hogon.Framework.Utilities.SmartList;
using Hogon.Framework.Web.Controller;
using Hogon.Store.Models.Dto.GoodsMan;
using Hogon.Store.Services.ApplicationServices.GoodsManContext;
using Hogon.Store.UserInterface.Admin.Areas.GoodsMan.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Hogon.Store.UserInterface.Admin.Areas.GoodsMan.Controllers
{
    public class GoodsTypeController : SmartListController<GoodsTypeViewModel>
    {
        GoodsTypeApplicationService goodsTypeSvc = new GoodsTypeApplicationService();
        ProductTypeApplicationService productTypeSvc = new ProductTypeApplicationService();

        public GoodsTypeController(BaseViewRender<GoodsTypeViewModel> listRender) : base(listRender)
        {
            
        }

        // GET: GoodsMan/GoodsType
        public ActionResult Index()
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
        /// 查询上级分类
        /// </summary>
        /// <returns></returns>
		[HttpPost]
        public ActionResult FindAllGoodsTypeNameForDrop()
        {
            var dtoGoodsType = goodsTypeSvc.FindGoodsTypeOutThreeNode();
            return Json(dtoGoodsType);
        }

		/// <summary>
		/// 查询产品类型
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult FindAllProductTypeForDrop()
        {
            var dtoProductType = productTypeSvc.FindAllProductType();
			return Json(dtoProductType);
        }

        /// <summary>
        /// 根据Id查询商品分类
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FindGoodsTypeById(Guid Id)
        {
            var GoodsType= goodsTypeSvc.FindGoodsTypeById(Id);
            return Json(GoodsType);
        }

        /// <summary>
        /// 编辑页
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            return View();
        }

        /// <summary>
        /// 查询Name是否重复
        /// </summary>
        /// <param name="dtoGoodsType"></param>
        /// <returns></returns>
        public ActionResult FindGoodsTypeByName(DtoGoodsType dtoGoodsType)
        {
            var GoodsTypeCount = goodsTypeSvc.FindGoodsTypeByName(dtoGoodsType);
            return Json(GoodsTypeCount);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="dtoGoodsType"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(DtoGoodsType dtoGoodsType)
        {
            var Id =  goodsTypeSvc.SaveGoodsType(dtoGoodsType);
            return Json(Id);
        }

        /// <summary>
        /// 删除商品分类
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Remove(Guid Id)
        {
            goodsTypeSvc.RemoveGoodsType(Id);
            return View("Index");
        }

        /// <summary>
        /// 获取Tree表格数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FindGoodTypeInTreeTable()
         {
            var goodsType = goodsTypeSvc.FindAllGoodsType().ToList();
            var treeTableGoodsType = goodsTypeSvc.FindGoodsTypeAndChildren(goodsType.AsQueryable(), goodsType.AsQueryable());
            return Json(treeTableGoodsType); 
        }

        /// <summary>
        /// Tree查询
        /// </summary>
        /// <returns></returns>
        public ActionResult FindAllGoodsType()
        {
            var goodsType = goodsTypeSvc.FindAllGoodsType().ToList();
            var dtoGoodsType = goodsTypeSvc.CreateNodeList(goodsType.AsQueryable(), goodsType.AsQueryable(), goodsType.AsQueryable());
            return Json(dtoGoodsType);
        }
        
        /// <summary>
        /// 获取所有商品分类
        /// </summary>
        /// <returns></returns>
        protected override IQueryable<GoodsTypeViewModel> GetAllModels()
        {
            var dtoGoodsType = goodsTypeSvc.FindAllGoodsType().OrderByDescending(m => m.CreateTime);

            Mapper.Initialize(cfg => cfg.CreateMap<DtoGoodsType, GoodsTypeViewModel>());

            var viewModels = dtoGoodsType.ProjectTo<GoodsTypeViewModel>(dtoGoodsType);

            return viewModels;
        }
    }
}