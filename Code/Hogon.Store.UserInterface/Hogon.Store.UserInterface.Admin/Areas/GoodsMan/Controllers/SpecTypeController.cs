using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hogon.Framework.Core.Common;
using Hogon.Framework.Utilities.SmartList;
using Hogon.Framework.Web.Controller;
using Hogon.Store.Models.Dto.GoodsMan;
using Hogon.Store.Services.ApplicationServices.GoodsManContext;
using Hogon.Store.UserInterface.Admin.Areas.GoodsMan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hogon.Store.UserInterface.Admin.Areas.GoodsMan.Controllers
{
    public class SpecTypeController : SmartListController<SpecTypeViewModel>
    {
        SpecTypeApplicationService specTypeSvc = new SpecTypeApplicationService();
        public SpecTypeController(BaseViewRender<SpecTypeViewModel> listRender) : base(listRender)
        {

        }
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

        // GET: GoodsMan/SpecType
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
        /// 保存规格类型
        /// </summary>
        /// <param name="dtoSpecType">规格类型</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveSpecType(DtoSpecType dtoSpecType, Guid productTypeId)
        {
            //得到返回的规格类型Id
            var specTypeId = specTypeSvc.SaveSpecType(dtoSpecType, productTypeId);
            if (specTypeId == new Guid())
            {
                throw new UserFriendlyException("数据已存在");

            }

            return Json(specTypeId);
        }

        /// <summary>
        /// 保存规格参数
        /// </summary>
        /// <param name="dtoSpecTypeParameter">规格参数</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveSpecParameter(DtoSpecTypeParameter dtoSpecTypeParameter, Guid specTypeId,Guid? fileId)
        {
           var parameterId = specTypeSvc.SaveSpecParameter(dtoSpecTypeParameter, specTypeId, fileId);

            return Json(parameterId);
        }

        /// <summary>
        /// 根据规格类型Id查询规格参数
        /// </summary>
        /// <param name="specTypeId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetParametersById(Guid specTypeId)
        {
            var specTypeParameters = specTypeSvc.GetParametersById(specTypeId);
            Mapper.Initialize(cfg => cfg.CreateMap<DtoSpecTypeParameter, SpecTypeParameterViewModel>());
            var specTypeParameterViewModels = specTypeParameters.ProjectTo<SpecTypeParameterViewModel>();
            return Json(specTypeParameterViewModels);
        }

        /// <summary>
        /// 根据规格类型Id查询相应数据
        /// </summary>
        /// <param name="specTypeId">规格类型Id</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSpecTypeBySpecTypeId(Guid specTypeId)
        {
            //把返回的数据转换为ViewModel类型
            var dtoSpecType = specTypeSvc.GetSpecTypeBySpecTypeId(specTypeId);
            Mapper.Initialize(cfg => cfg.CreateMap<DtoSpecType, SpecTypeViewModel>());
            var specTypeViewModel = Mapper.Map<SpecTypeViewModel>(dtoSpecType);

            return Json(specTypeViewModel);
        }

        /// <summary>
        /// 删除规格类型
        /// </summary>
        /// <param name="specTypeId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Remove(Guid Id)
        {
            specTypeSvc.RemoveSpecType(Id);

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