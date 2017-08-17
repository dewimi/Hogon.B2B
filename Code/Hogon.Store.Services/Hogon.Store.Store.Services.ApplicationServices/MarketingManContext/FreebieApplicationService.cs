using AutoMapper;
using Hogon.Framework.Core.Common;
using Hogon.Framework.Core.Owin;
using Hogon.Framework.Core.UnitOfWork;
using Hogon.Store.Models.Dto.GoodsMan;
using Hogon.Store.Models.Dto.MarketingMan;
using Hogon.Store.Models.Entities.GoodsMan;
using Hogon.Store.Models.Entities.MarketingMan;
using Hogon.Store.Repositories.GoodsMan;
using Hogon.Store.Repositories.MarketingMan;
using Hogon.Store.Services.DomainServices.SecurityContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Services.ApplicationServices.MarketingManContext
{
    public class FreebieApplicationService : BaseApplicationService
    {
        FreebieRepository freebieRepository = new FreebieRepository();
        ProductRepository productRepository = new ProductRepository();
        FreebieCatalogRepository freebieCatalogRepository = new FreebieCatalogRepository();
        ProductGoodsRepository productGoodsRes = new ProductGoodsRepository();
        GoodsRepository goodsRes = new GoodsRepository();

        AuthorizationDomainService _authorizationService = new AuthorizationDomainService();
        Md5Encryptor _encryptor = new Md5Encryptor();

        public FreebieApplicationService()
        {

        }

        /// <summary>
        /// 获取所有赠品名称、编号
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoProduct> FindAllFreebie()
        {
            var freebie = productRepository.FindAll();
            var dtoFreebie = freebie.Select(m => new DtoProduct()
            {
                Id = m.Id,
                ProductName = m.ProductName,
                ProductCode = m.ProductCode,
                ProductTypeName = m.ProductType.TypeName
            });
            return dtoFreebie;
        }

        /// <summary>
        /// 根据id获取赠品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetFreebieMsg(Guid id)
        {
            var freebie = productRepository.FindBy(t => t.Id == id).FirstOrDefault();
            return freebie;
        }

        /// <summary>
        /// 查找所有赠品分类
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoFreebieCatalog> GetFreebieCatalog()
        {
            var freebieCa = freebieCatalogRepository.FindAll();
            var dtoFreebieCa = freebieCa.Select(m => new DtoFreebieCatalog()
            {
                Id = m.Id,
                FreebieCatalogName = m.FreebieCatalogName,
                Sort = m.Sort
            });
            return dtoFreebieCa;
        }

        /// <summary>
        /// 根据产品id获取所有产品商品信息
        /// </summary>
        /// <param name="id">产品id</param>
        /// <returns></returns>
        public IQueryable<DtoProductGoods> GetFreebieLine(Guid id)
        {
            //var freebieLine = productGoodsRes.FindAll().OfType<ProductGoods>();
            var freebieLine = productGoodsRes.FindBy(m => m.Product.Id == id);
            var dtoFreebieLine = freebieLine.Select(m => new DtoProductGoods()
            {
                Id = m.Id,
                GoodsCode = m.GoodsCode,
                SpecParameterS = "mid", //规格类型：规格参数格式JSON
                Weight = m.Weight
            });

            return dtoFreebieLine;
        }

        /// <summary>
        /// 保存freebie和freebieLine两张表信息
        /// </summary>
        /// <param name="FreebieCatalogId">赠品分类id</param>
        /// <param name="dtoFreebie"></param>
        /// <param name="ProductId">赠品id</param>
        public void SaveFreebieMsg(Guid FreebieCatalogId, DtoFreebie dtoFreebie, Guid ProductId, Guid[] ProductGoodsId, int[] Qupta)
        {
            //保存Freebie表
            //dtoFreebie.FreebieCatalog = freebieCatalogRepository.FindBy(m => m.Id == FreebieCatalogId).First();

            Mapper.Initialize(cfg => cfg.CreateMap<DtoFreebie, Freebie>());
            var freebie = Mapper.Map<Freebie>(dtoFreebie);
            freebie.IsPublish = dtoFreebie.IsPublish;
            freebie.LimitBuyAmount = dtoFreebie.LimitBuyAmount;
            freebie.Description = dtoFreebie.Description;
            freebie.FreebiSortNum = dtoFreebie.FreebiSortNum;
            //freebie.FreebieCatalog.Id = dtoFreebie.FreebieCatalogId;
            freebie.FreebieCatalog = freebieCatalogRepository.FindBy(m => m.Id == FreebieCatalogId).First();
            freebie.Product = productRepository.FindBy(m => m.Id == ProductId).First();
            freebieRepository.Add(freebie);

            //保存FreebieLine表
            for (int i = 0; i < ProductGoodsId.Length; i++)
            {
                var freebieLine = new FreebieLine();
                Guid proGoodId = ProductGoodsId[i];
                freebieLine.ProductGoods = productGoodsRes.FindBy(m => m.Id == proGoodId).FirstOrDefault();
                freebieLine.Quota = Qupta[i];
                //freebieLine.Freebie = freebieRepository.FindBy(m => m.Id == freebie.Id).First();
                freebie.FreebieLines.Add(freebieLine);
                //freebieRepository.deleteObject  删除
            }

            Commit();
        }

        /// <summary>
        /// 获取赠品信息列表
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoFreebieLine> FindFreebieLine()
        {
            var freebieLine = freebieRepository.FindAll();
            var dtoFreebieLine = freebieLine.Select(m => new DtoFreebieLine()
            {
                Id = m.Id,
                FreebieCaltalogName = m.FreebieCatalog.FreebieCatalogName,
                FreebieName = m.Product.ProductName,
                CreatePerson = m.CreatePerson,
                CreateTime = m.CreateTime,
                UpdatePerson = m.UpdatePerson,
                UpdateTime = m.UpdateTime
            });

            return dtoFreebieLine;
        }

        /// <summary>
        /// 获取赠品信息
        /// </summary>
        /// <param name="id">赠品id</param>
        /// <returns></returns>
        public DtoFreebie GetFreebieList(Guid id)
        {
            var freebie = freebieRepository.FindBy(t => t.Id == id).Select(m => new DtoFreebie()
            {
                Description = m.Description,
                //dtoFreebieCatalog = m.FreebieCatalog,
                FreebieCatalogName = m.FreebieCatalog.FreebieCatalogName,
                Sort = m.FreebieCatalog.Sort,
                FreebieLines = m.FreebieLines,
                //Product = m.Product,
                FreebiSortNum = m.FreebiSortNum,
                IsPublish = m.IsPublish,
                LimitBuyAmount = m.LimitBuyAmount,
                Id = m.Id,
                FreebieCatalogId = m.FreebieCatalog.Id,
                ProductId = m.Product.Id,
                ProductCode = m.Product.ProductCode,
                ProductName = m.Product.ProductName

            }).FirstOrDefault();

            return freebie;
        }

        /// <summary>
        /// 获取赠品列表
        /// </summary>
        /// <param name="id">赠品id</param>
        /// <returns></returns>
        public IQueryable<DtoFreebieLines> AddFrebieList(Guid id)
        {
            var freebie = freebieRepository.FindBy(m => m.Id == id).SelectMany(f => f.FreebieLines);
            var dtoFreebie = freebie.Select(m => new DtoFreebieLines()
            {
                GoodsCode=m.ProductGoods.GoodsCode,
                Weight = m.ProductGoods.Weight,
                Quota = m.Quota, Id=m.ProductGoods.Id,
                SpecParameterS ="M"
            });

            return dtoFreebie;
        }

        /// <summary>
        /// 删除赠品
        /// </summary>
        /// <param name="id">赠品id</param>
        public void DeleteFreebie(Guid id)
        {
            var freebie = freebieRepository.FindBy(m => m.Id == id).First();
            freebieRepository.DeleteFreebie(freebie);
            Commit();
        }


    }
}
