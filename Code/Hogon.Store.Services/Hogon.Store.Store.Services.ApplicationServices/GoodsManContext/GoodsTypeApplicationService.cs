using AutoMapper;
using Hogon.Framework.Core.Common;
using Hogon.Framework.Core.Owin;
using Hogon.Framework.Core.UnitOfWork;
using Hogon.Store.Models.Dto.Common;
using Hogon.Store.Models.Dto.GoodsMan;
using Hogon.Store.Models.Entities.GoodsMan;
using Hogon.Store.Repositories.GoodsMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogon.Store.Services.ApplicationServices.GoodsManContext
{
	public class GoodsTypeApplicationService : BaseApplicationService
	{
		GoodsTypeRepository goodsTypeReps = new GoodsTypeRepository();

		ProductTypeRepository productTypeReps = new ProductTypeRepository();

		ProductTypeCategoryRepository productTypeCategoryReps = new ProductTypeCategoryRepository();

		/// <summary>
		/// 查询所有商品分类
		/// </summary>
		/// <returns></returns>
		public IQueryable<DtoGoodsType> FindAllGoodsType()
		{
			var goodsType = goodsTypeReps.FindAll().OrderBy(t => t.UpdateTime);

			var dtoGoodsType = goodsType.Select(m => new DtoGoodsType()
			{
				Id = m.Id,
				ParentId = m.ParentId,
				Name = m.Name,
				CreatePerson = m.CreatePerson,
				CreateTime = m.CreateTime,
				IsEnabled = m.IsEnabled,
				Order = m.Order,
				UpdatePerson = m.UpdatePerson,
				UpdateTime = m.UpdateTime,
				Describe = m.Describe,
				ProductTypeId = m.ProductType.Id,
			});

			return dtoGoodsType;
		}

		/// <summary>
		/// 查询所有非三级商品分类
		/// </summary>
		/// <returns></returns>
		public IQueryable<DtoGoodsType> FindGoodsTypeOutThreeNode()
		{
			var goodsType = goodsTypeReps.FindAll().Where(p =>(( p.ParentId == null  )||(p.Parent.ParentId==null)) );
			
		   var dtoGoodsType = goodsType.ConvertTo<GoodsType, DtoGoodsType>();

			return dtoGoodsType;
		}

		/// <summary>
		/// 查询所有第三级商品分类
		/// </summary>
		/// <returns></returns>
		public IQueryable<DtoGoodsType> FindGoodsTypeThreeNode()
		{
			var goodsType = goodsTypeReps.FindAll().Where(p => (p.Parent.Parent.Parent == null) && (p.ParentId != null) && (p.Parent.ParentId != null));


			var dtoGoodsTpye = goodsType.ConvertTo<GoodsType, DtoGoodsType>();

			return dtoGoodsTpye;
		}

		/// <summary>
		/// 生成Tree
		/// </summary>
		/// <param name="goodsTypes"></param>
		/// <param name="dtoGoodsType"></param>
		/// <param name="dtoGoodsTypeData"></param>
		/// <returns></returns>
		public List<DtoTreeNode> CreateNodeList(IQueryable<DtoGoodsType> goodsTypes, IQueryable<DtoGoodsType> dtoGoodsType
			, IQueryable<DtoGoodsType> dtoGoodsTypeData)
		{

			List<DtoTreeNode> nodeList = new List<DtoTreeNode>();
			var parents = goodsTypes.Where(m => m.ParentId == null);
			if (parents != null)
			{
				// 遍历一级菜单
				#region
				foreach (var parent in parents)
				{
					var parentNode = new DtoTreeNode
					{
						Id = parent.Id,
						ParentId = null,
						text = parent.Name,
					};

					// 根据一级菜单Id查找二级菜单
					var children = dtoGoodsType.Where(m => m.ParentId == parent.Id);
					if (children.Count() > 0)
					{
						var childrenNodes = children.Select(m => new DtoTreeNode
						{
							Id = m.Id,
							ParentId = m.ParentId,
							text = m.Name,
						});
						
						#region
						foreach (var childNode in childrenNodes)
						{
							//根据二级菜单查找三级菜单
							var three = dtoGoodsTypeData.Where(m => m.ParentId == childNode.Id);
							if (three.Count() > 0)
							{
									var threeNodes = three.Select(g => new DtoTreeNode
								{
									Id = g.Id,
									ParentId = g.ParentId,
									text = g.Name,
								});
								childNode.nodes = threeNodes.ToList();
							}
							parentNode.nodes.Add(childNode);
						}
						#endregion
					}
					nodeList.Add(parentNode);
				}
				#endregion
				return nodeList;
			}
			return null;
		}

		/// <summary>
		/// 根据Id查询商品分类
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public DtoGoodsType FindGoodsTypeById(Guid Id)
		{
			var goodsType = goodsTypeReps.FindBy(t => t.Id == Id);
			var productType = goodsType.Select(s => s.ProductType).First();
			var dtoGoodsType = goodsType.ConvertTo<GoodsType, DtoGoodsType>().First();
			dtoGoodsType.ProductTypeName = productType.TypeName;
			dtoGoodsType.ProductTypeId = goodsType.First().ProductType.Id;
			if (dtoGoodsType.ParentId != null)
			{
				dtoGoodsType.ParentName = goodsTypeReps.FindBy(t => t.Id == dtoGoodsType.ParentId).First().Name;
			}
			return dtoGoodsType;
		}

		/// <summary>
		/// 查询Name是否存在
		/// </summary>
		/// <param name="dtoGoodsType"></param>
		/// <returns></returns>
		public int FindGoodsTypeByName(DtoGoodsType dtoGoodsType)
		{
			Mapper.Initialize(cfg => cfg.CreateMap<DtoGoodsType, GoodsType>());
			var goodsType = Mapper.Map<GoodsType>(dtoGoodsType);

			var goodsTypeData = goodsTypeReps.FindAll().Where(p => p.Name == goodsType.Name);

			var dtoGoodsTypeData = goodsTypeData.ConvertTo<GoodsType, DtoGoodsType>();

			return dtoGoodsTypeData.Count();
		}

		///<summary>
		///保存商品分类
		///</summary>
		///<param name ="dtoGoodsType"></param>
		///<returns></returns>
		public Guid SaveGoodsType(DtoGoodsType dtoGoodsType)
		{

			if (dtoGoodsType.Id == new Guid())
			{
				//id为空进行添加
				Mapper.Initialize(cfg => cfg.CreateMap<DtoGoodsType, GoodsType>());
				var goodsType = Mapper.Map<GoodsType>(dtoGoodsType);
				goodsType.Id = Guid.NewGuid();
				goodsType.CreateTime = DateTime.Now;
				goodsType.UpdateTime = DateTime.Now;
				goodsType.CreatePerson = UserState.Current.UserName;
				goodsType.UpdatePerson = UserState.Current.UserName;
				//找到对应的ProductType ,ProductTypeCategory
				var productType = productTypeReps.FindBy(p => p.Id == dtoGoodsType.ProductTypeId).First();
				productType.ProductTypeCategory = productTypeReps.FindBy(p => p.Id == dtoGoodsType.ProductTypeId).Select(m => m.ProductTypeCategory).First();
				goodsType.ProductType = productType;

				goodsTypeReps.Add(goodsType);

				Commit();
				return goodsType.Id;
			}
			else
			{
				//id 不为空进行修改
				var goodsTypeData = goodsTypeReps.FindBy(t => t.Id == dtoGoodsType.Id).First();
				dtoGoodsType.CreatePerson = goodsTypeData.CreatePerson;
				dtoGoodsType.CreateTime = goodsTypeData.CreateTime;

				dtoGoodsType.UpdatePerson = UserState.Current.UserName;
				dtoGoodsType.UpdateTime = DateTime.Now;
				Mapper.Initialize(cfg => cfg.CreateMap<DtoGoodsType, GoodsType>());
				Mapper.Map(dtoGoodsType, goodsTypeData);
				Commit();
				return dtoGoodsType.Id;
			}
		}

		/// <summary>
		/// 删除商品分类
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public void RemoveGoodsType(Guid Id)
		{
			var goodsType = goodsTypeReps.FindBy(t => t.Id == Id).First();

			var goodsTypeData = goodsTypeReps.FindBy(t => t.ParentId == goodsType.Id);
			//当存在子节点时不删除
			if (goodsTypeData.Count() > 0)
			{
				foreach (var item in goodsTypeData)
				{
					goodsTypeReps.Remove(item);
				}
			}
			goodsTypeReps.Remove(goodsType);
			Commit();
		}
	}
}