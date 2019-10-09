using System.Collections.Generic;
using System.Threading.Tasks;
using curso_api.Model;

namespace curso_api.Repository.Interfaces
{
    public interface IPersonRepository
    {
        Task<List<Person>> FindAllAsync();
        Task<Person> FindByIdAsync(long id);
        Task<Person> InsertAsync(Person person);
        Task<Person> UpdateAsync(Person person);
        Task RemoveAsync(long id);
        Task<bool> ExistsAsync(Person person);
    }
}