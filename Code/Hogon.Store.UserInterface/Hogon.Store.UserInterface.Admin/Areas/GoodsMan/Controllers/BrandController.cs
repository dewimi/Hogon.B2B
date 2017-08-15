using Hogon.Framework.Web.Controller;
using Hogon.Store.UserInterface.Admin.Areas.GoodsMan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hogon.Framework.Utilities.SmartList;
using Hogon.Store.Services.ApplicationServices.GoodsManContext;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hogon.Store.Models.Dto.GoodsMan;

namespace Hogon.Store.UserInterface.Admin.Areas.GoodsMan.Controllers
{
	public class BrandController : SmartListController<BrandViewModel>
	{
		BrandApplicationService brandSvc = new BrandApplicationService();

		GoodsApplicationService goodsSvc = new GoodsApplicationService();

		GoodsTypeApplicationService goodsTypeSvc = new GoodsTypeApplicationService();

		protected override void OnException(ExceptionContext filterContext)
		{
			base.OnException(filterContext);
		}

		public BrandController(BaseViewRender<BrandViewModel> listRender) : base(listRender)
		{
		}

		// GET: GoodsMan/Brand
		public ActionResult Index()
		{
			return View();
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
		/// 详情页
		/// </summary>
		/// <returns></returns>
		public ActionResult Detail()
		{
			return View(); 
		}

		/// <summary>
		/// 查询所有产品
		/// </summary>
		/// <returns></returns>
		public ActionResult FindProductForDrop()
		{
			var products = goodsSvc.GetAllProductInfo();
			return Json(products);
		}

		/// <summary>
		/// 查询所有商品分类
		/// </summary>
		/// <returns></returns>
		public ActionResult FindGoodsTypeForDrop()
		{
			var goodsType = goodsTypeSvc.FindAllGoodsType();
			return Json(goodsType);
		}

		/// <summary>
		/// 根据品牌名判断重复
		/// </summary>
		/// <param name="dtoBrand"></param>
		/// <returns></returns>
		public ActionResult FindBrandByBrandName(DtoBrand dtoBrand)
		{
			int count = brandSvc.FindBrandByBrandName(dtoBrand);
			return Json(count);
		}

		/// <summary>
		/// 根据Id查询品牌
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public ActionResult FindBrandById(Guid Id)
		{
			var dtoBrand = brandSvc.FindBrandById(Id);
			return Json(dtoBrand);
		}

		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="dtoBrand"></param>
		/// <returns></returns>
		public ActionResult Save(DtoBrand dtoBrand)
		{
			var Id = brandSvc.SaveBrand(dtoBrand);
			return Json(Id);
		}



		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public ActionResult Remove(Guid Id)
		{
			brandSvc.Remove(Id);
			return Json("");
		}


		/// <summary>
		///获取所有品牌 
		/// </summary>
		/// <returns></returns>
		protected override IQueryable<BrandViewModel> GetAllModels()
		{
			var dtoBrand = brandSvc.FindAllBrand().OrderBy(m => m.CreateTime);

			Mapper.Initialize(cfg => cfg.CreateMap<DtoBrand, BrandViewModel>());

			var viewModels = dtoBrand.ProjectTo<BrandViewModel>(dtoBrand);

			return viewModels;

		}
	}
}