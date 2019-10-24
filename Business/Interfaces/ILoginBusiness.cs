using System.Threading.Tasks;
using curso_api.Model;

namespace curso_api.Business.Interfaces
{
    public interface ILoginBusiness
    {
        Task<object> Login(User user);
        Task<object> Register(User user);
    }
}