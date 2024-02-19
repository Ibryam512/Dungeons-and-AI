namespace DungeonsAndArtificialIntelligenceAPI.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAll(Func<TEntity, bool> predicate);

        TEntity Get(Func<TEntity, bool> predicate);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
