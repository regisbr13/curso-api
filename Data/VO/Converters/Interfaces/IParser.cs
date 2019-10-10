using System.Collections.Generic;

namespace curso_api.Data.VO.Converters.Interfaces
{
    public interface IParser<O, D>
    {
         D Parse(O origin);
         List<D> ParseList(List<O> origin);
    }
}