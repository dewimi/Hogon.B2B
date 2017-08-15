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
using System.Collections.Generic;
using Hogon.Store.Services.DomainServices.GoodsManContext;
using Hogon.Framework.Core.Owin;
using Hogon.Framework.Core.Common;
using Hogon.Store.Models.Entities.GoodsMan;

namespace Hogon.Store.UserInterface.Admin.Areas.GoodsMan.Controllers
{
    public class ProductGoodsController : SmartListController<ProductViewModel>
    {
        GoodsApplicationService goodsSvc = new GoodsApplicationService();

        GoodsDomainService goodsDomainSvc = new GoodsDomainService();

        public ProductGoodsController(BaseViewRender<ProductViewModel> listRender) : base(listRender)
        {
            //构造函数
        }

        /// <summary>
        /// GET: GoodsMan/ProductGoods/Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GET: GoodsMan/ProductGoods/Detail
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail()
        {
            ViewBag.Title = "商品管理->商品详情";
            return View();
        }

        /// <summary>
        /// GET: GoodsMan/ProductGoods/Add and Edit
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            ViewBag.Title = "商品管理->商品编辑";
            return View();
        }

        #region 下拉框赋值
        /// <summary>
        /// 查询所有品牌赋值给下拉框
        /// </summary>
        /// <returns></returns>
        public ActionResult FindAllBrandForDrop()
        {
            var dtoBrandS = goodsSvc.FindAllBrand();
            return Json(dtoBrandS);
        }
        /// <summary>
        /// 查询所有产品类型赋值给下拉框
        /// </summary>
        /// <returns></returns>
        public ActionResult FindAllProductTypeForDrop()
        {
            var dtoProductTypeS = goodsSvc.FindAllProductType();
            return Json(dtoProductTypeS);
        }

        /// <summary>
        /// 查询所有商品类型赋值给下拉框
        /// </summary>
        /// <returns></returns>
        public ActionResult FindAllGoodsType()
        {
            var dtoGoodsType = goodsSvc.FindAllGoodsType();
            return Json(dtoGoodsType);
        }

        /// <summary>
        /// 查询所有规格类型赋值给下拉框
        /// </summary>
        /// <returns></returns>
        public ActionResult FindAllSpecTypeForDrop()
        {
            var dtoSpecTypeS = goodsSvc.FindAllSpecType();
            return Json(dtoSpecTypeS);
        }
        /// <summary>
        /// 根据规格类型查询所有规格参数赋值给下拉框
        /// </summary>
        /// <param name="dtoSpecType"></param>
        /// <returns></returns>
        public ActionResult FindAllSpecTypeParameterForDrop(DtoSpecType dtoSpecType)
        {
            var dtoSpecTypeParameter = goodsSvc.FindAllSpecTypeParameter(dtoSpecType);
            return Json(dtoSpecTypeParameter);
        }
        #endregion

        #region 产品信息操作
        /// <summary>
        /// 获取所有产品Model
        /// </summary>
        /// <returns></returns>
        protected override IQueryable<ProductViewModel> GetAllModels()
        {
            var dtoProductS = goodsSvc.GetAllProductInfo().OrderByDescending(t => t.UpdateTime);
            Mapper.Initialize(cfg => cfg.CreateMap<DtoProduct, ProductViewModel>());

            var viewModels = dtoProductS.ProjectTo<ProductViewModel>(dtoProductS);

            return viewModels;
        }

        ///// <summary>
        ///// 获取所有产品信息
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult GetAllProduct()
        //{
        //    var Product = productGoodsSvc.GetAllProductInfo().ToList();

        //    return Json(Product);
        //}

        /// <summary>
        /// 根据Id查询指定产品信息
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public ActionResult FindProductById(Guid Id)
        {
            var dtoProduct = goodsSvc.FindProductById(Id);

            return Json(dtoProduct);
        }

