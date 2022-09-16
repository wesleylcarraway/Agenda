using Agenda.Domain.Interfaces;
using Agenda.Infrastructure.Data;

namespace Agenda.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationContext _context { get; set; }
        public UnitOfWork(ApplicationContext Context)
        {
            _context = Context;
        }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
