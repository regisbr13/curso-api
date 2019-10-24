using System.Threading.Tasks;
using curso_api.Model;

namespace curso_api.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> FindByLogin(string username);
        Task InsertAsync(User user);
    }
}