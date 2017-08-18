﻿using Hogon.Framework.Web.Controller;
using Hogon.Store.Services.ApplicationServices.MarketingManContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hogon.Store.UserInterface.Admin.Areas.MarketingMan.Controllers
{
    public class PackageGoodsController : Controller
    {
        PackageGoodsApplicationService packagegoodsAppSer = new PackageGoodsApplicationService();

        public PackageGoodsController()
        {

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
        /// 进入商品选择界面
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GotoDetail()
        {
            return Redirect("/PackageGoods/Detail");
        }

      

        /// <summary>
        /// 选择组合商品
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoods()
        {
            var goods = packagegoodsAppSer.FindAllGoods();
            return Json(goods);
        }

        public ActionResult GetPackGoodsMsg(Guid[] id)
        {

            return Redirect("");
        }

    }
}