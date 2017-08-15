<<<<<<< HEAD
﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hogon.Framework.Utilities.SmartList;
using Hogon.Framework.Web.Controller;
using Hogon.Store.Models.Dto.GoodsMan;
using Hogon.Store.Models.Dto.MarketingMan;
using Hogon.Store.Services.ApplicationServices.MarketingManContext;
using Hogon.Store.UserInterface.Admin.Areas.MarketingMan.Models;
using System;
=======
﻿using System;
>>>>>>> f80cd5d8ef8824bfbf25d550271c21c819442cf4
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hogon.Store.UserInterface.Admin.Areas.MarketingMan.Controllers
{
<<<<<<< HEAD
    public class FreebieController: SmartListController<FreebieLineViewModel>
    {
        FreebieApplicationService _freebie;

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

        public FreebieController(BaseViewRender<FreebieLineViewModel> listRender) : base(listRender)
        {
            _freebie = new FreebieApplicationService();
        }
=======
    public class FreebieController:Controller
    {
>>>>>>> f80cd5d8ef8824bfbf25d550271c21c819442cf4

        public ActionResult Index()
        {

            return View();
        }

<<<<<<< HEAD
=======

>>>>>>> f80cd5d8ef8824bfbf25d550271c21c819442cf4
        public ActionResult Edit()
        {
            return View();
        }

<<<<<<< HEAD
        //public ActionResult 
=======
>>>>>>> f80cd5d8ef8824bfbf25d550271c21c819442cf4
        public ActionResult Detail()
        {
            return View();
        }

<<<<<<< HEAD
        [HttpPost]
        /// <summary>
        /// 获取全部赠品信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllFreebie()
        {
            var FreebieData = _freebie.FindAllFreebie();
            return Json(FreebieData);
        }

        public ActionResult Test()
        {
            return View();
        }

        /// <summary>
        /// 根据id获取赠品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetFreebieMsg(Guid id)
        {
            var freebieMsg = _freebie.GetFreebieMsg(id);
            return Json(freebieMsg);
        }

        /// <summary>
        /// 获取所有赠品分类
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFreebieCatalog()
        {
            var freebieCatalog = _freebie.GetFreebieCatalog();
            return Json(freebieCatalog);
        }
        
        /// <summary>
        /// 获取产品商品信息（赠品明细）
        /// </summary>
        /// <param name="id">产品id</param>
        /// <returns></returns>
        public ActionResult GetFreebieLine(Guid id)
        {
            var freebieLine = _freebie.GetFreebieLine(id);
            return Json(freebieLine);
        }

        /// <summary>
        /// 保存赠品编辑信息
        /// </summary>
        /// <param name="FreebieCatalogId">赠品分类id</param>
        /// <param name="dtoFreebie"></param>
        /// <param name="ProjectGoodsId">赠品id</param>
        /// <returns></returns>
        public ActionResult SaveFreebieMsg(Guid FreebieCatalogId,DtoFreebie dtoFreebie, Guid ProductId)
        {
             _freebie.SaveFreebieMsg(FreebieCatalogId, dtoFreebie, ProductId);
            return Json("");
        }

        protected override IQueryable<FreebieLineViewModel> GetAllModels()
        {
            throw new NotImplementedException();
        }

        //protected override IQueryable<FreebieViewModel> GetAllModels()
        //{
        //    var freebies = _freebie.FindAllFreebie().OrderBy(m=>m.ProductName);
        //    Mapper.Initialize(cfg => cfg.CreateMap<DtoProduct, FreebieViewModel>());
        //    var viewModels = freebies.ProjectTo<FreebieViewModel>();

        //    return viewModels;
        //}
=======

>>>>>>> f80cd5d8ef8824bfbf25d550271c21c819442cf4
    }
}