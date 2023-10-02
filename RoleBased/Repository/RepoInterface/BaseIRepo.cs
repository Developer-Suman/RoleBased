using RoleBased.Models.ViewModel;
using System.Linq.Expressions;

namespace RoleBased.Repository.RepoInterface
{
    public interface BaseIRepo<T> where T : class
    {
        void Attach<T> (T entity);
        IQueryable<T> Queryable();

        //Removes a record from the context
        void delete(T entity);
        //Add a new record to the context
        void insert(T entity);

        void update(T entity);

        //Get's all the record
        List<T> getAll();

        //Gets the entity by id
        T getById(int id);
        T getByString(string stringId);

        IQueryable<T> getQueryable();

        //Find  a set of record that matches the passed expression
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);

        public T Where(Expression<Func<T, bool>> expression);

        //Add alist of records
        void AddRange(IEnumerable<T> entities);
        //Remove a list of records
        void RemoveRange(IEnumerable<T> entities);
        void Save();
       
    }
}
