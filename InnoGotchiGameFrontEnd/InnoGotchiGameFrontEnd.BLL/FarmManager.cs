using AutoMapper;
using InnoGotchiGameFrontEnd.BLL.ComandModels.Farm;
using InnoGotchiGameFrontEnd.BLL.Filtrators;
using InnoGotchiGameFrontEnd.BLL.Model;
using InnoGotchiGameFrontEnd.BLL.Sorters;
using InnoGotchiGameFrontEnd.DAL.Models.Farms;
using InnoGotchiGameFrontEnd.DAL.Services;

namespace InnoGotchiGameFrontEnd.BLL
{
	public class FarmManager
    {
        private FarmService _service;
        private IMapper _mapper;

        public FarmManager(HttpClient client, IMapper mapper)
        {
            _service = new FarmService(client);
            _mapper = mapper;
        }

        public async Task<IEnumerable<PetFarmDTO>> GetAllFarms(FarmDTOSorter sorter, FarmDTOFiltrator filtrator)
        {
            var dataSorter = _mapper.Map<FarmSorter>(sorter);
            var dataFiltrator = _mapper.Map<FarmFiltrator>(filtrator);
            var dataFarms = await _service.GetFarms(dataSorter, dataFiltrator);
            var farms = _mapper.Map<IEnumerable<PetFarmDTO>>(dataFarms);
            return farms;
        }

        public async Task<PetFarmDTO> GetFarmById(int id)
        {
            var dataUsers = await _service.GetFarmById(id);
            var user = _mapper.Map<PetFarmDTO>(dataUsers);
            return user;
        }

        public async Task<ManagerRezult> Create(AddFarmDTOModel addModel)
        {
            var addDataModel = _mapper.Map<AddFarmModel>(addModel);
            var rezult = new ManagerRezult();
            var serviceRezult = await _service.Create(addDataModel);
            rezult.Errors.AddRange(serviceRezult.Errors);
            return rezult;
        }

        public async Task<ManagerRezult> UpdateFarm(UpdateFarmDTOModel updateModel)
        {
            var updateDataModel = _mapper.Map<UpdateFarmModel>(updateModel);
            var rezult = new ManagerRezult();
            var serviceRezult = await _service.UpdateFarm(updateDataModel);
            rezult.Errors.AddRange(serviceRezult.Errors);
            return rezult;
        }
    }
}
