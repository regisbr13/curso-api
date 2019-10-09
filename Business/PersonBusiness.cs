using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using curso_api.Business.Interfaces;
using curso_api.Model;
using curso_api.Repository.Interfaces;

namespace curso_api.Business
{
    // CAMADA DE IMPLEMENTAÇÃO DAS REGRAS DE NEGÓCIO
    public class PersonBusiness : IPersonBusiness
    {
        private readonly IPersonRepository _repository;

        public PersonBusiness(IPersonRepository repository) {
            _repository = repository;
        }
        public async Task<bool> ExistsAsync(Person person)
        {
            return await _repository.ExistsAsync(person);
        }

        public Task<List<Person>> FindAllAsync()
        {
            return _repository.FindAllAsync();
        }

        public Task<Person> FindByIdAsync(long id)
        {
            return _repository.FindByIdAsync(id);
        }

        public async Task<Person> InsertAsync(Person person)
        {
            return await _repository.InsertAsync(person);
        }

        public async Task RemoveAsync(long id)
        {
            await _repository.RemoveAsync(id);
        }

        public async Task<Person> UpdateAsync(Person person)
        {
            return await _repository.UpdateAsync(person);
        }
    }
}