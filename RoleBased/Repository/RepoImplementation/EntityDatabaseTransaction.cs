using Microsoft.EntityFrameworkCore.Storage;
using RoleBased.Models.Domain;

namespace RoleBased.Repository.RepoImplementation
{
    public class EntityDatabaseTransaction : IDisposable
    {
        private IDbContextTransaction _transaction;
        public EntityDatabaseTransaction(ApplicationDb applicationDb) 
        {
            _transaction = applicationDb.Database.BeginTransaction();

        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }
        public void Dispose()
        {
            _transaction.Dispose();
        }
    }
}
