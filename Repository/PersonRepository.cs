using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using curso_api.Data;
using curso_api.Model;
using curso_api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace curso_api.Repository
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        private readonly Context _context;
        public PersonRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Person>> FindByName(string firstName, string lastName)
        {
            if(!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
                return await _context.Persons.Where(x => x.FirstName.ToUpper().Contains(firstName.ToUpper()) && x.LastName.ToUpper().Contains(lastName.ToUpper())).ToListAsync();
            else if(!string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
                return await _context.Persons.Where(x => x.FirstName.ToUpper().Contains(firstName.ToUpper())).ToListAsync();
            else if(string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
                return await _context.Persons.Where(x => x.LastName.ToUpper().Contains(lastName.ToUpper())).ToListAsync();
            return await _context.Persons.ToListAsync();
        }

        public async Task<List<Person>> FindWithPagedSearch(string query)
        {
            return await _context.Persons.FromSql<Person>(query).ToListAsync();
        }

        public int TotalResults(string query) 
        {
            var result = "";
            using(var connection = _context.Database.GetDbConnection()) 
            {
                connection.Open();
                using(var comand = connection.CreateCommand())
                {
                    comand.CommandText = query;
                    result = comand.ExecuteScalar().ToString();
                }
            }
            return int.Parse(result);
        }
    }
}