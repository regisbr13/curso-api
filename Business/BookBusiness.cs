using System.Collections.Generic;
using System.Threading.Tasks;
using curso_api.Business.Interfaces;
using curso_api.Data.VO;
using curso_api.Data.VO.Converters;
using curso_api.Model;
using curso_api.Repository.Interfaces;

namespace curso_api.Business
{
    public class BookBusiness : IBusiness<BookVO>
    {
        private readonly IRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookBusiness(IRepository<Book> repository, BookConverter converter) {
            _repository = repository;
            _converter = converter;
        }

        public async Task<bool> Exists(BookVO entity)
        {
            return await _repository.ExistsAsync(_converter.Parse(entity));
        }

        public async Task<List<BookVO>> FindAllAsync()
        {
            return _converter.ParseList(await _repository.FindAllAsync());
        }

        public async Task<BookVO> FindByIdAsync(long id)
        {
            return _converter.Parse(await _repository.FindByIdAsync(id));
        }

        public async Task<BookVO> InsertAsync(BookVO entity)
        {
            return _converter.Parse(await _repository.InsertAsync(_converter.Parse(entity)));
        }

        public async Task RemoveAsync(long id)
        {
            await _repository.RemoveAsync(id);
        }

        public async Task<BookVO> UpdateAsync(BookVO entity)
        {
            return _converter.Parse(await _repository.UpdateAsync(_converter.Parse(entity)));       
        }
    }
}