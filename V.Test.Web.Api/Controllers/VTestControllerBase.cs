using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using V.Test.Web.Api.BusinessService.Interface;
using V.Test.Web.App.Core;
using V.Test.Web.Api.Entities.Interface;
using V.Test.Web.App.ViewModels.Interface;

namespace V.Test.Web.Api.Controllers
{
    [Route("api/[controller]")]
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

        protected int CurrentPageNumber
        {
            get
            {
                if (_currentPageNumber < 1)
                {
                    _currentPageNumber = 1;
                }
                return _currentPageNumber;
            }
            set
            {
                _currentPageNumber = value;
            }
        }
        private int _currentPageNumber { get; set; }


        public VTestControllerBase(ILogger<TEntity> logger
                                    , TBusinessServiceManager businessService
                                    , IConfiguration configuration)
        : base(logger, configuration)
        {
            BusinessServiceManager = businessService;
            IMapper = ConfigureMapper().CreateMapper();
        }


        [HttpPost]
        protected virtual async Task<IActionResult> AddAsync(TviewModel item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }


                TEntity result = MapViewModelToEntity(item);

                SetAuditInformation(result);

                await BusinessServiceManager.AddAsync(result);

                var name = typeof(TEntity).Name;

                return Created($"http://localhost:5000/{name}/{result.Id}", result);

            }
            catch (Exception ex)
            {
                LogException(ex);
                return StatusCode(500);
            }
        }



        [HttpGet]
        protected virtual async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var entity = await BusinessServiceManager.GetAsync(id);

                TviewModel result = ConvertEntityToViewModel(entity);

                if (result == null)
                {
                    return NotFound(id);
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                LogException(ex);
                return StatusCode(500);
            }
        }

        [HttpGet]
        protected virtual async Task<IActionResult> ListAsync([FromQuery]int? pageNumber)
        {
            try
            {
                CurrentPageNumber = pageNumber ?? 0;

                var entities = await BusinessServiceManager.ListAsync(CurrentPageNumber) as List<TEntity>;

                CurrentPageNumber = 0;

                if (entities == null || !entities.Any())
                {
                    return NotFound();
                }

                var result = IMapper.Map<List<TEntity>, List<TviewModel>>(entities);

                return Ok(result);

            }
            catch (Exception ex)
            {
                LogException(ex);
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        protected virtual async Task<IActionResult> UpdateAsync(TviewModel item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }

                var entity = await TrackedEntityForUpdateAsync(item);

                if (entity == null)
                {
                    return NotFound(item.Id);
                }

                SetAuditInformation(entity, true);

                await BusinessServiceManager.UpdateAsync(entity);

                return Ok();

            }
            catch (Exception ex)
            {
                LogException(ex);
                return StatusCode(500);
            }
        }

        [HttpDelete]
        protected virtual async Task<IActionResult> DeleteAsync(TviewModel item)
        {
            try
            {

                var entity = await BusinessServiceManager.GetAsync(item.Id);

                if (entity == null)
                {
                    return NotFound(item.Id);
                }

                SetAuditInformation(entity, true);

                entity.IsDeleted = true;
                await BusinessServiceManager.DeleteAsync(entity);

                return Ok();

            }
            catch (Exception ex)
            {
                LogException(ex);
                return StatusCode(500);
            }
        }

        protected void LogException(Exception Exception)
        {
            var exceptionMessage = Exception?.Message;
            var exceptionStackTrack = Exception?.StackTrace;
            var innerException = Exception?.InnerException?.Message;
            var controllerName = RouteData?.Values["controller"]?.ToString();
            var actionName = RouteData?.Values["action"]?.ToString();
            var exceptionLogTime = DateTime.UtcNow;

            VTestLogger.LogError($"Message : {exceptionMessage}, StackTrack : {exceptionStackTrack}" +
                $",Controller : {controllerName}, Action : {actionName}, Inner Exception : {innerException}, Time : {exceptionLogTime} ");

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