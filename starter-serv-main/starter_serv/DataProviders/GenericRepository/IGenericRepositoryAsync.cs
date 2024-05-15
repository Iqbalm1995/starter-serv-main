using starter_serv.Helper;
using starter_serv.ViewModel;
using System.Linq.Expressions;

namespace starter_serv.DataProviders
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        Task<T> GetByIdAsync(int id, string idFieldName = "Id", string[] includes = null);
        Task<T> GetByIdAsync(Guid id, string idFieldName = "Id", string[] includes = null);
        Task<T> GetByIdAsync(string id, string idFieldName = "Id", string[] includes = null);
        Task<T> GetByPredicate(Expression<Func<T, bool>> predicate, string[] includes = null);
        Task<T> GetByPredicate(List<RequestFilterParameter> requestFilters, string[] includes = null);
        Task<bool> GetAnyByPredicate(Expression<Func<T, bool>> predicate, string[] includes = null);
        Task<List<T>> GetListByPredicate(Expression<Func<T, bool>> predicate, string[] includes = null, bool ignoreQueryFilter = false);
        IQueryable<T> GetQueryByPredicate(Expression<Func<T, bool>> predicate);
        Task<int> GetCountByPredicate(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAllAsync(string[] includes = null);
        IReadOnlyList<T> GetAll();
        Task<PagedRepositoryResponse<IReadOnlyList<T>>> GetPagedReponseAsync(IRequestParameter request, string[] includes = null);
        Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<T> AddAsync(T entity, bool callSave = true);
        Task SaveChangeAsync();
        Task<List<T>> AddRangeAsync(List<T> entities);
        List<T> AddRange(List<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(List<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(List<T> entities);
        void DeleteRange(List<T> entities);
        //void DisableAuditable();
    }
}
