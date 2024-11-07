using System.Linq.Expressions;

namespace BadmintonBlast.DataAccess.Adstraction
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        Task<IEnumerable<TEntity>> GetAsync();  
        Task<IEnumerable<TOut>> GetAsync<TOut>();
        Task<IEnumerable<TEntity>> GetAsync(int pageIndex, int pageSize);
        Task<IEnumerable<TOut>> GetAsync<TOut>(int pageIndex, int pageSize);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predecate);
        Task<IEnumerable<TOut>> GetAsync<TOut>(Expression<Func<TEntity, bool>> predecate);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predecate, int pageIndex, int pageSize);
        Task<IEnumerable<TOut>> GetAsync<TOut>(Expression<Func<TEntity, bool>> predecate, int pageIndex, int pageSize);

        Task<TEntity> GetSingleAsync(object id);
        Task<TOut> GetSingleAsync<TOut>(object id);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predecate);
        Task<TOut> GetSingleAsync<TOut>(Expression<Func<TEntity, bool>> predecate);

        Task<int> GetTotalItemsAsync();
        Task<int> GetTotalItemsAsync(Expression<Func<TEntity, bool>> predecate);

        Task InsertAsync(TEntity entity);
        Task DeleteAsync(object id);
        void Update(TEntity entity);

        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}
