using InnoGotchiGameFrontEnd.Domain.AggregatesModel.FarmAggregate.Comands;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.FarmAggregate.Filtrators;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.FarmAggregate.Sorters;

namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.FarmAggregate
{
    public interface IFarmService
    {
        Task<IServiceResult> CreateAsync(AddFarmModel addModel, CancellationToken cancellationToken = default);
        Task<IEnumerable<PetFarm>> GetAsync(FarmSorter? sorter = null, FarmFiltrator? filtrator = null, CancellationToken cancellationToken = default);
        Task<PetFarm?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IServiceResult> UpdateAsync(UpdateFarmModel updateModel, CancellationToken cancellationToken = default);
    }
}