using Hogon.Store.Models.Entities.Common;
using System;
using System.Collections.Generic;

namespace Hogon.Store.Models.Entities.GoodsMan
{
    /// <summary>
    /// 服务商品
    /// </summary>
    public class PackageGoods : Goods
    {
        public override ICollection<Goods> ChildrenGoods
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}