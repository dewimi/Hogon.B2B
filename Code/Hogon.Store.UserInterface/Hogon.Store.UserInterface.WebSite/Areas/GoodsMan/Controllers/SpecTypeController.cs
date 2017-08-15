using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hogon.Framework.Utilities.SmartList;
using Hogon.Framework.Web.Controller;
using Hogon.Store.Models.Dto.GoodsMan;
using Hogon.Store.Services.ApplicationServices.GoodsManContext;
using Hogon.Store.UserInterface.WebSite.Areas.GoodsMan.Models.SpecType;
using Hogon.Store.UserInterface.WebSite.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hogon.Store.UserInterface.WebSite.Areas.GoodsMan.Controllers
{
    public class SpecTypeController : SmartListController<SpecTypeViewModel>
    {
        SpecTypeApplicationService specTypeSvc = new SpecTypeApplicationService();
        public SpecTypeController(BaseViewRender<SpecTypeViewModel> listRender) : base(listRender)
        {

        }
        // GET: GoodsMan/SpecType
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 保存规格类型
        /// </summary>
        /// <param name="dtoSpecType">规格类型</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveSpecType(DtoSpecType dtoSpecType,Guid productTypeId)
        {
            //得到返回的规格类型Id
            var specTypeId =  specTypeSvc.SaveSpecType(dtoSpecType, productTypeId);

            return Json(specTypeId);
        }

        /// <summary>
        /// 保存规格参数
        /// </summary>
        /// <param name="dtoSpecTypeParameter">规格参数</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveSpecParameter(DtoSpecTypeParameter dtoSpecTypeParameter, Guid specTypeId, Guid fileId)
        {

            specTypeSvc.SaveSpecParameter(dtoSpecTypeParameter, specTypeId,fileId);
            return Json("");
        }

        /// <summary>
        /// 根据规格类型Id查询规格参数
        /// </summary>
        /// <param name="specTypeId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetParametersById(Guid specTypeId)
        {
           var specTypeParameters =  specTypeSvc.GetParametersById(specTypeId);

            return Json(specTypeParameters);
        }

        /// <summary>
        /// 根据规格类型Id查询相应数据
        /// </summary>
        /// <param name="specTypeId">规格类型Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSpecTypeBySpecTypeId( Guid specTypeId)
        {
            //把返回的数据转换为ViewModel类型
           var dtoSpecType =  specTypeSvc.GetSpecTypeBySpecTypeId(specTypeId);
            Mapper.Initialize(cfg => cfg.CreateMap<DtoSpecType, SpecTypeViewModel>());
            var specType = Mapper.Map<SpecTypeViewModel>(dtoSpecType);
            return Json(specType);
        }

        /// <summary>
        /// 删除规格类型
        /// </summary>
        /// <param name="specTypeId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveSpecType(Guid specTypeId)
        {
            //specTypeSvc.RemoveSpecType(specTypeId);

            return Json("");

        }

        /// <summary>
        /// 获取所有规格类型
        /// </summary>
        /// <returns></returns>
        protected override IQueryable<SpecTypeViewModel> GetAllModels()
        {
           var specTypes = specTypeSvc.GetAllSpecTypes().OrderByDescending(m => m.CreateTime);
            Mapper.Initialize(cfg => cfg.CreateMap<DtoSpecType, SpecTypeViewModel>());
            var viewModels = specTypes.ProjectTo<SpecTypeViewModel>();

            return viewModels;

        }


    }
}