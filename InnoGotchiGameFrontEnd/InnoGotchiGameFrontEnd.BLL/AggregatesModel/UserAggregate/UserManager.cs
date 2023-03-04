using AutoMapper;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate.Comands;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate.Filtrators;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate.Sorters;

namespace InnoGotchiGameFrontEnd.BLL.AggregatesModel.UserAggregate
{
    public class UserManager
    {
        private IUserService _userService;
        private IMapper _mapper;

        public UserManager(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> GetAsync(UserDTOSorter sorter, UserDTOFiltrator filtrator, CancellationToken cancellationToken = default)
        {
            var dataSorter = _mapper.Map<UserSorter>(sorter);
            var dataFiltrator = _mapper.Map<UserFiltrator>(filtrator);
            var dataUsers = await _userService.GetAsync(dataSorter, dataFiltrator, cancellationToken);
            return _mapper.Map<IEnumerable<UserDTO>>(dataUsers);
        }

        public async Task<IEnumerable<UserDTO>> GetPageAsync(int pageSize, int pageNumber, UserDTOSorter sorter, UserDTOFiltrator filtrator, CancellationToken cancellationToken = default)
        {
            var dataSorter = _mapper.Map<UserSorter>(sorter);
            var dataFiltrator = _mapper.Map<UserFiltrator>(filtrator);
            var dataUsers = await _userService.GetPageAsync(pageSize, pageNumber, dataSorter, dataFiltrator, cancellationToken);
            return _mapper.Map<IEnumerable<UserDTO>>(dataUsers);
        }

        public async Task<UserDTO> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var dataUsers = await _userService.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<UserDTO>(dataUsers);
        }

        public async Task<UserDTO> GetAuthodizedUserAsync(CancellationToken cancellationToken = default)
        {
            var dataUsers = await _userService.GetAuthodizedUserAsync(cancellationToken);
            return _mapper.Map<UserDTO>(dataUsers);
        }

        public async Task<ManagerResult> CreateAsync(AddUserDTOModel addModel, CancellationToken cancellationToken = default)
        {
            var validator = new AddUserDTOValidator();
            var validationResult = validator.Validate(addModel);
            if (!validationResult.IsValid)
            {
                return new ManagerResult(validationResult);
            }

            var addDataModel = _mapper.Map<AddUserModel>(addModel);
            var serviceResult = await _userService.CreateAsync(addDataModel, cancellationToken);

            return new ManagerResult(serviceResult);
        }

        public async Task<ManagerResult> UpdateDataAsync(UpdateUserDTODataModel updateModel, CancellationToken cancellationToken = default)
        {
            var validator = new UpdateUserDTODataValidator();
            var validationResult = validator.Validate(updateModel);
            if (!validationResult.IsValid)
            {
                return new ManagerResult(validationResult);
            }

            var updateDataModel = _mapper.Map<UpdateUserDataModel>(updateModel);
            var serviceResult = await _userService.UpdateDataAsync(updateDataModel, cancellationToken);

            return new ManagerResult(serviceResult);
        }

        public async Task<ManagerResult> UpdatePasswordAsync(UpdateUserDTOPasswordModel updateModel, CancellationToken cancellationToken = default)
        {
            var validator = new UpdateUserDTOPasswordValidator();
            var validationResult = validator.Validate(updateModel);

            if (!validationResult.IsValid)
            {
                return new ManagerResult(validationResult);
            }

            var updateDataModel = _mapper.Map<UpdateUserPasswordModel>(updateModel);
            var serviceResult = await _userService.UpdatePasswordAsync(updateDataModel, cancellationToken);

            return new ManagerResult(serviceResult);
        }

        public async Task<ManagerResult> DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var serviceResult = await _userService.DeleteByIdAsync(id, cancellationToken);
            return new ManagerResult(serviceResult);
        }

        public async Task<AuthorizeModelDTO?> Authorize(string email, string password, CancellationToken cancellationToken = default)
        {
            var token = await _userService.AuthorizeAsync(email, password, cancellationToken);

            return _mapper.Map<AuthorizeModelDTO?>(token);
        }
    }
}
