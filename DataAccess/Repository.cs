    using BadmintonBlast.DataAccess.Adstraction;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
namespace BadmintonBlast.DataAccess
{
   
    public class Repository<TEntity> 
        : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly DbContext context;
        private readonly DbSet<TEntity> _dbSet;
        protected readonly IMapper mapper;
        public Repository(DbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            _dbSet = context.Set<TEntity>();
        }
        public async Task<int> GetTotalItemsAsync()
        {
            return await context
                .Set<TEntity>()
                .CountAsync();
        }

        public async Task<int> GetTotalItemsAsync(
            Expression<Func<TEntity, bool>> predecate)
        {
            return await context
                .Set<TEntity>()
                .CountAsync(predecate);
        }

        public async Task DeleteAsync(object id)
        {
            var entity = await GetSingleAsync(id);
            context.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }
        public async Task<IEnumerable<TOut>> GetAsync<TOut>()
        {
            var query = context.Set<TEntity>();
            return await mapper.ProjectTo<TOut>(query).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(
            int pageIndex,
            int pageSize)
        {
            return await context
                .Set<TEntity>()
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<IEnumerable<TOut>> GetAsync<TOut>(
            int pageIndex,
            int pageSize)
        {
            var query = context
                .Set<TEntity>()
                .Skip((pageIndex-1) * pageSize)
                .Take(pageSize);
            return await mapper.ProjectTo<TOut>(query)
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> predecate)
        {
            return await context
                .Set<TEntity>()
                .Where(predecate)
                .ToListAsync();
        }

        public async Task<IEnumerable<TOut>> GetAsync<TOut>(
            Expression<Func<TEntity, bool>> predecate)
        {
            var query = context
                .Set<TEntity>()
                .Where(predecate);
            return await mapper.ProjectTo<TOut>(query).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> predecate,
            int pageIndex,
            int pageSize)
        {
            return await context
                .Set<TEntity>()
                .Where(predecate)
                .Skip((pageIndex-1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<TOut>> GetAsync<TOut>(
            Expression<Func<TEntity, bool>> predecate,
            int pageIndex,
            int pageSize)
        {
            var query = context
                .Set<TEntity>()
                .Where(predecate)
                .Skip((pageIndex-1) * pageSize)
                .Take(pageSize);

            return await mapper
                .ProjectTo<TOut>(query)
                .ToListAsync();
        }
        public async Task<TEntity> GetSingleAsync(object id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }
        public async Task<TOut> GetSingleAsync<TOut>(object id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            return mapper.Map<TOut>(entity);
        }
        public async Task<TEntity> GetSingleAsync(
            Expression<Func<TEntity, bool>> predecate
        )
        {
            return await context.Set<TEntity>().FirstOrDefaultAsync(predecate);
        }
        public async Task<TOut> GetSingleAsync<TOut>(
            Expression<Func<TEntity, bool>> predecate
        )
        {
            var entity = await context
                .Set<TEntity>()
                .FirstOrDefaultAsync(predecate);
            return mapper.Map<TOut>(entity);
        }
        public async Task InsertAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
        }

        // Các phương thức truy vấn
        public IQueryable<TEntity> GetAll() => _dbSet.AsQueryable();
        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) => _dbSet.Where(predicate);
    }
}