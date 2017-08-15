using AutoMapper;
using Hogon.Framework.Core.Common;
using Hogon.Framework.Core.Owin;
using Hogon.Framework.Core.UnitOfWork;
using Hogon.Store.Models.Dto.Common;
using Hogon.Store.Models.Dto.GoodsMan;
using Hogon.Store.Models.Entities.GoodsMan;
using Hogon.Store.Repositories.GoodsMan;
using System;
using System.Linq;

namespace Hogon.Store.Services.ApplicationServices.GoodsManContext
{
	public class ServiceGoodsApplicationService : BaseApplicationService
	{
		ServiceGoodsRepository serviceGoodsReps = new ServiceGoodsRepository();

		GoodsTypeRepository goodsTypeReps = new GoodsTypeRepository();

		/// <summary>
		/// 查询所有服务商品
		/// </summary>
		/// <returns></returns>
		public IQueryable<DtoServiceGoods> FindAllServiceGoods()
		{
			var serviceGoods = serviceGoodsReps.FindAll();
			var dtoServiceGoods = serviceGoods.ConvertTo<ServiceGoods, DtoServiceGoods>();
			return dtoServiceGoods;
		}

		/// <summary>
		/// 根据Id查询服务商品
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		public DtoServiceGoods FindServiceGoodsById(Guid Id)
		{
			var serviceGoods = serviceGoodsReps.FindBy(g => g.Id == Id).First();
			Mapper.Initialize(cfg => cfg.CreateMap<DtoServiceGoods, ServiceGoods>());
			var dtoServiceGoods = Mapper.Map<DtoServiceGoods>(serviceGoods);
			return dtoServiceGoods;
		}

		/// <summary>
		/// 根据ServiceName查询是否重复
		/// </summary>
		/// <param name="dtoServiceGoods"></param>
		/// <returns></returns>
		public int FindGoodsServiceByServiceName(DtoServiceGoods dtoServiceGoods)
		{
			var serviceGoods = serviceGoodsReps.FindBy(g => g.GoodsName == dtoServiceGoods.GoodsName);
			return serviceGoods.Count();
		}

		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="dtoServiceGoods"></param>
		/// <returns></returns>
		public Guid SaveGoodsService(DtoServiceGoods dtoServiceGoods)
		{
			if (dtoServiceGoods.Id == new Guid())
			{
				//Id为空时进行添加

				Mapper.Initialize(cfg => cfg.CreateMap<DtoServiceGoods, ServiceGoods>());
				var serviceGoods = Mapper.Map<ServiceGoods>(dtoServiceGoods);

				serviceGoods.Id = Guid.NewGuid();
				//创建人创建时间，修改人修改时间
				serviceGoods.CreatePerson = UserState.Current.UserName;
				serviceGoods.CreateTime = DateTime.Now;
				serviceGoods.UpdatePerson = UserState.Current.UserName;
				serviceGoods.UpdateTime = DateTime.Now;

				//var goodsType = goodsTypeReps.FindBy(p => p.Id == dtoServiceGoods.GoodsTypeId).First();

				serviceGoodsReps.Add(serviceGoods);

				Commit();
				return serviceGoods.Id;
			}
			else
			{
				//Id不为空时进行修改
				var serviceGoodsData = serviceGoodsReps.FindBy(p => p.Id == dtoServiceGoods.Id).First();

				Mapper.Initialize(cfg => cfg.CreateMap<DtoServiceGoods, ServiceGoods>());
				var serviceGoods = Mapper.Map<ServiceGoods>(dtoServiceGoods);

				//创建人创建时间，修改人修改时间
				serviceGoods.CreatePerson = serviceGoodsData.CreatePerson;
				serviceGoods.CreateTime = serviceGoodsData.CreateTime;
				serviceGoods.UpdatePerson = UserState.Current.UserName;
				serviceGoods.UpdateTime = DateTime.Now;

				Mapper.Initialize(cfg => cfg.CreateMap<DtoServiceGoods, ServiceGoods>());
				Mapper.Map(serviceGoods, serviceGoodsData);
				Commit();
				return serviceGoods.Id;
			}
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="Id"></param>
		public void Remove(Guid Id)
		{
			var serviceGoods = goodsTypeReps.FindBy(p => p.Id == Id).First();
			goodsTypeReps.Remove(serviceGoods);
		}
	}
}
