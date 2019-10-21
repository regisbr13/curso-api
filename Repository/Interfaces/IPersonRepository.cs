using System.Collections.Generic;
using System.Threading.Tasks;
using curso_api.Model;

namespace curso_api.Repository.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<List<Person>> FindByName(string firstName, string lastName);
        Task<List<Person>> FindWithPagedSearch(string query);
        int TotalResults(string name);
    }
}