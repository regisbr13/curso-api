using curso_api.Data;
using curso_api.Model;
using curso_api.Repository.Interfaces;

namespace curso_api.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(Context context) : base(context){}
    }
}