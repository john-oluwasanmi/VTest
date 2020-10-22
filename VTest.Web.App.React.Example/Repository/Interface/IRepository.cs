using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using V.Test.Web.Api.Entities.Interface;

namespace V.Test.Web.Api.Repository.Interface
{
    public interface IRepository<T> where T : IEntity
    {
        Task<long> AddAsync(T item);

        Task<T> GetAsync(int id);

        Task<T> GetAsync(int id, params string[] includes);

        Task<List<T>> ListAsync(int pageNumber);

        Task UpdateAsync(T item);

        Task DeleteAsync(T entity);






        List<T> FindBy(Expression<Func<T, bool>> predicate, int pageNumber);

        List<T> FindBy(int page, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);

        int Counts(Expression<Func<T, bool>> predicate);

        int Counts();

        bool Exists(Expression<Func<T, bool>> predicate);
    }
}
