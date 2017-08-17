using Hogon.Framework.Core.Owin;
using Hogon.Store.Models.Dto.GoodsMan;
using Hogon.Store.Models.Entities.GoodsMan;
using Hogon.Store.Repositories.GoodsMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Services.DomainServices.GoodsManContext
{
    public class GoodsDomainService
    {
        SpecParameterTemplateRepository specParameterTemplateReps = new SpecParameterTemplateRepository();
        SpecTypeParameterRepository specTypeParameterReps = new SpecTypeParameterRepository();

        SpecTypeRepository specTypeReps = new SpecTypeRepository();

        ProductRepository productReps = new ProductRepository();

        ProductTypeRepository productTypeReps = new ProductTypeRepository();

        ProductTypeCategoryRepository productTypeCategory = new ProductTypeCategoryRepository();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtoProduct"></param>
        /// <param name="specTypeParams"></param>
        /// <returns></returns>
        public ICollection<DtoProductGoods> CreateGoods(DtoProduct dtoProduct
            , ICollection<ICollection<DtoSpecTypeParameter>> specTypeParams)
        {
            // 获取排列组合的商品参数集合
            var goodsParams = CreateSpecParameters(specTypeParams);

            ICollection<DtoSpecParameterTemplate> dtoSpecParameterTemplateS = new List<DtoSpecParameterTemplate>();

            foreach (var item in goodsParams)
            {
                foreach (var spec in item)
                {
                    DtoSpecParameterTemplate dtoSpecParameterTemplate = new DtoSpecParameterTemplate();

                    var s = specParameterTemplateReps.FindBy(m => m.Id == spec.Id).First();

                    dtoSpecParameterTemplate.CreatePerson = s.CreatePerson;
                    dtoSpecParameterTemplate.CreateTime = s.CreateTime;
                    dtoSpecParameterTemplate.UpdatePerson = s.UpdatePerson;
                    dtoSpecParameterTemplate.UpdateTime = s.UpdateTime;
                    dtoSpecParameterTemplate.Id = spec.Id;
                    dtoSpecParameterTemplate.ParameterName = spec.ParameterName;

                    dtoSpecParameterTemplateS.Add(dtoSpecParameterTemplate);
                }
            }
            
            ICollection<DtoProductGoods> dtoProductGoodsS = new List<DtoProductGoods>();
            var result = "";
            int num = 1;
            foreach (var item in goodsParams)
            {
                var specParameter = "";
                var specTypeName = "";
                foreach (var specParams in item)
                {
                    var specType = specTypeReps.FindBy(m => m.Id == specParams.SpecTypeId).First();

                    specParameter = specParams.ParameterName + ";";
                    specTypeName += specType.SpecName + ":" + specParameter;
                }
                //result = specParameter;
                result = specTypeName;

                DtoProductGoods dtoProductGoods = new DtoProductGoods()
                {
                    CreatePerson = UserState.Current.UserName,
                    CreateTime = DateTime.Now,
                    GoodsAlias = " ",
                    GoodsCode = dtoProduct.ProductCode + num.ToString(),
                    GoodsDesription = dtoProduct.ProductDescription,
                    GoodsName = dtoProduct.ProductName,
                    IsAvailable = true,
                    IsDefaultGoods = true,
                    SalePrice = dtoProduct.SalerBasicPrice,
                    SearchKeywords = dtoProduct.SearchPrimaryKey,
                    DtoSpecParameterTemplateS = null,//规格参数模板集合
                    //ChildrenGoods = null,//子商品集合
                    ProductId = productReps.FindBy(m => m.Id == dtoProduct.Id).First().Id,
                    ProductName = productReps.FindBy(m => m.Id == dtoProduct.Id).First().ProductName,
                    SpecParameterS = result,
                    UpdatePerson = UserState.Current.UserName,
                    UpdateTime = DateTime.Now,
                };
                num++;

                dtoProductGoodsS.Add(dtoProductGoods);
            }
            
            return dtoProductGoodsS;
        }


        /// <summary>
        /// 根据商品类型规格参数创建商品规格参数排列组合
        /// </summary>
        /// <param name="typeParams"></param>
        protected List<ICollection<DtoSpecTypeParameter>> CreateSpecParameters
            (ICollection<ICollection<DtoSpecTypeParameter>> typeParams)
        {
            List<ICollection<DtoSpecTypeParameter>> goodsParamtersList =
                new List<ICollection<DtoSpecTypeParameter>>();

            var typeParamsClone = typeParams.ToArray().ToList();
            ManipulateSpecParameters(ref goodsParamtersList, typeParamsClone, null, null);

            return goodsParamtersList;
        }

        /// <summary>
        /// 递归对商品规格参数列表添加对象
        /// </summary>
        /// <param name="goodsParamtersList"></param>
        /// <param name="typeParams"></param>
        /// <param name="lastPath"></param>
        /// <param name="lastNode"></param>
        /// <returns></returns>
        protected void ManipulateSpecParameters
            (ref List<ICollection<DtoSpecTypeParameter>> goodsParamtersList
                , ICollection<ICollection<DtoSpecTypeParameter>> typeParams
                , ICollection<DtoSpecTypeParameter> lastPath
                , DtoSpecTypeParameter lastNode)
        {
            // 当前路径
            ICollection<DtoSpecTypeParameter> currentPath = null;
            // 获取目前规格类型参数的第一组作为当前规格类型的参数
            var currentTypeParams = typeParams.First();
            // 每移动一层规格类型，取消第一组规格类型的参数
            typeParams.Remove(currentTypeParams);

            // 当上个方法的路径为空时，一定是第一次调用该方法
            if (lastPath == null)
            {
                currentPath = new List<DtoSpecTypeParameter>();
            }
            else
            {
                // 克隆一个新的当前路径，应该有更简单的方法
                currentPath = lastPath.ToArray().ToList();
                // 上个路径会根据同层节点循环多次调用，新节点作为末节点放到当前路径
                currentPath.Add(lastNode);
            }

            foreach (var currentNode in currentTypeParams)
            {
                // 当规格类型层数不是最末层级时，继续调用递归函数
                if (typeParams.Count > 0)
                {
                    ManipulateSpecParameters(ref goodsParamtersList, typeParams.ToArray().ToList(), currentPath, currentNode);
                }
                // 当规格类型层数已是最末层级时，创建新商品数据节点路径并加入goodsParamtersList
                else
                {
                    // 克隆一个最终路径并加入最末节点来完成它
                    var newPath = currentPath.ToArray().ToList();
                    newPath.Add(currentNode);

                    goodsParamtersList.Add(newPath);

                }
            }
        }
   }
}
