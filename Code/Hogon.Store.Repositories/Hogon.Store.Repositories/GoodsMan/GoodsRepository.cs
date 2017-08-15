using Hogon.Framework.Core.UnitOfWork.EntityFramework;
using Hogon.Store.Models.Entities.GoodsMan;

namespace Hogon.Store.Repositories.GoodsMan
{
    public class GoodsRepository : EFRepository<Models.Entities.GoodsMan.Goods>
    {

		public Goods DeleteServiceGoods(Goods goods)
		{
			return DeleteObject(goods);
		}
        public Rela_Goods_GoodsType DeleteRela_Goods_GoodsType(Rela_Goods_GoodsType rela_Goods_GoodsType)
        {
            return DeleteObject(rela_Goods_GoodsType);
        }
    }
}
