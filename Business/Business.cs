using System.Collections.Generic;
using System.Threading.Tasks;
using curso_api.Business.Interfaces;
using curso_api.Model.Base;
using curso_api.Repository.Interfaces;

namespace curso_api.Business
{
    // Business Rules Layer
    public class Business<TEntity> : IBusiness<TEntity> where TEntity : BaseEntity
    {
        private readonly IRepository<TEntity> _repository;

        public Business(IRepository<TEntity> repository) {
            _repository = repository;
        }
        public async Task<bool> ExistsAsync(TEntity entity)
        {
            return await _repository.ExistsAsync(entity);
        }

        public Task<List<TEntity>> FindAllAsync()
        {
            return _repository.FindAllAsync();
        }

        public Task<TEntity> FindByIdAsync(long id)
        {
            return _repository.FindByIdAsync(id);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            return await _repository.InsertAsync(entity);
        }

        public async Task RemoveAsync(long id)
        {
            await _repository.RemoveAsync(id);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await _repository.UpdateAsync(entity);
        }
    }
}