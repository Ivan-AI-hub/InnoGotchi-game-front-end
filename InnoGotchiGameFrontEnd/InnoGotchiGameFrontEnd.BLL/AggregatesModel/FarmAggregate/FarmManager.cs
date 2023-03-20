using AutoMapper;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.FarmAggregate;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.FarmAggregate.Comands;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.FarmAggregate.Filtrators;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.FarmAggregate.Sorters;

namespace InnoGotchiGameFrontEnd.BLL.AggregatesModel.FarmAggregate
{
    public class FarmManager
    {
        private IFarmService _farmService;
        private IMapper _mapper;

        public FarmManager(IFarmService farmService, IMapper mapper)
        {
            _farmService = farmService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PetFarmDTO>> GetAsync(FarmDTOSorter? sorter = null, FarmDTOFiltrator? filtrator = null, CancellationToken cancellationToken = default)
        {
            var dataSorter = _mapper.Map<FarmSorter>(sorter);
            var dataFiltrator = _mapper.Map<FarmFiltrator>(filtrator);

            var dataFarms = await _farmService.GetAsync(dataSorter, dataFiltrator, cancellationToken);
            var farms = _mapper.Map<IEnumerable<PetFarmDTO>>(dataFarms);
            return farms;
        }

        public async Task<PetFarmDTO> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var dataFarm = await _farmService.GetByIdAsync(id, cancellationToken);
            var farm = _mapper.Map<PetFarmDTO>(dataFarm);
            return farm;
        }

        public async Task<ManagerResult> CreateAsync(AddFarmDTOModel addModel, CancellationToken cancellationToken = default)
        {
            var validator = new AddFarmDTOValidator();
            var validationResult = validator.Validate(addModel);
            if (!validationResult.IsValid)
            {
                return new ManagerResult(validationResult);
            }

            var addDataModel = _mapper.Map<AddFarmModel>(addModel);
            var serviceResult = await _farmService.CreateAsync(addDataModel, cancellationToken);
            return new ManagerResult(serviceResult);
        }

        public async Task<ManagerResult> UpdateAsync(UpdateFarmDTOModel updateModel, CancellationToken cancellationToken = default)
        {
            var validator = new UpdateFarmDTOValidator();
            var validationResult = validator.Validate(updateModel);
            if (!validationResult.IsValid)
            {
                return new ManagerResult(validationResult);
            }

            var updateDataModel = _mapper.Map<UpdateFarmModel>(updateModel);
            var serviceResult = await _farmService.UpdateAsync(updateDataModel, cancellationToken);
            return new ManagerResult(serviceResult);
        }
    }
}
