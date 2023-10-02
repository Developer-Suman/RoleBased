using RoleBased.Models.Domain;
using RoleBased.Repository.RepoInterface;

namespace RoleBased.Repository.RepoImplementation
{
    public class UnitOfWorkRepoImpl : UnitOfWorkIRepo, IDisposable
    {
        private ApplicationDb entities = null;

        public UnitOfWorkRepoImpl(ApplicationDb applicationDb)
        {
            entities = applicationDb;
            
        }

        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public BaseIRepo<T> Repository<T>() where T : class
        {
            if(repositories.Keys.Contains(typeof(T)) ==  true)
            {
                return repositories[typeof(T)] as BaseIRepo<T>;
            }

            BaseIRepo<T> repository = new BaseRepoImpl<T>(entities);
            repositories.Add(typeof(T), repository);
            return repository;
        }

        public EntityDatabaseTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public int Commit()
        {
            return entities.SaveChanges();
        }

        private bool disposed = false;

        public void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                entities.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
