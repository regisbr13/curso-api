using curso_api.Business.Interfaces;
using curso_api.Model;
using curso_api.Repository.Interfaces;

namespace curso_api.Business
{
    public class PersonBusiness : Business<Person>, IPersonBusiness
    {
        private readonly IRepository<Person> _repository;

        public PersonBusiness(IRepository<Person> repository) :base(repository) {}
    }
}