using System.Collections.Generic;
using System.Threading.Tasks;
using curso_api.Business.Interfaces;
using curso_api.Data.VO;
using curso_api.Data.VO.Converters;
using curso_api.Model;
using curso_api.Repository.Interfaces;

namespace curso_api.Business
{
    public class PersonBusiness : IBusiness<PersonVO>
    {
        private readonly IRepository<Person> _repository;
        private readonly PersonConverter _converter;

        public PersonBusiness(IRepository<Person> repository, PersonConverter converter) {
            _repository = repository;
            _converter = converter;
        }

        public async Task<bool> Exists(PersonVO entity)
        {
            return await _repository.ExistsAsync(_converter.Parse(entity));
        }

        public async Task<List<PersonVO>> FindAllAsync()
        {
            return _converter.ParseList(await _repository.FindAllAsync());
        }

        public async Task<PersonVO> FindByIdAsync(long id)
        {
            return _converter.Parse(await _repository.FindByIdAsync(id));
        }

        public async Task<PersonVO> InsertAsync(PersonVO entity)
        {
            return _converter.Parse(await _repository.InsertAsync(_converter.Parse(entity)));
        }

        public async Task RemoveAsync(long id)
        {
            await _repository.RemoveAsync(id);
        }

        public async Task<PersonVO> UpdateAsync(PersonVO entity)
        {
            return _converter.Parse(await _repository.UpdateAsync(_converter.Parse(entity)));       
        }
    }
}