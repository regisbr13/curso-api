using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaApi.Controllers.Model.Interfaces.Service
{
    public interface IPersonService
    {
        Task<List<Person>> FindAll();
        Task<Person> FindById(long id);
        Task<Person> Insert(Person person);
        Task<Person> Update(Person person);
        Task Remove(long id);

    }
}