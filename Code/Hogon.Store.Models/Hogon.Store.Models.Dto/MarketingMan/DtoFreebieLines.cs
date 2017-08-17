using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Dto.MarketingMan
{
 public   class DtoFreebieLines
    {
        /// <summary>
    /// 产品商品id
    /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 配额
        /// </summary>
        public int Quota { get; set; }


        /// <summary>
        /// 产品商品（赠品）重量
        /// </summary>
        public Decimal Weight { get; set; }

        /// <summary>
        /// 产品商品规格参数
        /// </summary>
        public String SpecParameterS { get; set; }


        /// <summary>
        /// 产品商品的编码
        /// </summary>
        public string GoodsCode { get; set; }


    }
}
