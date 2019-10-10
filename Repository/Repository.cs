using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using curso_api.Data;
using curso_api.Model.Base;
using curso_api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace curso_api.Repository
{
    // Data Persistence Layer
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly Context _context;
        private readonly DbSet<TEntity> dataset;

        public Repository(Context context) {
            _context = context;
            dataset = _context.Set<TEntity>();
        }
        
        public async Task<List<TEntity>> FindAllAsync()
        {
            var list = await dataset.ToListAsync();
            return list;
        }

        public async Task<TEntity> FindByIdAsync(long id)
        {
            var entity = await dataset.FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            try {
                await dataset.AddAsync(entity);
                await _context.SaveChangesAsync();
                return await FindByIdAsync(entity.Id);
            } 
            catch(Exception ex) {
                throw ex;
            }
        }

        public async Task RemoveAsync(long id)
        {
            try {
                var entity = await FindByIdAsync(id);
                dataset.Remove(entity);
                await _context.SaveChangesAsync();
            } 
            catch(Exception ex) {
                throw ex;
            }

        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try {
                var updatedPerson = await FindByIdAsync(entity.Id);
                if(updatedPerson == null) return null;
                _context.Entry(updatedPerson).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                return entity;
            } 
            catch(Exception ex) {
                throw ex;
            }
        }

        public async Task<bool> ExistsAsync(TEntity entity) {
            return await dataset.AnyAsync(x => x.Id == entity.Id);
        }
    }
}