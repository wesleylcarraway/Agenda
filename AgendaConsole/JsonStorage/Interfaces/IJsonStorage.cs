namespace AgendaConsole.JsonStorage.Interfaces
{
    public interface IJsonStorage<T> where T : class
    {
        T Create(T model);
        T Update(T model);
        T Remove(int id);
        IEnumerable<T> GetAll();
        T GetById(int id);
        Task SaveAsync();
    }
}
