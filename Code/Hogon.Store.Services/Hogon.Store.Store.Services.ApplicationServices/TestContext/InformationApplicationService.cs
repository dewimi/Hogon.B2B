using AutoMapper;
using Hogon.Store.Models.Entities.CommonContext;
using Hogon.Store.Models.Dto.TestContext;
using Hogon.Store.Repositories.CommonContext;
using System;
using System.Linq;
using System.Collections.Generic;
using Hogon.Framework.Core.UnitOfWork;
using Hogon.Framework.Core.Common;

namespace Hogon.Store.Services.ApplicationServices.TestContext
{
    public class InformationApplicationService : BaseApplicationService
    {
        IRepository<Information> informationReps;

        public InformationApplicationService()
        {
            informationReps = new InformationRepository();
        }

        /// <summary>
        /// 根据产品ID查询数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DtoInformation GetInformationById(int id)
        {

            var information = informationReps.FindBy(i => i.Id == id).FirstOrDefault();
            if (information == null)
            {
                throw new ArgumentException();
            }

            Mapper.Initialize(cfg => cfg.CreateMap<Information, DtoInformation>());
            DtoInformation dtoinformation = new DtoInformation();
            Mapper.Map(information, dtoinformation);

            return dtoinformation;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="dtoinformation"></param>
        public void UpdateInformation(DtoInformation dtoinformation)
        {
            var information = informationReps.FindBy(u => u.Id == 1).FirstOrDefault();

            if (information == null)
            {
                throw new ArgumentException();
            }

            information.Pname = dtoinformation.Pname;
            information.Ename = dtoinformation.Ename;
            information.Brand = dtoinformation.Brand;
            information.ProductModel = dtoinformation.ProductModel;
            information.ProducingArea = dtoinformation.ProducingArea;
            information.Quote = dtoinformation.Quote;
            information.UploadName = dtoinformation.UploadName;

            Commit();
        }
        
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="pname"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public List<DtoInquiry> GetData(string pname, int pageSize, int pageIndex)
        {

            List<DtoInquiry> dtoInformations = new List<DtoInquiry>();
            var informations = informationReps.FindAll();
            if (!string.IsNullOrEmpty(pname))
            {
                informations = informations.Where(p => p.Pname == pname).Select(p => p);
            }
            informations = informations.OrderBy(p => p.Id).Select(p => p);
            Mapper.Initialize(cfg => cfg.CreateMap<Information, DtoInquiry>());

            foreach (var information in informations)
            {
                DtoInquiry dtoinformation = new DtoInquiry();
                Mapper.Map(information, dtoinformation);
                dtoInformations.Add(dtoinformation);
            }

            return dtoInformations;
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoInformation> FindAll()
        {
            var informations = informationReps.FindAll();
            informations = informations.OrderBy(p => p.Id).Select(p => p);

            var dtoInformations = informations.ConvertTo<Information, DtoInformation>();

            return dtoInformations;
        }
        /// <summary>
        /// 添加信息
        /// </summary>
        public void AddInstrument(DtoInformation dtoinformation)
        {
            Information information = new Information()
            {
                Pname = dtoinformation.Pname,
                Ename = dtoinformation.Ename,
                Brand = dtoinformation.Brand,
                ProductModel = dtoinformation.ProductModel,
                ProducingArea = dtoinformation.ProducingArea,
                Quote = dtoinformation.Quote,
                UploadName = dtoinformation.UploadName,
                OriginalFileName = dtoinformation.OriginalFileName,
            };

            informationReps.Add(information);

            Commit();
        }
    }
}