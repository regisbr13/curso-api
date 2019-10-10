using curso_api.Business.Interfaces;
using curso_api.Model;
using curso_api.Repository.Interfaces;

namespace curso_api.Business
{
    public class BookBusiness : Business<Book>, IBookBusiness
    {
        private readonly IRepository<Book> _repository;

        public BookBusiness(IRepository<Book> repository) :base(repository) {}
    }
}