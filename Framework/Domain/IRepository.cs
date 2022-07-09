using System.Linq.Expressions;

namespace Framework.Domain
{
    public interface IRepository<TEntity>
    {
        //Read ALL
        Task<IEnumerable<TEntity>> GetAllEntitiesAsync();
        IEnumerable<TEntity> GetAllEntities();

        //Read ONE
        Task<TEntity> GetEntityByIdAsync(object id);
        TEntity GetEntityById(object id);

        //Create One&Range
        object AddEntity(TEntity entity);
        void AddRangeOfEntities(IEnumerable<TEntity> entities);
        Task<object> AddEntityAsync(TEntity entity);
        Task AddRangeOfEntitiesAsync(IEnumerable<TEntity> entities);

        //Update One&Range
        bool UpdateEntity(TEntity entity);
        void UpdateRangeOfEntities(IEnumerable<TEntity> entities);

        //Delete One&Range
        bool DeleteEntity(TEntity entity);
        void DeleteRangeOfEntities(IEnumerable<TEntity> entities);

        bool Exists(Expression<Func<TEntity, bool>> expression);
        void SaveChanges();
        Task SaveChangesAsync();

        int GetCountOfEntity();
        IEnumerable<TEntity> PaginationOfEntity(int currentPage, int pageSize);
    }
}