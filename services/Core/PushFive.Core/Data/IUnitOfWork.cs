using System.Threading.Tasks;

namespace PushFive.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
