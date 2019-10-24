using System;
using System.Threading.Tasks;
using curso_api.Data;
using curso_api.Model;
using curso_api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace curso_api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public async Task<User> FindByLogin(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task InsertAsync(User user)
        {
            try 
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            } 
            catch(Exception ex) 
            {
                throw ex;
            }
        }
    }
}