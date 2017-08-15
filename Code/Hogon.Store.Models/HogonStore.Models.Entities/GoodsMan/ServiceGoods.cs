using System;
using System.Collections.Generic;

namespace Hogon.Store.Models.Entities.GoodsMan
{
	/// <summary>
	/// 服务商品
	/// </summary>
	public class ServiceGoods : Goods
	{
		/// <summary>
		/// 产品商品集合
		/// </summary>
		public ProductGoods ProductGoodss { get; set; }
		/// <summary>
		/// 子商品集合
		/// </summary>
		public override ICollection<Goods> ChildrenGoods { get; set; }
	}
}