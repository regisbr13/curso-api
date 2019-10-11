using curso_api.Data;
using curso_api.Model;
using curso_api.Repository.Interfaces;

namespace curso_api.Repository
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(Context context) : base(context){}
    }
}