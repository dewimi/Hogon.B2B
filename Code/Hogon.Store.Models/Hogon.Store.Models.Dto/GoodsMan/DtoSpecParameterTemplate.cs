using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Dto.GoodsMan
{
   public class DtoSpecParameterTemplate : BaseDto
    {

        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParameterName { get; set; }

        ///// <summary>
        ///// 规格类型Id
        ///// </summary>
        //public Guid SpecTypeId { get; set; }

        ///// <summary>
        ///// 规格类型
        ///// </summary>
        //public SpecType SpecType { get; set; }
    }
}
