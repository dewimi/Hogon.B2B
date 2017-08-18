using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Dto.MarketingMan
{
 public   class DtoFreebieLine:BaseDto
    {
        /// <summary>
        /// 赠品名称
        /// </summary>
        public string FreebieName
        {
            get; set;
        }

        /// <summary>
        /// 赠品所属分类
        /// </summary>
        public string FreebieCaltalogName
        {
            get; set;
        }
    }
}
