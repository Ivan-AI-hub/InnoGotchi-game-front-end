
using AutoMapper;
using InnoGotchiGameFrontEnd.BLL.ComandModels.Pet;
using InnoGotchiGameFrontEnd.BLL.Filtrators;
using InnoGotchiGameFrontEnd.BLL.Model;
using InnoGotchiGameFrontEnd.BLL.Sorters;
using InnoGotchiGameFrontEnd.DAL.Models.Pets;
using InnoGotchiGameFrontEnd.DAL.Services;

namespace InnoGotchiGameFrontEnd.BLL
{
	public class PetManager
	{
		private PetService _service;
		private IMapper _mapper;

		public PetManager(PetService service, IMapper mapper)
		{
			_service = service;
			_mapper = mapper;
		}


		public async Task<IEnumerable<PetDTO>> GetAllPets(PetDTOSorter sorter, PetDTOFiltrator filtrator)
		{
			var dataSorter = _mapper.Map<PetSorter>(sorter);
			var dataFiltrator = _mapper.Map<PetFiltrator>(filtrator);
			var dataPets = await _service.GetPets(dataSorter, dataFiltrator);
			var pets = _mapper.Map<IEnumerable<PetDTO>>(dataPets);
			return pets;
		}

		public async Task<IEnumerable<PetDTO>> GetPetsPage(int pageSize, int pageNumber, PetDTOSorter sorter, PetDTOFiltrator filtrator)
		{
			var dataSorter = _mapper.Map<PetSorter>(sorter);
			var dataFiltrator = _mapper.Map<PetFiltrator>(filtrator);
			var dataPets = await _service.GetPetsPage(pageSize, pageNumber, dataSorter, dataFiltrator);
			var pets = _mapper.Map<IEnumerable<PetDTO>>(dataPets);
			return pets;
		}
		public async Task<PetDTO> GetPetById(int id)
		{
			var dataPets = await _service.GetPetById(id);
			var pet = _mapper.Map<PetDTO>(dataPets);
			return pet;
		}

		public async Task<ManagerRezult> Create(AddPetDTOModel addModel)
		{
			var addDataModel = _mapper.Map<AddPetModel>(addModel);
			var rezult = new ManagerRezult();
			var serviceRezult = await _service.Create(addDataModel);
			rezult.Errors.AddRange(serviceRezult.Errors);
			return rezult;
		}

		public async Task<ManagerRezult> UpdatePet(UpdatePetDTOModel updateModel)
		{
			var updateDataModel = _mapper.Map<UpdatePetModel>(updateModel);
			var rezult = new ManagerRezult();
			var serviceRezult = await _service.UpdatePet(updateDataModel);
			rezult.Errors.AddRange(serviceRezult.Errors);

			return rezult;
		}

		public async Task<ManagerRezult> Feed(int petId, int feederId)
		{
			var rezult = new ManagerRezult();
			var serviceRezult = await _service.Feed(petId, feederId);
			rezult.Errors.AddRange(serviceRezult.Errors);
			return rezult;
		}

		public async Task<ManagerRezult> GiveDrink(int petId, int drinkerId)
		{
			var rezult = new ManagerRezult();
			var serviceRezult = await _service.GiveDrink(petId, drinkerId);
			rezult.Errors.AddRange(serviceRezult.Errors);
			return rezult;
		}

		//public async Task<ManagerRezult> DeleteById(int id)
		//{
		//	var rezult = new ManagerRezult();
		//	var serviceRezult = await _service.DeleteById(id);
		//	rezult.Errors.AddRange(serviceRezult.Errors);
		//	return rezult;
		//}
	}
}
