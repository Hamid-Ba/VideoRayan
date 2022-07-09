using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Framework.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        public Repository(DbContext context) => _context = context;

        //Read
        public IEnumerable<TEntity> GetAllEntities() => _context.Set<TEntity>().AsNoTracking().ToList();
        public async Task<IEnumerable<TEntity>> GetAllEntitiesAsync() => await _context.Set<TEntity>().AsNoTracking().ToListAsync();

        public TEntity GetEntityById(object id) => _context.Set<TEntity>().Find(id)!;
        public async Task<TEntity> GetEntityByIdAsync(object id) => (await _context.Set<TEntity>().FindAsync(id))!;

        //Create
        public object AddEntity(TEntity entity) => _context.Set<TEntity>().Add(entity);
        public void AddRangeOfEntities(IEnumerable<TEntity> entities) => _context.Set<TEntity>().AddRange(entities);
        public async Task<object> AddEntityAsync(TEntity entity) => await _context.Set<TEntity>().AddAsync(entity);
        public async Task AddRangeOfEntitiesAsync(IEnumerable<TEntity> entities) => await _context.Set<TEntity>().AddRangeAsync(entities);

        //Update
        public bool UpdateEntity(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Update(entity);
                return true;
            }
            catch { return false; }
        }
        public void UpdateRangeOfEntities(IEnumerable<TEntity> entities) => _context.Set<TEntity>().UpdateRange(entities);

        //Delete    
        public bool DeleteEntity(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);
                return true;
            }
            catch { return false; }
        }

        public void DeleteRangeOfEntities(IEnumerable<TEntity> entities) => _context.Set<TEntity>().RemoveRange(entities);

        public int GetCountOfEntity() => _context.Set<TEntity>().AsNoTracking().Count();

        public IEnumerable<TEntity> PaginationOfEntity(int currentPage, int pageSize)
        {
            var entities = GetAllEntities();
            return entities.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        public bool Exists(Expression<Func<TEntity, bool>> expression) => _context.Set<TEntity>().Any(expression);

        public void SaveChanges() => _context.SaveChanges();

        public Task SaveChangesAsync() => _context.SaveChangesAsync();
        
    }
}