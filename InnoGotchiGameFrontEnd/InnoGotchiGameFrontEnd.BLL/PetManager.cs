
using AutoMapper;
using InnoGotchiGameFrontEnd.BLL.ComandModels.Pet;
using InnoGotchiGameFrontEnd.BLL.Filtrators;
using InnoGotchiGameFrontEnd.BLL.Model.Pet;
using InnoGotchiGameFrontEnd.BLL.Sorters;
using InnoGotchiGameFrontEnd.BLL.Validators.Pets;
using InnoGotchiGameFrontEnd.DAL.Models.Pets;
using InnoGotchiGameFrontEnd.DAL.Services;

namespace InnoGotchiGameFrontEnd.BLL
{
    public class PetManager
	{
		private PetService _service;
		private IMapper _mapper;

		public PetManager(HttpClient client, IMapper mapper)
		{
			_service = new PetService(client);
			_mapper = mapper;
		}


		public async Task<IEnumerable<PetDTO>> GetAllPets(PetDTOSorter sorter, PetDTOFiltrator filtrator)
		{
			var dataSorter = _mapper.Map<PetSorter>(sorter);
			var dataFiltrator = _mapper.Map<PetFiltrator>(filtrator);
			var dataPets = await _service.GetPets(dataSorter, dataFiltrator);
			var pets = _mapper.Map<IEnumerable<PetDTO>>(dataPets);
			CheckHappinessStatus(pets);
			return pets;
		}

		public async Task<IEnumerable<PetDTO>> GetPetsPage(int pageSize, int pageNumber, PetDTOSorter sorter, PetDTOFiltrator filtrator)
		{
			var dataSorter = _mapper.Map<PetSorter>(sorter);
			var dataFiltrator = _mapper.Map<PetFiltrator>(filtrator);
			var dataPets = await _service.GetPetsPage(pageSize, pageNumber, dataSorter, dataFiltrator);
			var pets = _mapper.Map<IEnumerable<PetDTO>>(dataPets);
            CheckHappinessStatus(pets);
            return pets;
		}

		public async Task<PetDTO> GetPetById(int id)
		{
			var dataPets = await _service.GetPetById(id);
			var pet = _mapper.Map<PetDTO>(dataPets);
            CheckHappinessStatus(pet);
            return pet;
		}

		public async Task<ManagerRezult> Create(AddPetDTOModel addModel)
		{
            var validator = new AddPetDTOValidator();
            var validationResult = validator.Validate(addModel);
            var rezult = new ManagerRezult(validationResult);
			if (validationResult.IsValid)
			{
				var addDataModel = _mapper.Map<AddPetModel>(addModel);
				var serviceRezult = await _service.Create(addDataModel);
				rezult.Errors.AddRange(serviceRezult.Errors);
			}
			return rezult;
		}

		public async Task<ManagerRezult> UpdatePet(UpdatePetDTOModel updateModel)
		{
            var validator = new UpdatePetDTOValidator();
            var validationResult = validator.Validate(updateModel);
            var rezult = new ManagerRezult(validationResult);
			if (validationResult.IsValid)
			{
				var updateDataModel = _mapper.Map<UpdatePetModel>(updateModel);
				var serviceRezult = await _service.UpdatePet(updateDataModel);
				rezult.Errors.AddRange(serviceRezult.Errors);
			}
			return rezult;
		}

		public async Task<ManagerRezult> Feed(PetDTO pet)
		{
			var rezult = new ManagerRezult();
			var serviceRezult = await _service.Feed(pet.Id);
			rezult.Errors.AddRange(serviceRezult.Errors);

            if (rezult.IsComplete)
            {
                pet.Statistic.HungerLevel = HungerLevel.Full;
                pet.Statistic.FeedingCount++;
                pet.Statistic.DateLastFeed = DateTime.UtcNow;
            }

            return rezult;
		}

		public async Task<ManagerRezult> GiveDrink(PetDTO pet)
		{
			var rezult = new ManagerRezult();
			var serviceRezult = await _service.GiveDrink(pet.Id);
			rezult.Errors.AddRange(serviceRezult.Errors);

			if(rezult.IsComplete)
			{
                pet.Statistic.ThirstyLevel = ThirstyLevel.Full;
                pet.Statistic.DrinkingCount++;
                pet.Statistic.DateLastDrink = DateTime.UtcNow;
            }

			return rezult;
		}

		public async Task<ManagerRezult> SetDeadStatus(PetDTO pet)
		{
			var rezult = new ManagerRezult();
			ServiceRezult serviceRezult;
			if(pet.Statistic.DeadDate != null)
				serviceRezult = await _service.SetDeadStatus(pet.Id, pet.Statistic.DeadDate.Value);
			else
                serviceRezult = await _service.SetDeadStatus(pet.Id, DateTime.Now);

            rezult.Errors.AddRange(serviceRezult.Errors);

			if(rezult.IsComplete)
			{
				pet.Statistic.IsAlive = false;
				pet.Statistic.DeadDate = pet.Statistic.DeadDate ?? DateTime.UtcNow;
			}
			return rezult;
		}

        private void CheckHappinessStatus(IEnumerable<PetDTO> pets)
		{
			foreach (var pet in pets)
			{
				CheckHappinessStatus(pet);
			}
		}

        private void CheckHappinessStatus(PetDTO pet)
		{
			if (pet.Statistic.HappinessDayCount > 0)
			{
				if (pet.Statistic.HungerLevel == HungerLevel.Hunger || pet.Statistic.ThirstyLevel == ThirstyLevel.Thirsty)
				{
                    pet.Statistic.FirstHappinessDay = DateTime.UtcNow;
                    _service.ResetHappinessDay(pet.Id);
				}
			}
		}

    }
}