        /// <summary>
        /// 根据产品名称查询重复
        /// </summary>
        /// <param name="dtoProduct"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FindProductByRepetition(DtoProduct dtoProduct)
        {
            var count = goodsSvc.FindProductByRepetition(dtoProduct);

            return Json(count);
        }

        /// <summary>
        /// 保存产品信息
        /// </summary>
        /// <param name="dtoProduct">产品DTO</param>
        /// <returns></returns>
        public ActionResult SaveProduct(DtoProduct dtoProduct)
        {
            var productId = goodsSvc.SaveProduct(dtoProduct);

            return Json(productId);
        }

        ///// <summary>
        ///// 删除产品信息
        ///// </summary>
        ///// <param name="ProductId">产品Id</param>
        ///// <returns></returns>
        //public ActionResult DeleteProductInfo(Guid ProductId)
        //{
        //    productGoodsSvc.DeleteProduct(ProductId);

        //    return Json(" ");
        //}
        #endregion

        #region 商品信息操作
            /// <summary>
            /// 根据产品信息，规格参数，生成商品信息
            /// </summary>
            /// <param name="dtoProduct"></param>
            /// <param name="typeJsonArray"></param>
            /// <returns></returns>
        public ActionResult CreateSpec(DtoProduct dtoProduct, ICollection<ICollection<DtoSpecTypeParameter>> typeJsonArray)
        {
            var dtoProductGoodsS = goodsDomainSvc.CreateGoods(dtoProduct, typeJsonArray);
            return Json(dtoProductGoodsS);
        }

        /// <summary>
        /// 保存产品商品
        /// </summary>
        /// <param name="listProductGoods"></param>
        /// <returns></returns>
        public ActionResult SaveProductGoods(List<string> listProductGoods)
        {
            var dtoProductGoods = goodsSvc.TransformForProductGoods(listProductGoods);
            var count = goodsSvc.FindGoodsByCode(dtoProductGoods.GoodsCode);
            if (count > 0)
            {
                throw new UserFriendlyException("商品编码不可重复");
            }
            var productGoods = goodsSvc.SaveProductGoods(dtoProductGoods);
            return Json(productGoods);
        }

        ///// <summary>
        ///// 获取所有商品信息
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult GetAllGoodsMan()
        //{
        //    var goodsMan = productGoodsSvc.GetAllGoodsManInfo().ToList();

        //    return Json(goodsMan);
        //}

        ///// <summary>
        ///// 根据Id查询指定商品信息
        ///// </summary>
        ///// <param name="GoodsId"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult GetGoodsManInfoById(Guid Id)
        //{
        //    var goodsManInfos = productGoodsSvc.GetGoodsManInfoById(Id);

        //    return Json(goodsManInfos);
        //}

        ///// <summary>
        ///// 保存商品信息
        ///// </summary>
        ///// <param name="dtoGoodsMan">商品DTO</param>
        ///// <returns></returns>
        //public ActionResult SaveGoodsManInfo(DtoProductGoods dtoGoodsMan)
        //{
        //    productGoodsSvc.SaveGoodsMan(dtoGoodsMan);

        //    return Json("");
        //}

        ///// <summary>
        ///// 删除商品信息
        ///// </summary>
        ///// <param name="Id">商品Id</param>
        ///// <returns></returns>
        //public ActionResult DeleteGoodsManInfo(Guid Id)
        //{
        //    productGoodsSvc.DeleteGoodsMan(Id);

        //    return Json(" ");
        //}
        #endregion

        #region 应用案列信息操作
        /// <summary>
        /// 获取所有应用案例信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllAppCase()
        {
            var AppCase = goodsSvc.GetAllAppCase().ToList();

            return Json(AppCase);
        }

        /// <summary>
        /// 根据Id查询指定应用案例信息
        /// </summary>
        /// <param name="AppCaseId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAppCaseById(Guid AppCaseId)
        {
            var appCaseInfos = goodsSvc.GetAppCaseById(AppCaseId);

            return Json(appCaseInfos);
        }

