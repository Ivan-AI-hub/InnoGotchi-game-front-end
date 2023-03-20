using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate.Comands;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate.Filtrators;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate.Sorters;

namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate
{
    public interface IPetService
    {
        Task<IServiceResult> CreateAsync(AddPetModel addModel, CancellationToken cancellationToken = default);
        Task<IServiceResult> FeedAsync(int petId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Pet>> GetAsync(PetSorter? sorter = null, PetFiltrator? filtrator = null, CancellationToken cancellationToken = default);
        Task<Pet?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Pet>> GetPageAsync(int pageSize, int pageNumber, PetSorter sorter, PetFiltrator filtrator, CancellationToken cancellationToken = default);
        Task<IServiceResult> GiveDrinkAsync(int petId, CancellationToken cancellationToken = default);
        Task<IServiceResult> ResetHappinessDay(int petId, CancellationToken cancellationToken = default);
        Task<IServiceResult> SetDeadStatus(int petId, DateTime deadDate, CancellationToken cancellationToken = default);
        Task<IServiceResult> UpdateAsync(UpdatePetModel updateModel, CancellationToken cancellationToken = default);
    }
}