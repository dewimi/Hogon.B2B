using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Dto.GoodsMan
{
    public class DtoRela_Brand_GoodsType
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public DtoBrand Brand { get; set; }

        /// <summary>
        /// 商品分类
        /// </summary>
        public DtoGoodsType GoodsType { get; set; }
    }
}
