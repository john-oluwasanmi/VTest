using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V.Test.Web.App.Entities.Interface;

using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;
using V.Test.Web.App.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace V.Test.Web.App.Repository
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        protected TEntity Entity { get; set; } = new TEntity();
        protected int SkippedDbRecordSize { get => MaxPageSize * (CurrentPageNumber - 1); }
        protected short MaxPageSize { get => ConfigSetting.GetValue<short>("MaxPageSize"); }

        protected ILogger<TEntity> VLogger { get; set; }

        protected readonly IConfiguration ConfigSetting;


        protected readonly DbContextOptionsBuilder<VTestsContext> OptionsBuilder;
        protected readonly VTestsContext DatabaseContext;

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

        protected virtual void CommonExceptionThrow(TEntity entity, Exception ex)
        {
            LogError(ex, entity);
            throw ex;
        }

        protected RepositoryBase(IConfiguration configuration, ILogger<TEntity> logger)
        {
            ConfigSetting = configuration;
            VLogger = logger;

            OptionsBuilder = new DbContextOptionsBuilder<VTestsContext>();
            OptionsBuilder.UseSqlServer(configuration.GetConnectionString(""));

        }
        public virtual async Task<long> AddAsync(TEntity entity)
        {
            try
            {
                using (var context = new VTestsContext(OptionsBuilder.Options))
                {
                    await context.Set<TEntity>().AddAsync(entity);
                    await context.SaveChangesAsync();
                }

                var typeName = Entity?.GetType()?.Name;
                VLogger.LogInformation($" Successfully Added {typeName}'s Id: '{entity?.Id} ");

                return entity.Id;
            }
            catch (Exception ex)
            {
                LogError(ex, entity);

                throw ex;
            }
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            try
            {
                using (var context = new VTestsContext(OptionsBuilder.Options))
                {

                    var query = context.Set<TEntity>().AsQueryable();
                    var dd = await query.ToListAsync();

                    var entity = await query.Where(x => x.Id == id && (x.IsDeleted == null || x.IsDeleted == false))
                                        .SingleOrDefaultAsync();

                    var typeName = Entity?.GetType()?.Name;
                    VLogger.LogInformation($" Successfully retrieved {typeName} with the Id: '{entity?.Id} ");

                    return entity;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, null, id);

                throw ex;
            }
        }
        public virtual async Task<List<TEntity>> ListAsync(int pageNumber)
        {
            try
            {
                CurrentPageNumber = pageNumber;

                using (var context = new VTestsContext(OptionsBuilder.Options))
                {
                    List<TEntity> result = await context.Set<TEntity>().AsNoTracking()
                                                .Where(e => e.IsDeleted == false || e.IsDeleted == null)
                                                .OrderBy(r => r.Id)
                                                .Skip(SkippedDbRecordSize)
                                                .Take(MaxPageSize)
                                                .ToListAsync();

                    var typeName = Entity?.GetType()?.Name;
                    VLogger.LogInformation($" Successfully retrieved {typeName}'s List with page number {pageNumber} ");
                    CurrentPageNumber = 0;

                    return result;
                }
            }
            catch (SqlNullValueException s)
            {
                LogError(s, null, pageNumber);

                throw s;
            }
            catch (Exception ex)
            {
                LogError(ex, null, pageNumber);

                throw ex;
            }
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            TEntity newEntity = new TEntity();
            try
            {
                using (var context = new VTestsContext(OptionsBuilder.Options))
                {
                    context.Entry(entity).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }

                var typeName = Entity?.GetType()?.Name;
                VLogger.LogInformation($" Successfully Updated {typeName}'s Id: '{entity?.Id} ");
            }
            catch (Exception ex)
            {
                LogError(ex, entity);

                throw ex;
            }
        }



        public virtual async Task DeleteAsync(TEntity entity)
        {

            try
            {
                entity.IsDeleted = true;
                await UpdateAsync(entity);

                var typeName = Entity?.GetType()?.Name;
                VLogger.LogInformation($" Successfully Deleted {typeName}'s Id: '{entity?.Id} ");

            }
            catch (Exception ex)
            {
                LogError(ex, entity);

                throw ex;
            }
        }


        public virtual async Task<TEntity> GetAsync(int id, params string[] includes)
        {
            try
            {
                using (var context = new VTestsContext(OptionsBuilder.Options))
                {
                    var query = context.Set<TEntity>().AsQueryable();

                    if (includes != null)
                    {
                        foreach (var include in includes)
                        {
                            query = query.Include(include);
                        }
                    }

                    TEntity entity = await query.SingleOrDefaultAsync(x => x.Id == id
                                                             && (x.IsDeleted == false || x.IsDeleted == null));

                    var typeName = Entity?.GetType()?.Name;
                    VLogger.LogInformation($" Successfully retrieved {typeName} with the Id: '{entity?.Id} ");

                    return entity;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, includes, id);

                throw ex;
            }
        }

        public List<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, int currentPageNumber)
        {
            try
            {
                CurrentPageNumber = currentPageNumber;
                using (var context = new VTestsContext(OptionsBuilder.Options))
                {
                    List<TEntity> query = context.Set<TEntity>()
                                                           .Where(predicate)
                                                           .Where(e => e.IsDeleted == false || e.IsDeleted == null)
                                                           .OrderBy(r => r.Id)
                                                           .Skip(SkippedDbRecordSize)
                                                           .Take(MaxPageSize)
                                                           .ToList();


                    var typeName = Entity?.GetType()?.Name;
                    VLogger.LogInformation($" Successfully completed Find By {predicate} for {typeName} with page number {currentPageNumber} ");

                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, predicate, currentPageNumber);

                throw ex;
            }
        }





















        public virtual List<TEntity> FindBy(
            int page,
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            try
            {
                using (var context = new VTestsContext(OptionsBuilder.Options))
                {
                    CurrentPageNumber = page;

                    IQueryable<TEntity> query = context.Set<TEntity>();

                    if (filter != null)
                    {
                        query = query.Where(filter);
                    }

                    if (orderBy != null)
                    {
                        query = orderBy(query);
                    }

                    var typeName = Entity?.GetType()?.Name;
                    VLogger.LogInformation($" Successfully completed Find By {filter} for {typeName} with page number {page} ");

                    return query.Where(e => e.IsDeleted == false || e.IsDeleted == null)
                                 .Skip(SkippedDbRecordSize)
                                 .Take(MaxPageSize).ToList();
                }
            }
            catch (Exception ex)
            {
                LogError(ex, filter, page);

                throw ex;
            }
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            using (var context = new VTestsContext(OptionsBuilder.Options))
            {
                var isExist = context.Set<TEntity>()
                                     .Where(e => e.IsDeleted == false || e.IsDeleted == null)
                                     .Any(predicate);

                var typeName = Entity?.GetType()?.Name;
                VLogger.LogInformation($" Successfully check if {typeName} exists ");

                return isExist;
            }
        }
        public virtual int Counts()
        {
            try
            {
                using (var context = new VTestsContext(OptionsBuilder.Options))
                {
                    var counts = context.Set<TEntity>()
                                        .Where(e => e.IsDeleted == false || e.IsDeleted == null)
                                        .Count();

                    var typeName = Entity?.GetType()?.Name;
                    VLogger.LogInformation($" Successfully retrieved {typeName} count");

                    return counts;
                }
            }
            catch (Exception ex)
            {
                LogError(ex);

                throw ex;
            }
        }

        public virtual int Counts(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                using (var context = new VTestsContext(OptionsBuilder.Options))
                {
                    int counts = context.Set<TEntity>()
                                        .Where(predicate)
                                         .Where(e => e.IsDeleted == false || e.IsDeleted == null)
                                        .Count();

                    var typeName = Entity?.GetType()?.Name;
                    VLogger.LogInformation($" Successfully retrieved {typeName} count");

                    return counts;
                }
            }
            catch (Exception ex)
            {
                LogError(ex, predicate);

                throw ex;
            }
        }

        protected virtual void LogError(Exception ex, object data = null, int? intValue = null)
        {
            var callStack = new StackFrame(1, true);

            var exceptionMessage = ex?.Message;
            var innerExceptioMessage = ex?.InnerException?.Message;
            var exceptionStackTrack = ex?.StackTrace;
            var methodname = callStack?.GetMethod()?.Name;
            var filename = callStack?.GetMethod()?.DeclaringType?.FullName;
            var exceptionLogTime = DateTime.UtcNow;

            var serialisedData = JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            VLogger.LogError($"Exception Message: {exceptionMessage}, Inner Exception Message: {innerExceptioMessage} , Failed Method: {methodname}," +
                $" File Name: {filename}, Error Data: {serialisedData}, Integer Parameter: {intValue}, Time: {exceptionLogTime} ");
        }
    }
}