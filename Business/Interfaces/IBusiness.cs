using System.Collections.Generic;
using System.Threading.Tasks;
using curso_api.Model.Base;

namespace curso_api.Business.Interfaces
{
    public interface IBusiness<TEntity> where TEntity : class
    {
        Task<List<TEntity>> FindAllAsync();
        Task<TEntity> FindByIdAsync(long id);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task RemoveAsync(long id);
        Task<bool> Exists(TEntity entity);
    }
}