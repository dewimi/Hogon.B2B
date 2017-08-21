using Hogon.Framework.Core.UnitOfWork;
using Hogon.Store.Models.Dto.GoodsMan;
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

        public PackageGoodsApplicationService()
        {

        }


        public IQueryable<DtoPackageGoods> FindAllGoods()
        {
            var goods = goodsRes.FindAll();
            var dtoGoods = goods.Select(m => new DtoPackageGoods()
            {
                Id = m.Id,
                GoodsName = m.GoodsName,
                SalePrice = m.SalePrice,
                GoodsCode = m.GoodsCode
            });

            return dtoGoods;
        }


    }
}
