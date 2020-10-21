using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V.Test.Web.App.BusinessService.Interface;
using V.Test.Web.App.Entities.Interface;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using V.Test.Web.App.Repository.Interface;

namespace V.Test.Web.App.BusinessService
{
    public class BusinessServiceBase<TEntity, TRepositoryManager> : IBusinessService<TEntity>
       where TEntity : IEntity, new()
       where TRepositoryManager : IRepository<TEntity>
    {
        protected TEntity Entity { get; set; } = new TEntity();

        protected readonly TRepositoryManager RepositoryManager;
        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();

        private int _currentPageNumber { get; set; }

        public BusinessServiceBase(TRepositoryManager repository)
        {
            RepositoryManager = repository;
        }
        public virtual async Task<long> AddAsync(TEntity item)
        {
            CheckIfNull(item);

            if (item?.Id > 0)
            {
                var typeName = Entity?.GetType()?.Name;
                throw new Exception($"Invalid {typeName}Id");
            }

            var id = await RepositoryManager.AddAsync(item);
            return id;
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            ValidateId(id);
            var result = await RepositoryManager.GetAsync(id);

            return result;
        }

        public virtual async Task<TEntity> GetAsync(int id, params string[] includes)
        {
            ValidateId(id);
            var result = await RepositoryManager.GetAsync(id, includes);

            return result;
        }

        public virtual async Task<List<TEntity>> ListAsync(int pageNumber = 1)
        {
            ValidateId(pageNumber);
            var result = await RepositoryManager.ListAsync(pageNumber);
            return result;
        }

        public virtual async Task UpdateAsync(TEntity item)
        {
            CheckIfNull(item);
            ValidateId(item?.Id);

            await RepositoryManager.UpdateAsync(item);
        }

        public virtual async Task DeleteAsync(TEntity item)
        {
            CheckIfNull(item);
            ValidateId(item?.Id);
            ValidateId(item.IsDeleted);

            await RepositoryManager.DeleteAsync(item); ;
        }

        protected void CheckIfNull(TEntity item)
        {
            if (item == null)
            {
                var typeName = Entity?.GetType()?.Name;
                throw new NullReferenceException($"Invalid {typeName} item");
            }
        }

        protected void ValidateId(bool? IsDeleted, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string caller = "", [CallerMemberName] string memberName = "")
        {
            if (IsDeleted.HasValue && IsDeleted == false)
            {
                throw new Exception($"Invalid {Entity?.GetType()?.Name} parameter, method name:{caller}, class name: {memberName}, line number: {lineNumber}");
            }
        }
        protected void ValidateId(string id, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string caller = "", [CallerMemberName] string memberName = "")
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new Exception($"Invalid {Entity?.GetType()?.Name} parameter, method name:{caller}, class name: {memberName}, line number: {lineNumber}");
            }
        }

        protected virtual void ValidateId(long? id, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string caller = "", [CallerMemberName] string memberName = "")
        {
            if (id < 1)
            {
                throw new Exception($"Invalid {Entity?.GetType()?.Name} parameter, method name:{caller}, class name: {memberName}, line number: {lineNumber}");
            }
        }


        public List<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, int currentPageNumber)
        {
            return RepositoryManager.FindBy(predicate, currentPageNumber).ToList();
        }

        public virtual List<TEntity> FindBy(
            int page,
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            return RepositoryManager.FindBy(page, filter, orderBy).ToList();
        }

        public virtual IEnumerable<TEntity> FindBy(
            int page,
            Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, object>> sortExpression = null,
            bool isSortAscending = true)
        {
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = x => x.OrderBy(sortExpression);

            if (!isSortAscending)
            {
                orderBy = x => x.OrderByDescending(sortExpression);
            }

            return FindBy(page, filter, orderBy);
        }



        public int Counts(Expression<Func<TEntity, bool>> predicate)
        {
            return RepositoryManager.Counts(predicate);
        }

        public int Counts()
        {
            return RepositoryManager.Counts();
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return RepositoryManager.Exists(predicate);
        }
    }
}
