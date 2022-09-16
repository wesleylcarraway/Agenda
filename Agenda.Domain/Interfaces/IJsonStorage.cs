using Agenda.Domain.Core;

namespace Agenda.Domain.Interfaces
{
    public interface IJsonStorage<T> where T : Register
    {
        T Create(T model);
        T Update(T model);
        T Remove(int id);
        IEnumerable<T> GetAll();
        T GetById(int id);
        Task SaveAsync();
        IEnumerable<T> CreateMany(IEnumerable<T> models);
    }
}
