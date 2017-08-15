using AutoMapper;
using Hogon.Framework.Core.Common;
using Hogon.Framework.Core.Owin;
using Hogon.Framework.Core.UnitOfWork;
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
    public class BrandApplicationService : BaseApplicationService
    {
        BrandRepository brandReps = new BrandRepository();

        GoodsTypeRepository goodsTypeReps = new GoodsTypeRepository();

        /// <summary>
        /// 查询所有品牌
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoBrand> FindAllBrand()
        {
            var brand = brandReps.FindAll();

            var dtoBrands = brand.Select(s => new DtoBrand()
            {
                Id = s.Id,
                BrandAlias = s.BrandAlias,
                BrandLogo = s.BrandLogo,
                BrandName = s.BrandName,
                CreatePerson = s.CreatePerson,
                CreateTime = s.CreateTime,
                GoodsTypeNames = s.Rela_Brand_GoodsType.Select(m => m.GoodsType.Name),
                UpdatePerson = s.UpdatePerson,
                UpdateTime = s.UpdateTime,
                GoodsTypeIds = s.Rela_Brand_GoodsType.Select(m => m.GoodsType.Id),
                Nation = s.Nation,
                Country = s.Country,
                City = s.City,
            });

            return dtoBrands;
        }

        /// <summary>
        /// 根据品牌名，品牌Id查询是否重复
        /// </summary>
        /// <param name="dtoBrand"></param>
        /// <returns></returns>
        public int FindBrandByBrandName(DtoBrand dtoBrand)
        {
            int count = 0;
            if (dtoBrand.Id == new Guid())
            {
                count = brandReps.FindBy(f => f.BrandName == dtoBrand.BrandName).Count(); ;
            }
            else
            {
                count = brandReps.FindBy(f => f.BrandName == dtoBrand.BrandName && f.Id == dtoBrand.Id).Count();
            }
            return count;
        }

        /// <summary>
        /// 根据Id查询品牌
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DtoBrand FindBrandById(Guid Id)
        {
            var brand = brandReps.FindBy(f => f.Id == Id);

            var dtoBrand = brand.Select(s => new DtoBrand()
            {
                Id = s.Id,
                BrandAlias = s.BrandAlias,
                BrandLogo = s.BrandLogo,
                BrandName = s.BrandName,
                CreatePerson = s.CreatePerson,
                CreateTime = s.CreateTime,
                GoodsTypeNames = s.Rela_Brand_GoodsType.Select(m => m.GoodsType.Name),
                UpdatePerson = s.UpdatePerson,
                UpdateTime = s.UpdateTime,
                GoodsTypeIds = s.Rela_Brand_GoodsType.Select(m => m.GoodsType.Id),
                Nation = s.Nation,
                Country = s.Country,
                City = s.City,
            }).First();

            return dtoBrand;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="dtoBrand"></param>
        /// <returns></returns>
        public Guid SaveBrand(DtoBrand dtoBrand)
        {
            if (dtoBrand.Id == new Guid())
            {
                //Id为空进行添加
                Mapper.Initialize(cfg => cfg.CreateMap<DtoBrand, Brand>());
                var brand = Mapper.Map<Brand>(dtoBrand);

                if (dtoBrand.GoodsTypeIds.Count() > 0)
                {
                    foreach (var item in dtoBrand.GoodsTypeIds)
                    {
                        Rela_Brand_GoodsType rela_Brand_GoodsType = new Rela_Brand_GoodsType();

                        var goodsType = goodsTypeReps.FindBy(f => f.Id == item).First();
                        rela_Brand_GoodsType.GoodsType = goodsType;
                        rela_Brand_GoodsType.Brand = brand;
                        rela_Brand_GoodsType.Id = Guid.NewGuid();
                        brand.Rela_Brand_GoodsType.Add(rela_Brand_GoodsType);
                    }
                }
                brandReps.Add(brand);
                Commit();
                return brand.Id;
            }
            else
            {
                //Id不为空进行修改
                var brand = brandReps.FindBy(p => p.Id == dtoBrand.Id).First();

                Mapper.Initialize(cfg => cfg.CreateMap<DtoBrand, Brand>());
                Mapper.Map<DtoBrand, Brand>(dtoBrand, brand);

                //删除商品-商品分类关系表对应数据
                if (brand.Rela_Brand_GoodsType.Count > 0)
                {
                    var rela_Brand_GoodsType = brandReps.FindBy(s => s.Id == brand.Id).SelectMany(m => m.Rela_Brand_GoodsType);
                    foreach (var item in rela_Brand_GoodsType)
                    {
                        brandReps.DeleteRela_Brand_GoodsType(item);
                    }
                }
                //重建关系
                if (dtoBrand.GoodsTypeIds.Count() > 0)
                {
                    foreach (var item in dtoBrand.GoodsTypeIds)
                    {
                        Rela_Brand_GoodsType rela_Brand_GoodsType = new Rela_Brand_GoodsType();
                        rela_Brand_GoodsType.Brand = brand;

                        var goodsType = goodsTypeReps.FindBy(p => p.Id == item).First();

                        rela_Brand_GoodsType.GoodsType = goodsType;
                        rela_Brand_GoodsType.Id = Guid.NewGuid();

                        brand.Rela_Brand_GoodsType.Add(rela_Brand_GoodsType);
                    }
                }

                Commit();
                return brand.Id;

            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        public void Remove(Guid Id)
        {
            var brand = brandReps.FindBy(s => s.Id == Id).First();
            brandReps.Remove(brand);
            Commit();
        }
    }
}
