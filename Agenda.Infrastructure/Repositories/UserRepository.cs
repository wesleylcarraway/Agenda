using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using Agenda.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<User> GetUserUserRole(string userName)
        {
            return await _context.Users.AsNoTracking()
                .Include(x => x.UserRole)
                .FirstOrDefaultAsync(x => x.Username == userName);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await Query().AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
