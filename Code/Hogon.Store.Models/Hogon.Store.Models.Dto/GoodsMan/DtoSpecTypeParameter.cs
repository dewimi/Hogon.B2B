
using Hogon.Store.Models.Entities.Common;
using Hogon.Store.Models.Entities.GoodsMan;
using System;

namespace Hogon.Store.Models.Dto.GoodsMan
{
    public class DtoSpecTypeParameter : BaseDto
    {
        public string ParameterName { get; set; }

        /// <summary>
        /// 规格类型Id
        /// </summary>
        public Guid SpecTypeId { get; set; }

        /// <summary>
        /// 规格类型
        /// </summary>
        public SpecType SpecType { get; set; }
    }
}
