using Hogon.Framework.Core.UnitOfWork;
using Hogon.Store.Models.Dto.GoodsMan;
using Hogon.Store.Models.Entities.GoodsMan;
using Hogon.Store.Repositories.GoodsMan;
using Hogon.Store.Repositories.MarketingMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Services.ApplicationServices.MarketingManContext
{
  public   class PackageGoodsApplicationService: BaseApplicationService
    {
        PackageGoodsRepository packageGoodsRes = new PackageGoodsRepository();
        GoodsRepository goodsRes = new GoodsRepository();
        ProductGoodsRepository productGoodsRes = new ProductGoodsRepository();

        public PackageGoodsApplicationService()
        {

        }

        /// <summary>
        /// 获取所有商品
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoPackageGoods> FindAllGoods()
        {
            //var productGoods = productGoodsRes.FindAll();
            //var dtoProductGoods = productGoods.Select(m => new DtoProductGoods()
            //{
            //    Id = m.Id,
            //    GoodsName = m.GoodsName,
            //    SalePrice = m.SalePrice,
            //    GoodsCode = m.GoodsCode
            //});
            //return dtoProductGoods;

            var goods = goodsRes.FindAll();
            var dtoGoods = goods.Select(m => new DtoPackageGoods()
            {
                Id = m.Id,
                GoodsName = m.GoodsName,
                GoodsAlias = m.GoodsAlias,
                SalePrice = m.SalePrice,
                GoodsCode = m.GoodsCode
            });

            return dtoGoods;
        }

        public IQueryable<DtoGoods> GetPackGoodsMsg(Guid[] goodsId)
        {
            ICollection<DtoGoods> dtoGoods = new List<DtoGoods>();
            for (int i = 0; i < goodsId.Length; i++)
            {
                Guid goodId = goodsId[i];
                var goods = goodsRes.FindBy(m => m.Id == goodId).Select(s => new DtoGoods()
                {
                    GoodsName = s.GoodsName,
                    SalePrice = s.SalePrice,
                    Id = s.Id,
                    GoodsCode = s.GoodsCode
                }).FirstOrDefault();
                dtoGoods.Add(goods);
            }

            return dtoGoods.AsQueryable();
        }


    }
}
