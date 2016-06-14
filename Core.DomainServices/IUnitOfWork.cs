using System.Threading.Tasks;

namespace Core.DomainServices
{
    public interface IUnitOfWork
    {
        int Save();
        int SaveChanges();
        Task<int> SaveAsync();
    }
}
