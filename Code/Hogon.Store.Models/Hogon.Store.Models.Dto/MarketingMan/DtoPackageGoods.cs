using Hogon.Store.Models.Dto.GoodsMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Dto.MarketingMan
{
    public class DtoPackageGoods : BaseDto
    {
        /// <summary>
        /// 子商品集合
        /// </summary>
        public  ICollection<DtoGoods> ChildrenGoods { get; set; }



    }
}
