using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hogon.Framework.Utilities.SmartList;
using Hogon.Framework.Web.Controller;
using Hogon.Store.Models.Dto.MarketingMan;
using Hogon.Store.Services.ApplicationServices.MarketingManContext;
using Hogon.Store.UserInterface.Admin.Areas.MarketingMan.Models.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hogon.Store.UserInterface.Admin.Areas.MarketingMan.Controllers
{
    public class FreebieCatalogController : SmartListController<FreebieCatalogViewModel>
    {
        FreebieCatalogApplicationService _freebieCatalog;

        public FreebieCatalogController(BaseViewRender<FreebieCatalogViewModel> listRender) : base(listRender)
        {
            _freebieCatalog = new FreebieCatalogApplicationService();
        }

        public ActionResult Edit()
        {
            return View();
        }

       /// <summary>
       /// 编辑赠品分类信息
       /// </summary>
       /// <param name="id">id</param>
       /// <returns></returns>
        public ActionResult EditFreebie(Guid id)
        {
            var freebie = _freebieCatalog.FindFreebie(id);
            return Json(freebie);
        }

        /// <summary>
        /// 保存赠品分类
        /// </summary>
        /// <param name="dtoFreebie"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveFreebie(DtoFreebieCatalog dtoFreebie)
        {
            _freebieCatalog.SaveFreebie(dtoFreebie);
            return Json("");
        }

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 删除赠品分类
        /// </summary>
        /// <param name="Id">赠品id</param>
        /// <returns></returns>
        public ActionResult Remove(Guid Id)
        {
            _freebieCatalog.DeleteFreebie(Id);
            return Json("");
        }

        /// <summary>
        /// 获取所有赠品分类
        /// </summary>
        /// <returns></returns>
        protected override IQueryable<FreebieCatalogViewModel> GetAllModels()
        {
            var freebies = _freebieCatalog.FindAllFreebie().OrderBy(m => m.Sort);
            Mapper.Initialize(cfg => cfg.CreateMap<DtoFreebieCatalog, FreebieCatalogViewModel>());
            var viewModels = freebies.ProjectTo<FreebieCatalogViewModel>();

            return viewModels;
        }


    }
}