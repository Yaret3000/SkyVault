using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Repositories.Base
{
    /// <summary>
    /// Base repository used to make CRUD operations
    /// </summary>
    /// <typeparam name="T">DbSet Types</typeparam>
    public abstract class BaseRepository<T> where T : class
    {
        protected DbContext _context;

        protected BaseRepository(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get the DbSet from context
        /// </summary>
        /// <returns></returns>
        public DbSet<T> GetEntitySet()
        {
            return _context.Set<T>();
        }

        /// <summary>
        /// Add new entity to selected table
        /// </summary>
        /// <param name="entity">Entity object</param>
        public virtual void Add(T entity)
        {
            GetEntitySet().Add(entity);
        }

        /// <summary>
        /// Return all rows from table
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll()
        {
            return GetEntitySet();
        }

        /// <summary>
        /// Rerturn the first coincidence in table
        /// </summary>
        /// <param name="predicate">Expression</param>
        /// <returns></returns>
        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return GetEntitySet().FirstOrDefault(predicate);
        }

        /// <summary>
        /// Delete a row in database
        /// </summary>
        /// <param name="entity">Entity object</param>
        public virtual void Delete(T entity)
        {
            GetEntitySet().Attach(entity);
            GetEntitySet().Remove(entity);
        }

        /// <summary>
        /// Save context changes
        /// </summary>
        public virtual void Save() {
            _context.SaveChanges();
        }
    }
}
