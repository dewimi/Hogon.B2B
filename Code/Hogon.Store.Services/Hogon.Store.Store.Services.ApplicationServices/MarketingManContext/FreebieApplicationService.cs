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
    public class FreebieApplicationService: BaseApplicationService
    {
        FreebieRepository freebieRepository = new FreebieRepository();
        FreebieLineRepository freebieLineRes = new FreebieLineRepository();
        ProductRepository productRepository = new ProductRepository();
        FreebieCatalogRepository freebieCatalogRepository = new FreebieCatalogRepository();
        ProductGoodsRepository productGoodsRes = new ProductGoodsRepository();

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
            var freebie = productRepository.FindBy(t => t.Id ==id).First();
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
        /// <param name="id">id</param>
        /// <returns></returns>
        public IQueryable<DtoProductGoods> GetFreebieLine(Guid id)
        {
            var freebieLine = productGoodsRes.FindAll();
            var dtoFreebieLine = freebieLine.Select(m => new DtoProductGoods()
            {
                Id = m.Product.Id
            });

            return dtoFreebieLine;
        }

        /// <summary>
        /// 保存freebie和freebieLine两张表信息
        /// </summary>
        /// <param name="FreebieCatalogId">赠品分类id</param>
        /// <param name="dtoFreebie"></param>
        /// <param name="ProductId">赠品id</param>
        public void SaveFreebieMsg(Guid FreebieCatalogId, DtoFreebie dtoFreebie, Guid ProductId)
        {
            //保存Freebie表
            //dtoFreebie.FreebieCatalog = new FreebieCatalog();
            dtoFreebie.FreebieCatalog = freebieCatalogRepository.FindBy(m=>m.Id==FreebieCatalogId).First();
            Mapper.Initialize(cfg => cfg.CreateMap<DtoFreebie, Freebie>());
            var freebie = Mapper.Map<Freebie>(dtoFreebie);
            freebie.IsPublish = dtoFreebie.IsPublish;
            freebie.LimitBuyAmount = dtoFreebie.LimitBuyAmount;
            freebie.Description = dtoFreebie.Description;
            freebie.FreebiSortNum = dtoFreebie.FreebiSortNum;
            freebie.FreebieCatalog.Id = dtoFreebie.FreebieCatalog.Id;
            freebie.Product = productRepository.FindBy(m=>m.Id==ProductId).First();
            freebieRepository.Add(freebie);
            Commit();

            //保存FreebieLine表
            var freebieLine = new FreebieLine();
            freebieLine.ProductGoods= productGoodsRes.FindBy(m => m.Product.Id == ProductId).FirstOrDefault();
            freebieLine.Freebie = freebieRepository.FindBy(m=>m.Id==freebie.Id).First();
            freebieLineRes.Add(freebieLine);

            Commit();
        }

        //public IQueryable<DtoProductGoods> GetProductGoods(Guid productId)
        //{
        //    var productgoods=productGoodsRes.FindBy(m=>m)
        //}
    }
}
