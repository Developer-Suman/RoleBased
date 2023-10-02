using RoleBased.Repository.RepoImplementation;

namespace RoleBased.Repository.RepoInterface
{
    public interface UnitOfWorkIRepo : IDisposable
    {
        int Commit();

        EntityDatabaseTransaction BeginTransaction();
    }
}
