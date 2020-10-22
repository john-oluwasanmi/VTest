using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using V.Test.Web.Api.Entities.Interface;


namespace V.Test.Web.Api.BusinessService.Interface
{
    public interface IBusinessService<T> where T : IEntity
    {
        Task<long> AddAsync(T item);

        Task<T> GetAsync(int id);

        Task<T> GetAsync(int id, params string[] includes);

        Task<List<T>> ListAsync(int pageNumber);

        Task UpdateAsync(T item);

        Task DeleteAsync(T entity);

        List<T> FindBy(Expression<Func<T, bool>> predicate, int currentPageNumber);

        List<T> FindBy(int page, Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);

        IEnumerable<T> FindBy(int page, Expression<Func<T, bool>> filter, Expression<Func<T, object>> sortExpression, bool isSortAscending);


        int Counts(Expression<Func<T, bool>> predicate);

        int Counts();

        bool Exists(Expression<Func<T, bool>> predicate);
    }
}