        /// <summary>
        /// 根据主题查询指定应用案例列表
        /// </summary>
        /// <param name="Subject"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAppCaseInfoBySubject(string Subject)
        {
            var appCaseInfos = goodsSvc.GetAppCaseInfoBySubject(Subject);

            return Json(appCaseInfos);
        }

        /// <summary>
        /// 根据作者查询指定应用案例列表
        /// </summary>
        /// <param name="Author"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAppCaseInfoByAuthor(string Author)
        {
            var appCaseInfos = goodsSvc.GetAppCaseInfoByAuthor(Author);

            return Json(appCaseInfos);
        }

        /// <summary>
        /// 根据用途查询指定应用案例列表
        /// </summary>
        /// <param name="Usage"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAppCaseInfoByAppUsage(string Usage)
        {
            var appCaseInfos = goodsSvc.GetAppCaseInfoByAppUsage(Usage);

            return Json(appCaseInfos);
        }

        /// <summary>
        /// 根据应用行业查询指定应用案例列表
        /// </summary>
        /// <param name="AppIndustry"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAppCaseInfoByAppIndustry(string AppIndustry)
        {
            var appCaseInfos = goodsSvc.GetAppCaseInfoByAppIndustry(AppIndustry);

            return Json(appCaseInfos);
        }

        /// <summary>
        /// 保存应用案例信息
        /// </summary>
        /// <param name="dtoAppCase">应用案例DTO</param>
        /// <param name="AppCaseId">应用案例Id</param>
        /// <returns></returns>
        public ActionResult SaveAppCaseInfo(DtoAppCase dtoAppCase)
        {
            goodsSvc.SaveAppCase(dtoAppCase);

            return Json("");
        }

        /// <summary>
        /// 删除应用案例信息
        /// </summary>
        /// <param name="AppCaseId">应用案例Id</param>
        /// <returns></returns>
        public ActionResult DeleteAppCaseInfo(Guid AppCaseId)
        {
            goodsSvc.DeleteAppCase(AppCaseId);

            return Json(" ");
        }
        #endregion

        #region 使用说明信息操作
        /// <summary>
        /// 获取所有使用说明信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllInstruction()
        {
            var Instruction = goodsSvc.GetAllInstruction().ToList();

            return Json(Instruction);
        }

        /// <summary>
        /// 根据Id查询指定使用说明信息
        /// </summary>
        /// <param name="InstructionId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetInstructionById(Guid InstructionId)
        {
            var instructionInfos = goodsSvc.GetInstructionById(InstructionId);

            return Json(instructionInfos);
        }

        /// <summary>
        /// 使用说明模糊查询
        /// </summary>
        /// <param name="dtoInstruction"></param>
        /// <returns></returns>
        public ActionResult FindInstructionByLike(DtoInstruction dtoInstruction)
        {
            var dtoInstructions = goodsSvc.FindInstructionByLike(dtoInstruction);
            return Json(dtoInstructions);
        }

        /// <summary>
        /// 根据主题查询指定使用说明列表
        /// </summary>
        /// <param name="Subject"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetInstructionInfoBySubject(string Subject)
        {
            var instructionInfos = goodsSvc.GetInstructionInfoBySubject(Subject);

            return Json(instructionInfos);
        }

        /// <summary>
        /// 根据作者查询指定使用说明列表
        /// </summary>
        /// <param name="Author"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetInstructionInfoByAuthor(string Author)
        {
            var instructionInfos = goodsSvc.GetInstructionInfoByAuthor(Author);

            return Json(instructionInfos);
        }

        /// <summary>
        /// 根据用途查询指定使用说明列表
        /// </summary>
        /// <param name="Usage"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetInstructionInfoByAppUsage(string Usage)
        {
            var instructionInfos = goodsSvc.GetInstructionInfoByAppUsage(Usage);

            return Json(instructionInfos);
        }

