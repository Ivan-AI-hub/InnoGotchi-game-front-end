using AuthorizationInfrastructure.HttpClients;
using AutoMapper;
using InnoGotchiGameFrontEnd.BLL.ComandModels.Farm;
using InnoGotchiGameFrontEnd.BLL.Filtrators;
using InnoGotchiGameFrontEnd.BLL.Model;
using InnoGotchiGameFrontEnd.BLL.Sorters;
using InnoGotchiGameFrontEnd.BLL.Validators.Farms;
using InnoGotchiGameFrontEnd.DAL.Models.Farms;
using InnoGotchiGameFrontEnd.DAL.Services;

namespace InnoGotchiGameFrontEnd.BLL
{
    public class FarmManager
    {
        private FarmService _farmService;
        private IMapper _mapper;

        public FarmManager(IAuthorizedClient client, IMapper mapper)
        {
            _farmService = new FarmService(client);
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

        public async Task<ManagerRezult> CreateAsync(AddFarmDTOModel addModel, CancellationToken cancellationToken = default)
        {
            var validator = new AddFarmDTOValidator();
            var validationResult = validator.Validate(addModel);
            if (!validationResult.IsValid)
            {
                return new ManagerRezult(validationResult);
            }

            var rezult = new ManagerRezult();
            var addDataModel = _mapper.Map<AddFarmModel>(addModel);
            var serviceRezult = await _farmService.CreateAsync(addDataModel, cancellationToken);
            rezult.Errors.AddRange(serviceRezult.Errors);

            return rezult;
        }

        public async Task<ManagerRezult> UpdateAsync(UpdateFarmDTOModel updateModel, CancellationToken cancellationToken = default)
        {
            var validator = new UpdateFarmDTOValidator();
            var validationResult = validator.Validate(updateModel);
            var rezult = new ManagerRezult(validationResult);
            if (validationResult.IsValid)
            {
                var updateDataModel = _mapper.Map<UpdateFarmModel>(updateModel);
                var serviceRezult = await _farmService.UpdateAsync(updateDataModel, cancellationToken);
                rezult.Errors.AddRange(serviceRezult.Errors);
            }
            return rezult;
        }
    }
}
