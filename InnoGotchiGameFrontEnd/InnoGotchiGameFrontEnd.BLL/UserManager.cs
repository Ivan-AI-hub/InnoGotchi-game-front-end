using AuthorizationInfrastructure.HttpClients;
using AutoMapper;
using InnoGotchiGameFrontEnd.BLL.ComandModels.User;
using InnoGotchiGameFrontEnd.BLL.Filtrators;
using InnoGotchiGameFrontEnd.BLL.Model;
using InnoGotchiGameFrontEnd.BLL.Model.Identity;
using InnoGotchiGameFrontEnd.BLL.Sorters;
using InnoGotchiGameFrontEnd.BLL.Validators.Users;
using InnoGotchiGameFrontEnd.DAL.Models.Users;
using InnoGotchiGameFrontEnd.DAL.Services;

namespace InnoGotchiGameFrontEnd.BLL
{
    public class UserManager
    {
        private UserService _userService;
        private IMapper _mapper;

        public UserManager(IAuthorizedClient client, IMapper mapper)
        {
            _userService = new UserService(client);
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> GetAsync(UserDTOSorter sorter, UserDTOFiltrator filtrator, CancellationToken cancellationToken = default)
        {
            var dataSorter = _mapper.Map<UserSorter>(sorter);
            var dataFiltrator = _mapper.Map<UserFiltrator>(filtrator);
            var dataUsers = await _userService.GetAsync(dataSorter, dataFiltrator, cancellationToken);
            var users = _mapper.Map<IEnumerable<UserDTO>>(dataUsers);
            return users;
        }

        public async Task<IEnumerable<UserDTO>> GetPageAsync(int pageSize, int pageNumber, UserDTOSorter sorter, UserDTOFiltrator filtrator, CancellationToken cancellationToken = default)
        {
            var dataSorter = _mapper.Map<UserSorter>(sorter);
            var dataFiltrator = _mapper.Map<UserFiltrator>(filtrator);
            var dataUsers = await _userService.GetPageAsync(pageSize, pageNumber, dataSorter, dataFiltrator, cancellationToken);
            var users = _mapper.Map<IEnumerable<UserDTO>>(dataUsers);
            return users;
        }

        public async Task<UserDTO> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var dataUsers = await _userService.GetByIdAsync(id, cancellationToken);
            var user = _mapper.Map<UserDTO>(dataUsers);
            return user;
        }

        public async Task<UserDTO> GetAuthodizedUserAsync(CancellationToken cancellationToken = default)
        {
            UserDTO user;
            var dataUsers = await _userService.GetAuthodizedUserAsync(cancellationToken);
            user = _mapper.Map<UserDTO>(dataUsers);
            return user;
        }

        public async Task<ManagerRezult> CreateAsync(AddUserDTOModel addModel, CancellationToken cancellationToken = default)
        {
            var validator = new AddUserDTOValidator();
            var validationResult = validator.Validate(addModel);
            var rezult = new ManagerRezult(validationResult);
            if (!validationResult.IsValid)
            {
                return rezult;
            }

            var addDataModel = _mapper.Map<AddUserModel>(addModel);
            var serviceRezult = await _userService.CreateAsync(addDataModel, cancellationToken);
            rezult.Errors.AddRange(serviceRezult.Errors);

            return rezult;
        }

        public async Task<ManagerRezult> UpdateDataAsync(UpdateUserDTODataModel updateModel, CancellationToken cancellationToken = default)
        {
            var validator = new UpdateUserDTODataValidator();
            var validationResult = validator.Validate(updateModel);
            var rezult = new ManagerRezult(validationResult);
            if (!validationResult.IsValid)
            {
                return rezult;
            }

            var updateDataModel = _mapper.Map<UpdateUserDataModel>(updateModel);
            var serviceRezult = await _userService.UpdateDataAsync(updateDataModel, cancellationToken);
            rezult.Errors.AddRange(serviceRezult.Errors);

            return rezult;
        }

        public async Task<ManagerRezult> UpdatePasswordAsync(UpdateUserDTOPasswordModel updateModel, CancellationToken cancellationToken = default)
        {
            var validator = new UpdateUserDTOPasswordValidator();
            var validationResult = validator.Validate(updateModel);
            var rezult = new ManagerRezult(validationResult);
            if (validationResult.IsValid)
            {
                var updateDataModel = _mapper.Map<UpdateUserPasswordModel>(updateModel);
                var serviceRezult = await _userService.UpdatePasswordAsync(updateDataModel,cancellationToken);
                rezult.Errors.AddRange(serviceRezult.Errors);
            }
            return rezult;
        }

        public async Task<ManagerRezult> DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var rezult = new ManagerRezult();
            var serviceRezult = await _userService.DeleteByIdAsync(id,cancellationToken);
            rezult.Errors.AddRange(serviceRezult.Errors);
            return rezult;
        }

        public async Task<AuthorizeModelDTO?> Authorize(string email, string password, CancellationToken cancellationToken = default)
        {
            var token = await _userService.AuthorizeAsync(email, password,cancellationToken);

            return _mapper.Map<AuthorizeModelDTO?>(token);
        }
    }
}
