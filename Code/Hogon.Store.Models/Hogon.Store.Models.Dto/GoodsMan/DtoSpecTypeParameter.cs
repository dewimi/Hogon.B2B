﻿
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
        /// 图片Id
        /// </summary>
        public Guid? FileId { get; set; }

        /// <summary>
        /// 规格类型
        /// </summary>
        public DtoSpecType SpecType { get; set; }
    }
}
