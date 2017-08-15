using Hogon.Framework.Utilities.SmartList.Attributes;
using Hogon.Store.Models.Entities.GoodsMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hogon.Store.UserInterface.Admin.Areas.GoodsMan.Models
{
	public class ServiceGoodsViewModel
	{
		/// <summary>
		/// 服务商品Id
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// 服务名称
		/// </summary>
		[Field("服务名称")]
		[TextSearch("服务名称")]
		public string GoodsName { get; set; }

		/// <summary>
		/// 商品编码
		/// </summary>
		[Field("商品编码")]
		[TextSearch("商品编码")]
		public string GoodsCode { get; set; }

		/// <summary>
		/// 价格
		/// </summary>
		[Field("价格")]
		public decimal SalePrice { get; set; }

		/// <summary>
		/// 商品分类名称
		/// </summary>
		public IEnumerable<string> GoodsTypeNames { get; set; }

		/// <summary>
		/// 商品分类名称
		/// </summary>
		[Field("商品分类")]
		//[TextSearch("商品分类")]
		public string DisplayGoodsTypeNames
		{
			get
			{
				return string.Join(",", GoodsTypeNames);
			}
		}

		/// <summary>
		/// 商品分类
		/// </summary>
		public IEnumerable<GoodsType> GoodsType { get; set; }

		/// <summary>
		/// 商品分类Id
		/// </summary>
		public ICollection<Guid> GoodsTypeId { get; set; }

		/// <summary>
		/// 服务商名称
		/// </summary>
		[Field("服务商名称")]
		public string Name { get; set; }
	}
}