        /// <summary>
        /// 根据应用行业查询指定使用说明列表
        /// </summary>
        /// <param name="AppIndustry"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetInstructionInfoByAppIndustry(string AppIndustry)
        {
            var instructionInfos = goodsSvc.GetInstructionInfoByAppIndustry(AppIndustry);

            return Json(instructionInfos);
        }

        /// <summary>
        /// 保存使用说明信息
        /// </summary>
        /// <param name="dtoInstruction">使用说明DTO</param>
        /// <param name="InstructionId">使用说明Id</param>
        /// <returns></returns>
        public ActionResult SaveInstructionInfo(DtoInstruction dtoInstruction)
        {
            var InstructionId = goodsSvc.SaveInstruction(dtoInstruction);

            return Json(InstructionId);
        }

        /// <summary>
        /// 删除使用说明信息
        /// </summary>
        /// <param name="InstructionId">使用说明Id</param>
        /// <returns></returns>
        public ActionResult DeleteInstructionInfo(Guid InstructionId)
        {
            goodsSvc.DeleteInstruction(InstructionId);

            return Json(" ");
        }
        #endregion

        #region 服务商品信息操作
        /// <summary>
        /// 获取所有服务商品信息
        /// </summary>
        /// <returns></returns>
        ///
        public ActionResult GetAllServiceGoods()
        {
            var ServiceGoods = goodsSvc.FindAllServiceGoods();

            return Json(ServiceGoods);
        }
        /// <summary>
        /// 根据集合Id查询服务商品
        /// </summary>
        /// <param name="ServiceGoodsId"></param>
        /// <returns></returns>
        public ActionResult FindServiceGoodsByListId(ICollection<Guid> ServiceGoodsId)
        {
            var dtoServiceGoods = goodsSvc.FindServiceGoodsByListId(ServiceGoodsId);

            return Json(dtoServiceGoods);
        }

        /// <summary>
        /// 根据Id查询指定服务商品信息
        /// </summary>
        /// <param name="GoodsId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FindServiceGoodsById(Guid ServiceGoodsId)
        {
            var dtoServiceGoods = goodsSvc.FindServiceGoodsById(ServiceGoodsId);

            return Json(dtoServiceGoods);
        }

        ///// <summary>
        ///// 根据服务编码查询指定服务商品列表
        ///// </summary>
        ///// <param name="ServiceName"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult FindServiceGoodsInfoByGoodsCode(DtoServiceGoods dtoServiceGoods)
        //{
        //    int count = goodsSvc.FindServiceGoodsByGoodsCode(dtoServiceGoods);

        //    return Json(count);
        //}

        /// <summary>
        /// 根据服务编号查询指定服务商品列表
        /// </summary>
        /// <param name="ServiceNum"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetServiceGoodsInfoByGoodsCode(DtoServiceGoods dtoServiceGoods)
        {
            var serviceGoodsInfos = goodsSvc.FindServiceGoodsByGoodsCode(dtoServiceGoods);

            return Json(serviceGoodsInfos);
        }

        /// <summary>
        /// 保存服务商品信息
        /// </summary>
        /// <param name="dtoServiceGoods">服务商品DTO</param>
        /// <param name="serviceGoodsId">服务商品Id</param>
        /// <returns></returns>
        public ActionResult SaveServiceGoods(DtoServiceGoods dtoServiceGoods)
        {
            goodsSvc.SaveServiceGoods(dtoServiceGoods);

            return Json("");
        }

        /// <summary>
        /// 删除服务商品信息
        /// </summary>
        /// <param name="ServiceGoodsId">服务商品Id</param>
        /// <returns></returns>
        public ActionResult DeleteServiceGoods(Guid ServiceGoodsId)
        {
            goodsSvc.Remove(ServiceGoodsId);

            return Json(" ");
        }
        #endregion
    }
}