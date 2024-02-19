using Microsoft.EntityFrameworkCore;

namespace DungeonsAndArtificialIntelligenceAPI.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbSet<TEntity> dbSet { get; set; }
        private DandAIDbContext _context { get; set; }
        public Repository(DandAIDbContext context) 
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this.dbSet = this._context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll() => this.dbSet.AsQueryable();

        public IQueryable<TEntity> GetAll(Func<TEntity, bool> predicate) => this.dbSet.Where(predicate).AsQueryable();

        public TEntity Get(Func<TEntity, bool> predicate) => this.dbSet.SingleOrDefault(predicate);

        public void Add(TEntity entity)
        {
            this.dbSet.Add(entity);
            this._context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            this.dbSet.Update(entity);
            this._context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            this.dbSet.Remove(entity);
            this._context.SaveChanges();
        }
    }
}
