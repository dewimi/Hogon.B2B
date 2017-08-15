using AutoMapper;
using Hogon.Framework.Core.Common;
using Hogon.Framework.Core.Owin;
using Hogon.Framework.Core.UnitOfWork;
using Hogon.Store.Models.Dto.GoodsMan;
using Hogon.Store.Models.Entities.GoodsMan;
using Hogon.Store.Repositories.Common;
using Hogon.Store.Repositories.GoodsMan;
using System;
using System.Linq;

namespace Hogon.Store.Services.ApplicationServices.GoodsManContext
{
    public class SpecTypeApplicationService : BaseApplicationService
    {
        SpecTypeRepository spectypeReps = new SpecTypeRepository();
        ProductTypeRepository productTypeReps = new ProductTypeRepository();
        SpecParameterTemplateRepository specParameterTemplateReps = new SpecParameterTemplateRepository();
        FileUploadRepository fileReps = new FileUploadRepository();

        /// <summary>
        /// 获取所有规格参数
        /// </summary>
        /// <returns></returns>
        public IQueryable<DtoSpecType> GetAllSpecTypes()
        {
            var specTypes = spectypeReps.FindAll();
            //var dtospecTypes = specTypes.ConvertTo
            //      <SpecType, DtoSpecType>();
            var dtospecTypes = specTypes.Select(m => new DtoSpecType()
            {
                Id = m.Id,
                SpecName = m.SpecName,
                SpecCatalog = m.SpecCatalog,
                SpecRemark = m.SpecRemark,
                SpecSecondName = m.SpecSecondName,
                DisplayName = m.DisplayName,
                DisplayMode = m.DisplayMode,
                ProductTypeId = m.ProductType.Id,
                CreatePerson = m.CreatePerson,
                CreateTime = m.CreateTime,
                UpdatePerson = m.UpdatePerson,
                UpdateTime = m.UpdateTime,
                

            });
            return dtospecTypes;
        }

        /// <summary>
        /// 保存规格类型
        /// </summary>
        /// <param name="dtoSpecType"></param>
        /// <returns></returns>
        public Guid SaveSpecType(DtoSpecType dtoSpecType, Guid productTypeId)
        {
            if (dtoSpecType.Id == new Guid())
            {
                //判断数据库是否存在
               var spec = spectypeReps.FindBy(m => m.SpecName == dtoSpecType.SpecName
               && m.ProductType.Id == productTypeId).FirstOrDefault();
                if (spec == null)
                {
                    //Id为空就进行添加操作
                    SpecType specType = new SpecType();
                    Mapper.Initialize(cfg => cfg.CreateMap<DtoSpecType, SpecType>());
                    var specTypeData = Mapper.Map<SpecType>(dtoSpecType);
                    specTypeData.ProductType = productTypeReps.FindBy(m => m.Id == productTypeId).First();

                    spectypeReps.Add(specTypeData);
                    Commit();
                    return specTypeData.Id;

                }
                else
                {
                    return new Guid();
                }
            }
            else
            {
                //Id不为空就进行修改操作
                var specType = spectypeReps.FindBy(i => i.Id == dtoSpecType.Id).First();
                dtoSpecType.UpdateTime = DateTime.Now;
                dtoSpecType.UpdatePerson = UserState.Current.UserName;
                Mapper.Initialize(cfg => cfg.CreateMap<DtoSpecType, SpecType>());
                Mapper.Map(dtoSpecType, specType);

                Commit();
                return specType.Id;
            }

        }

