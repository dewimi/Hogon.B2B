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

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

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
        /// 根据产品类型查询规格类型
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <returns></returns>
        public ActionResult FindSpecTypeByProductType(Guid productTypeId)
        {
            var dtoSpecTypes = goodsSvc.FindSpecTypeByProductTypeId(productTypeId);

            return Json(dtoSpecTypes);
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
            goodsSvc.ProductAddSepcTypePara(dtoProductGoodsS, dtoProduct);
            return Json(dtoProductGoodsS);
        }

        /// <summary>
        /// 保存产品商品
        /// </summary>
        /// <param name="listProductGoods"></param>
        /// <returns></returns>
        public ActionResult SaveProductGoods(Guid productId, List<string> listProductGoods)
        {
            var dtoProductGoods = goodsSvc.TransformForProductGoods(listProductGoods);
            var count = goodsSvc.FindGoodsByCode(dtoProductGoods.GoodsCode);
            if (count > 0)
            {
                throw new UserFriendlyException("商品编码不可重复");
            }

            var productGoods = goodsSvc.SaveProductGoods(productId,dtoProductGoods);
            return Json(productGoods);
        }

        /// <summary>
        /// 根据产品查询商品数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult FindProductGoodsByProduct(Guid Id)
        {
            var dtoProductGoods = goodsSvc.FindProductGoodsByProduct(Id);
            return Json(dtoProductGoods);
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
        /// 应用案例模糊查询
        /// </summary>
        /// <param name="dtoInstruction"></param>
        /// <returns></returns>
        public ActionResult FindAppCaseByLike(DtoAppCase dtoAppCase)
        {
            var appCases = goodsSvc.FindAppCaseByLike(dtoAppCase);
            return Json(appCases);
        }

        /// <summary>
        /// 根据产品查询应用案例
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ActionResult FindAppCaseByProductId(Guid productId)
        {
            var appCases = goodsSvc.FindAppCaseByProductId(productId);
            return Json(appCases);
        }

        /// <summary>
        /// 根据Id查询指定应用案例信息
        /// </summary>
        /// <param name="AppCaseId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FindAppCaseById(Guid Id)
        {
            var appCaseInfos = goodsSvc.FindAppCaseById(Id);

            return Json(appCaseInfos);
        }

        /// <summary>
        /// 保存应用案例信息
        /// </summary>
        /// <param name="dtoAppCase">应用案例DTO</param>
        /// <param name="AppCaseId">应用案例Id</param>
        /// <returns></returns>
        public ActionResult SaveAppCase(DtoAppCase dtoAppCase)
        {
            var appCase = goodsSvc.SaveAppCase(dtoAppCase);

            return Json(appCase);
        }

        /// <summary>
        /// 删除应用案例信息
        /// </summary>
        /// <param name="AppCaseId">应用案例Id</param>
        /// <returns></returns>
        public ActionResult DeleteAppCase(Guid Id)
        {
            goodsSvc.RemoveAppCase(Id);

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
        public ActionResult FindInstructionById(Guid Id)
        {
            var instructionInfos = goodsSvc.FindInstructionById(Id);

            return Json(instructionInfos);
        }

        /// <summary>
        /// 根据产品查询使用说明
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ActionResult FindInstructionByProductId(Guid productId)
        {
            var instructions = goodsSvc.FindInstructionByProductId(productId);
            return Json(instructions);
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
        public ActionResult SaveInstruction(DtoInstruction dtoInstruction)
        {
            var Instruction = goodsSvc.SaveInstruction(dtoInstruction);

            return Json(Instruction);
        }

        /// <summary>
        /// 删除使用说明信息
        /// </summary>
        /// <param name="InstructionId">使用说明Id</param>
        /// <returns></returns>
        public ActionResult DeleteInstruction(Guid Id)
        {
            goodsSvc.RemoveInstruction(Id);

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

        /// <summary>
        /// 根据产品Id查询服务商品
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult FindServiceGoodsByProductId(Guid Id)
        {
            var dtoServiceGoods = goodsSvc.FindServiceGoodsByProductId(Id);
            return Json(dtoServiceGoods);
        }

        /// <summary>
        /// 商品管理界面删除服务商品
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ActionResult RemoveServiceGoodsInProduct(Guid Id, Guid productId)
        {
            goodsSvc.RemoveServiceGoodsInProduct(Id, productId);
            //goodsSvc.RemoveServiceGoods(Id);
            return Json("");
        }

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
        public ActionResult EditServiceGoodsInProduct(Guid Id, ICollection<Guid> serviceGoodsId)
        {
            var bl = goodsSvc.EditServiceGoodsInProductGoods(Id, serviceGoodsId);

            return Json("");
        }

        /// <summary>
        /// 删除服务商品信息
        /// </summary>
        /// <param name="ServiceGoodsId">服务商品Id</param>
        /// <returns></returns>
        public ActionResult DeleteServiceGoods(Guid ServiceGoodsId)
        {
            goodsSvc.RemoveServiceGoods(ServiceGoodsId);

            return Json(" ");
        }
        #endregion
    }
}
