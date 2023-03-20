using AutoMapper;
using InnoGotchiGameFrontEnd.BLL.AggregatesModel.PetAggregate.BaseModels;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate.Comands;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate.Filtrators;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate.Sorters;

namespace InnoGotchiGameFrontEnd.BLL.AggregatesModel.PetAggregate
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

        public async Task<ManagerResult> CreateAsync(AddPetDTOModel addModel, CancellationToken cancellationToken = default)
        {
            var validator = new AddPetDTOValidator();
            var validationResult = validator.Validate(addModel);
            if (!validationResult.IsValid)
            {
                return new ManagerResult(validationResult);
            }

            var addDataModel = _mapper.Map<AddPetModel>(addModel);
            var serviceResult = await _petService.CreateAsync(addDataModel, cancellationToken);

            return new ManagerResult(serviceResult);
        }

        public async Task<ManagerResult> UpdateAsync(UpdatePetDTOModel updateModel, CancellationToken cancellationToken = default)
        {
            var validator = new UpdatePetDTOValidator();
            var validationResult = validator.Validate(updateModel);
            if (!validationResult.IsValid)
            {
                return new ManagerResult(validationResult);
            }

            var updateDataModel = _mapper.Map<UpdatePetModel>(updateModel);
            var serviceResult = await _petService.UpdateAsync(updateDataModel, cancellationToken);

            return new ManagerResult(serviceResult);
        }

        public async Task<ManagerResult> FeedAsync(PetDTO pet, CancellationToken cancellationToken = default)
        {
            var serviceResult = await _petService.FeedAsync(pet.Id, cancellationToken);

            if (!serviceResult.IsComplete)
            {
                return new ManagerResult(serviceResult);
            }

            pet.Statistic.Feed();
            return new ManagerResult();
        }

        public async Task<ManagerResult> GiveDrinkAsync(PetDTO pet, CancellationToken cancellationToken = default)
        {
            var serviceResult = await _petService.GiveDrinkAsync(pet.Id, cancellationToken);

            if (!serviceResult.IsComplete)
            {
                return new ManagerResult(serviceResult);
            }

            pet.Statistic.GiveDrink();
            return new ManagerResult();
        }

        public async Task<ManagerResult> SetDeadStatusAsync(PetDTO pet, CancellationToken cancellationToken = default)
        {
            pet.Statistic.DeadDate ??= DateTime.Now;
            var serviceResult = await _petService.SetDeadStatus(pet.Id, pet.Statistic.DeadDate.Value, cancellationToken);

            if (!serviceResult.IsComplete)
            {
                return new ManagerResult(serviceResult);
            }

            pet.Statistic.SetDeadStatus();
            return new ManagerResult();
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
            if (pet.Statistic.HappinessDayCount <= 0)
            {
                return;
            }

            if (pet.Statistic.HungerLevel == HungerLevel.Hunger || pet.Statistic.ThirstyLevel == ThirstyLevel.Thirsty)
            {
                pet.Statistic.ResetHappinesDay();
                _petService.ResetHappinessDay(pet.Id);
            }
        }

    }
}
