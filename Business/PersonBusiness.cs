using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using curso_api.Business.Interfaces;
using curso_api.Data.VO;
using curso_api.Data.VO.Converters;
using curso_api.Model;
using curso_api.Repository.Interfaces;
using Tapioca.HATEOAS.Utils;

namespace curso_api.Business
{
    public class PersonBusiness : IBusiness<PersonVO>
    {
        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusiness(IPersonRepository repository, PersonConverter converter) {
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

        public async Task<List<PersonVO>> FindByName(string firstName, string lastName)
        {
            return _converter.ParseList(await _repository.FindByName(firstName, lastName));
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

        public async Task<PagedSearchDTO<PersonVO>> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            page = page > 1 ? page -1 : 0;
            var query = @"select * from Persons P";
            var countQuery = @"select count(*) from Persons P";
            if(!string.IsNullOrEmpty(name)) 
            {
                query += $" where P.FirstName like '%{name}%'";
                countQuery += $" where P.FirstName like '%{name}%'";
            }
            query += $" order by P.FirstName {sortDirection} limit {pageSize} offset {page}";
            
            int TotalResults = _repository.TotalResults(countQuery);
            var persons = _converter.ParseList(await _repository.FindWithPagedSearch(query));

            return new PagedSearchDTO<PersonVO>
            {
                CurrentPage = page + 1,
                List = persons,
                PageSize = pageSize,
                SortDirections = sortDirection,
                TotalResults = TotalResults
            };
        }
    }
}