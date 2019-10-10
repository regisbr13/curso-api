using System.Collections.Generic;
using curso_api.Data.VO.Converters.Interfaces;
using curso_api.Model;

namespace curso_api.Data.VO.Converters
{
    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {
        public PersonVO Parse(Person origin)
        {
            if(origin == null) return new PersonVO();
            return new PersonVO {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }

        public Person Parse(PersonVO origin)
        {
            if(origin == null) return new Person();
            return new Person {
                Id = origin.Id,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Address = origin.Address,
                Gender = origin.Gender
            };
        }

        public List<PersonVO> ParseList(List<Person> origin)
        {
            if(origin == null) return new List<PersonVO>();
            return origin.ConvertAll(person => Parse(person));
        }

        public List<Person> ParseList(List<PersonVO> origin)
        {
            if(origin == null) return new List<Person>();
            return origin.ConvertAll(person => Parse(person));
        }
    }
}