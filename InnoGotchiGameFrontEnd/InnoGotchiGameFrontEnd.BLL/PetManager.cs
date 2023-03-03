using AutoMapper;
using InnoGotchiGameFrontEnd.BLL.ComandModels.Pet;
using InnoGotchiGameFrontEnd.BLL.Filtrators;
using InnoGotchiGameFrontEnd.BLL.Model.Pet;
using InnoGotchiGameFrontEnd.BLL.Sorters;
using InnoGotchiGameFrontEnd.BLL.Validators.Pets;
using InnoGotchiGameFrontEnd.Domain;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate.Comands;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate.Filtrators;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate.Sorters;

namespace InnoGotchiGameFrontEnd.BLL
{
    public class PetManager
    {
        private IPetService _petService;
        private IMapper _mapper;

        public PetManager(IPetService petService, IMapper mapper)
        {
            _petService = petService;
            _mapper = mapper;
        }


        public async Task<IEnumerable<PetDTO>> GetAsync(PetDTOSorter sorter, PetDTOFiltrator filtrator, CancellationToken cancellationToken = default)
        {
            var dataSorter = _mapper.Map<PetSorter>(sorter);
            var dataFiltrator = _mapper.Map<PetFiltrator>(filtrator);
            var dataPets = await _petService.GetAsync(dataSorter, dataFiltrator, cancellationToken);
            var pets = _mapper.Map<IEnumerable<PetDTO>>(dataPets);
            CheckHappinessStatus(pets);
            return pets;
        }

        public async Task<IEnumerable<PetDTO>> GetPageAsync(int pageSize, int pageNumber, PetDTOSorter sorter, PetDTOFiltrator filtrator, CancellationToken cancellationToken = default)
        {
            var dataSorter = _mapper.Map<PetSorter>(sorter);
            var dataFiltrator = _mapper.Map<PetFiltrator>(filtrator);
            var dataPets = await _petService.GetPageAsync(pageSize, pageNumber, dataSorter, dataFiltrator, cancellationToken);
            var pets = _mapper.Map<IEnumerable<PetDTO>>(dataPets);
            CheckHappinessStatus(pets);
            return pets;
        }

        public async Task<PetDTO> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var dataPets = await _petService.GetByIdAsync(id, cancellationToken);
            var pet = _mapper.Map<PetDTO>(dataPets);
            CheckHappinessStatus(pet);
            return pet;
        }

        public async Task<ManagerRezult> CreateAsync(AddPetDTOModel addModel, CancellationToken cancellationToken = default)
        {
            var validator = new AddPetDTOValidator();
            var validationResult = validator.Validate(addModel);
            var rezult = new ManagerRezult(validationResult);
            if (validationResult.IsValid)
            {
                var addDataModel = _mapper.Map<AddPetModel>(addModel);
                var serviceRezult = await _petService.CreateAsync(addDataModel, cancellationToken);
                rezult.Errors.AddRange(serviceRezult.Errors);
            }
            return rezult;
        }

        public async Task<ManagerRezult> UpdateAsync(UpdatePetDTOModel updateModel, CancellationToken cancellationToken = default)
        {
            var validator = new UpdatePetDTOValidator();
            var validationResult = validator.Validate(updateModel);
            var rezult = new ManagerRezult(validationResult);
            if (validationResult.IsValid)
            {
                var updateDataModel = _mapper.Map<UpdatePetModel>(updateModel);
                var serviceRezult = await _petService.UpdateAsync(updateDataModel, cancellationToken);
                rezult.Errors.AddRange(serviceRezult.Errors);
            }
            return rezult;
        }

        public async Task<ManagerRezult> FeedAsync(PetDTO pet, CancellationToken cancellationToken = default)
        {
            var rezult = new ManagerRezult();
            var serviceRezult = await _petService.FeedAsync(pet.Id, cancellationToken);
            rezult.Errors.AddRange(serviceRezult.Errors);

            if (rezult.IsComplete)
            {
                pet.Statistic.HungerLevel = HungerLevel.Full;
                pet.Statistic.FeedingCount++;
                pet.Statistic.DateLastFeed = DateTime.UtcNow;
            }

            return rezult;
        }

        public async Task<ManagerRezult> GiveDrinkAsync(PetDTO pet, CancellationToken cancellationToken = default)
        {
            var rezult = new ManagerRezult();
            var serviceRezult = await _petService.GiveDrinkAsync(pet.Id, cancellationToken);
            rezult.Errors.AddRange(serviceRezult.Errors);

            if (rezult.IsComplete)
            {
                pet.Statistic.ThirstyLevel = ThirstyLevel.Full;
                pet.Statistic.DrinkingCount++;
                pet.Statistic.DateLastDrink = DateTime.UtcNow;
            }

            return rezult;
        }

        public async Task<ManagerRezult> SetDeadStatusAsync(PetDTO pet, CancellationToken cancellationToken = default)
        {
            var rezult = new ManagerRezult();
            IServiceRezult serviceRezult;
            if (pet.Statistic.DeadDate != null)
                serviceRezult = await _petService.SetDeadStatus(pet.Id, pet.Statistic.DeadDate.Value, cancellationToken);
            else
                serviceRezult = await _petService.SetDeadStatus(pet.Id, DateTime.Now, cancellationToken);

            rezult.Errors.AddRange(serviceRezult.Errors);

            if (rezult.IsComplete)
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
                    _petService.ResetHappinessDay(pet.Id);
                }
            }
        }

    }
}
