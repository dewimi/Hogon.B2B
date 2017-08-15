using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Models.Dto.MarketingMan
{
    public class DtoFreebieCatalog:BaseDto
    {
        /// <summary>
        /// 赠品分类名称
        /// </summary>
        public string FreebieCatalogName { get; set; }

        /// <summary>
        /// 赠品分类排序
        /// </summary>
        public int Sort { set; get; }

    }
}
