using Hogon.Framework.Utilities.SmartList.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hogon.Store.UserInterface.Admin.Areas.GoodsMan.Models
{
	public class BrandViewModel
	{
		/// <summary>
		/// 品牌Id
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// 品牌名称
		/// </summary>
		[Field("品牌名称")]
		[TextSearch("品牌名称")]
		public string BrandName { get; set; }

		/// <summary>
		/// 商品分类名称
		/// </summary>
		[Field("商品分类")]
		public string DisplayGoodsTypeNames
		{
			get
			{
				return string.Join(",", GoodsTypeNames);
			}
		}

		/// <summary>
		/// 品牌Logo
		/// </summary>
		public string BrandLogo { get; set; }

		/// <summary>
		/// 品牌别名
		/// </summary>
		[Field("品牌别名")]
		public string BrandAlias { get; set; }

		/// <summary>
		/// 商品分类名称
		/// </summary>
		public IEnumerable<string> GoodsTypeNames { get; set; }

        /// <summary>
        /// 商品分类Id
        /// </summary>
        public IEnumerable<Guid> GoodsTypeIds { get; set; }
    }
}