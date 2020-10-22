using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using V.Test.Web.App.BusinessService.Interface;
using V.Test.Web.App.Core;
using V.Test.Web.App.Entities.Interface;
using V.Test.Web.App.ViewModels.Interface;

namespace V.Test.Web.App.Controllers
{
    public abstract class VTestControllerBase<TviewModel, TEntity, TBusinessServiceManager>
        : VTestControllerBase
        where TEntity : class, IEntity, new()
        where TviewModel : class, IViewModel, new()
        where TBusinessServiceManager : IBusinessService<TEntity>
    {
        protected TEntity Entity { get; set; } = new TEntity();
        protected TviewModel ViewModel { get; set; } = new TviewModel();

        protected readonly TBusinessServiceManager BusinessServiceManager;
        protected readonly IMapper IMapper;

        public VTestControllerBase(ILogger<TEntity> logger
                                    , TBusinessServiceManager businessService
                                    , IConfiguration configuration)
        : base(logger, configuration)
        {
            BusinessServiceManager = businessService;
            IMapper = ConfigureMapper().CreateMapper();
        }

        public virtual async Task AddAsync(TviewModel item)
        {
            TEntity result = MapViewModelToEntity(item);

            SetAuditInformation(result, false);

           await BusinessServiceManager.AddAsync(result);
        }



        protected virtual async Task<TviewModel> GetAsync(int id)
        {
            var entity = await BusinessServiceManager.GetAsync(id);

            TviewModel result = ConvertEntityToViewModel(entity);

            return result;
        }

        protected virtual async Task<List<TviewModel>> ListAsync(int pageNumber )
        {
            var entities = await BusinessServiceManager.ListAsync(pageNumber);

            var result = IMapper.Map<List<TEntity>, List<TviewModel>>(entities);

            return result;
        }

        protected virtual async Task UpdateAsync(TviewModel item)
        {

            var entity = await TrackedEntityForUpdateAsync(item);

            SetUpdateAuditInformation(entity);

            await BusinessServiceManager.UpdateAsync(entity);
        }

        protected virtual async Task DeleteAsync(TviewModel item)
        {
          
            TEntity result = MapViewModelToEntity(item);
            result.IsDeleted = true;

            await BusinessServiceManager.DeleteAsync(result); ;
        }

        protected async Task<TEntity> TrackedEntityForUpdateAsync(TviewModel tviewModel)
        {
            TEntity entity = await BusinessServiceManager.GetAsync(tviewModel.Id);
            return SetAuditInformation(tviewModel, entity);
        }

        protected TEntity SetAuditInformation(TviewModel tviewModel, TEntity entity)
        {
            // Store the created by audit information
            var createdDate = entity.CreatedOn;
            IMapper.Map(tviewModel, entity);

            // Set the created by audit information as it may be lost during mapping
            entity.CreatedOn = createdDate;

            return entity;
        }

        protected virtual List<TviewModel> ConvertEntityToViewModel(List<TEntity> entities)
        {
            var result = IMapper.Map<List<TEntity>, List<TviewModel>>(entities);
            return result;
        }

        protected virtual TviewModel ConvertEntityToViewModel(TEntity entity)
        {
            return IMapper.Map<TEntity, TviewModel>(entity);
        }

        protected virtual List<TOtherviewModel> ConvertEntityToViewModel<TOtherEntity, TOtherviewModel>(List<TOtherEntity> entities)
            where TOtherEntity : class, IEntity, new()
            where TOtherviewModel : class, IViewModel, new()
        {
            var result = IMapper.Map<List<TOtherEntity>, List<TOtherviewModel>>(entities);
            return result;
        }

        protected TOtherviewModel ConvertEntityToViewModel<TOtherEntity, TOtherviewModel>(TOtherEntity entity)
            where TOtherEntity : class, IEntity, new()
            where TOtherviewModel : class, IViewModel, new()
        {
            return IMapper.Map<TOtherEntity, TOtherviewModel>(entity);
        }

        protected virtual TEntity MapViewModelToEntity(TviewModel item)
        {
            return IMapper.Map<TviewModel, TEntity>(item);
        }

        protected virtual TOtherEntity MapViewModelToEntity<TOtherviewModel, TOtherEntity>(TOtherviewModel item)
             where TOtherEntity : class, IEntity, new()
            where TOtherviewModel : class, IViewModel, new()
        {
            return IMapper.Map<TOtherviewModel, TOtherEntity>(item);
        }

        protected DateTime GetCurrentDate()
        {
            var currentDateTime = DateTime.UtcNow;
            var date = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Second, DateTimeKind.Utc);
            return date;
        }

        protected virtual void SetUpdateAuditInformation(TEntity entity)
        {
            ProcessAuditInformation(entity);
        }

        protected virtual void SetUpdateAuditInformation<T>(T entity)
            where T : IEntity
        {
            ProcessAuditInformation(entity);
        }

        protected virtual void SetAuditInformation<T>(T entity, bool isUpdate = false)
            where T : IEntity
        {
            CommonSetAuditInformation(entity, isUpdate);
        }

        protected virtual void SetAuditInformation(TEntity entity, bool isUpdate = false)
        {
            CommonSetAuditInformation(entity, isUpdate);
        }

        private void CommonSetAuditInformation<T>(T entity, bool isUpdate)
            where T : IEntity
        {
            var date = GetCurrentDate();

            if (entity != null)
            {
                if (!isUpdate)
                {
                    entity.CreatedOn = date;
                }
                else
                {
                    entity.ModifiedOn = date;
                }
            }
        }

        private MapperConfiguration ConfigureMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TviewModel, TEntity>(MemberList.None);
                cfg.CreateMap<TEntity, TviewModel>(MemberList.None);


                cfg.AddProfile<VProfile>();
            });

            return config;
        }

        private void ProcessAuditInformation<T>(T entity)
          where T : IEntity
        {
            var date = GetCurrentDate();

            if (entity?.CreatedOn == DateTime.MinValue)
                entity.CreatedOn = date;

            entity.ModifiedOn = date;
        }

         
    }


    public abstract class VTestControllerBase : Controller
    {

        protected readonly IConfiguration ConfigSettings;
        protected readonly ILogger VTestLogger;

        public VTestControllerBase(ILogger logger, IConfiguration configSetting)
        {
            VTestLogger = logger;
            this.ConfigSettings = configSetting;
        }

        protected void AddErrors(IdentityResult result)
        {

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}