        /// <summary>
        /// 保存规格参数
        /// </summary>
        /// <param name="dtoSpecTypeParameter">规格参数</param>
        /// <param name="specTypeId">规格类型Id</param>
        public Guid SaveSpecParameter(DtoSpecTypeParameter dtoSpecTypeParameter, Guid specTypeId, Guid? fileId)
        {

            var specType = spectypeReps.FindBy(s => s.Id == specTypeId).First();
            var parameter = spectypeReps.FindAll().SelectMany(m => m.SpecParameterTemplate)
                .Where(s=>s.ParameterName == dtoSpecTypeParameter.ParameterName).FirstOrDefault();
            if (parameter == null)
            {
                //Id为空就进行添加操作
                if (dtoSpecTypeParameter.Id == new Guid()&& !string.IsNullOrEmpty(dtoSpecTypeParameter.ParameterName))
                {
                    SpecParameterTemplate specParameterTemplate = new SpecParameterTemplate();
                    specParameterTemplate.SpecType = spectypeReps.FindBy(s => s.Id == specTypeId).First();
                    specParameterTemplate.SpecType.ProductType = spectypeReps.FindBy(s =>
                    s.Id == specTypeId).Select(p => p.ProductType).First();
                    specParameterTemplate.ParameterName = dtoSpecTypeParameter.ParameterName;
                   
                    if (fileId != null)
                    {
                        specParameterTemplate.FileUpload = fileReps.FindBy(m => m.Id == fileId).First();
                    }
                    else
                    {
                        specParameterTemplate.FileUpload = null;

                    }
                    specType.SpecParameterTemplate.Add(specParameterTemplate);
                    Commit();
                    return specParameterTemplate.Id;

                }
                //编辑
                else if(dtoSpecTypeParameter.Id != new Guid() && !string.IsNullOrEmpty(dtoSpecTypeParameter.ParameterName))
                {
                    var specTypeParameter = spectypeReps.FindAll().SelectMany(m => m.SpecParameterTemplate)
                                     .Where(s => s.Id == dtoSpecTypeParameter.Id).FirstOrDefault();
                    if (specTypeParameter != null)
                    {
                        dtoSpecTypeParameter.SpecType = specType;
                        Mapper.Initialize(cfg => cfg.CreateMap<DtoSpecTypeParameter, SpecParameterTemplate>());
                        Mapper.Map(dtoSpecTypeParameter, specTypeParameter);
                    }

                    Commit();
                    return specType.Id;
                 
                }
                return new Guid();
            }
            else
            {
                throw new UserFriendlyException("数据已存在");
            }

        }

        /// <summary>
        /// 根据规格类型Id查询规格参数
        /// </summary>
        /// <param name="specTypeId">规格类型Id</param>
        /// <returns></returns>
        public IQueryable<DtoSpecTypeParameter> GetParametersById(Guid specTypeId)
        {

            var specTypeParameters = spectypeReps.FindBy(s => s.Id == specTypeId)
                .SelectMany(s => s.SpecParameterTemplate);

            var dtospecTypeParameters = specTypeParameters.ConvertTo
                <SpecParameterTemplate, DtoSpecTypeParameter>();

            return dtospecTypeParameters;
        }

        /// <summary>
        /// 根据规格类型Id查询相应数据
        /// </summary>
        /// <param name="specTypeId"></param>
        /// <returns></returns>
        public DtoSpecType GetSpecTypeBySpecTypeId(Guid specTypeId)
        {
            var specType = spectypeReps.FindBy(s => s.Id == specTypeId).First();
            specType.ProductType = spectypeReps.FindBy(m => m.Id == specTypeId)
                .Select(p => p.ProductType).First();
            Mapper.Initialize(cfg => cfg.CreateMap<SpecType, DtoSpecType>());
            var dtoSpecType = Mapper.Map<DtoSpecType>(specType);

            return dtoSpecType;
        }

        /// <summary>
        /// 删除规格类型
        /// </summary>
        /// <param name="specTypeId">规格类型Id</param>
        public void RemoveSpecType(Guid specTypeId)
        {
            SpecType specType = spectypeReps.FindBy(u => u.Id == specTypeId).FirstOrDefault();

            spectypeReps.Remove(specType);

            Commit();
        }

    }
}
