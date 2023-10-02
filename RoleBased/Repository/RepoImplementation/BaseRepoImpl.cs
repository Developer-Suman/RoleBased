using Microsoft.EntityFrameworkCore;
using RoleBased.Models.Domain;
using RoleBased.Repository.RepoInterface;

namespace RoleBased.Repository.RepoImplementation
{
    public class BaseRepoImpl<T> : BaseIRepo<T> where T : class
    {

        private readonly ApplicationDb _applicationDb;
        protected readonly DbSet<T> _dbSet;

        public BaseRepoImpl(ApplicationDb applicationDb)
        {
            _applicationDb = applicationDb;
            _dbSet = applicationDb.Set<T>();
            
        }
        public void AddRange(IEnumerable<T> entities)
        {
            _applicationDb.Set<T>().AddRange(entities);
        }

        public void Attach<T>(T entity)
        {
            _applicationDb.Attach(entity);
            
        }

        public void delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return _applicationDb.Set<T>().Where(expression);
        }

        public List<T> getAll()
        {
            return _applicationDb.Set<T>().ToList();
        }

        public T getById(int id)
        {
            return _applicationDb.Set<T>().Find(id);
        }

    

        public IQueryable<T> getQueryable()
        {
            return _applicationDb.Set<T>();
        }

        public void insert(T entity)
        {
            _applicationDb.Add(entity);
        }

        public IQueryable<T> Queryable()
        {
            return _dbSet.AsQueryable();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _applicationDb.Set<T>().RemoveRange(entities);
        }

        public void Save()
        {
            _applicationDb.SaveChanges();
        }

        public void update(T entity)
        {
            _applicationDb.Entry(entity).State = EntityState.Modified;
        }

        public T Where(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return _applicationDb.Set<T>().FirstOrDefault(expression);
        }

        public T getByString(string stringId)
        {
            return _applicationDb.Set<T>().Find(stringId);
        }
    }
}
