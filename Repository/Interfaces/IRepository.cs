using System.Collections.Generic;
using System.Threading.Tasks;
using curso_api.Model.Base;

namespace curso_api.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> FindAllAsync();
        Task<TEntity> FindByIdAsync(long id);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task RemoveAsync(long id);
        Task<bool> ExistsAsync(TEntity entity);
    }
}