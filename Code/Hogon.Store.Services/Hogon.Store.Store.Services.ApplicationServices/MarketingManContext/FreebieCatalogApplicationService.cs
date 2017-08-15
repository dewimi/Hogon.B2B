using AutoMapper;
using Hogon.Framework.Core.Owin;
using Hogon.Framework.Core.UnitOfWork;
using Hogon.Store.Models.Dto.MarketingMan;
using Hogon.Store.Models.Entities.MarketingMan;
using Hogon.Store.Repositories.MarketingMan;
using Hogon.Store.Services.DomainServices.SecurityContext;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hogon.Framework.Core.Common;

namespace Hogon.Store.Services.ApplicationServices.MarketingManContext
{
 public   class FreebieCatalogApplicationService : BaseApplicationService
    {
        FreebieCatalogRepository freebieCatalogRepository = new FreebieCatalogRepository();
        AuthorizationDomainService _authorizationService = new AuthorizationDomainService();
        Md5Encryptor _encryptor = new Md5Encryptor();

        public FreebieCatalogApplicationService()
        {

        }

        /// <summary>
        /// 保存赠品分类信息
        /// </summary>
        /// <param name="dtoFreebie">赠品分类信息</param>
        public void SaveFreebie(DtoFreebieCatalog dtoFreebie)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DtoFreebieCatalog, FreebieCatalog>());
            if (dtoFreebie.Id != Guid.Empty)
            {
                DeleteFreebie(dtoFreebie.Id);
                //var freebie = freebieCatalogRepository.FindBy(t => dtoFreebie.Id == t.Id).First();
                var freebie = Mapper.Map<FreebieCatalog>(dtoFreebie);
                freebie.Sort = dtoFreebie.Sort;
                freebie.FreebieCatalogName = dtoFreebie.FreebieCatalogName;
                freebieCatalogRepository.Add(freebie);
            }
            else
            {
                var freebie = Mapper.Map<FreebieCatalog>(dtoFreebie);
                freebie.Sort = dtoFreebie.Sort;
                freebie.FreebieCatalogName = dtoFreebie.FreebieCatalogName;
                freebieCatalogRepository.Add(freebie);
            }

            //freebieCatalogRepository.Add(freebie);

            try
            {
                Commit();
            }
            catch (DbEntityValidationException e)
            {
                throw;
            }
        }

        /// <summary>
        /// 根据Id获取赠品分类信息
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public FreebieCatalog FindFreebie(Guid id)
        {
            var freebie = freebieCatalogRepository.FindBy(t => t.Id == id).First();
            return freebie;
        }

        /// <summary>
        /// 删除该赠品分类
        /// </summary>
        /// <param name="Id"></param>
        public void DeleteFreebie(Guid Id)
        {
            //var Product = productReps.FindBy(t => t.Id == ProductId).First();

            //productReps.Remove(Product);
            //Commit();
            var freebie = freebieCatalogRepository.FindBy(t => t.Id == Id).First();
            freebieCatalogRepository.Remove(freebie);
            Commit();
        }


        /// <summary>
        /// 查询FreebieCatalog整张表
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoFreebieCatalog> FindAllFreebie()
        {
            var freebieRes = freebieCatalogRepository.FindAll();
            var dtoFreebieRes = freebieRes.ConvertTo<FreebieCatalog,DtoFreebieCatalog>();
       
            return dtoFreebieRes;
        }


    }
}
