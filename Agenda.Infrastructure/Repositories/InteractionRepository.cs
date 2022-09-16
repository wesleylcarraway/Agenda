using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using Agenda.Infrastructure.Data;

namespace Agenda.Infrastructure.Repositories
{
    public class InteractionRepository : BaseRepository<Interaction>, IInteractionRepository
    {
        private readonly IJsonStorage<Interaction> _jsonStorage;

        public InteractionRepository(
            ApplicationContext context,
            IJsonStorage<Interaction> jsonStorage) : base(context)
        {
            _jsonStorage = jsonStorage;
        }

        public async Task<IEnumerable<Interaction>> SaveJsonInteractionsAsync()
        {
            var interactions = await GetAllAsync();
            if (interactions is not null)
                _jsonStorage.CreateMany(interactions);

            await _jsonStorage.SaveAsync();
            return interactions;
        }
    }
}
