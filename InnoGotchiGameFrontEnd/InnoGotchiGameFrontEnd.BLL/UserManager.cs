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
        private UserService _service;
        private IMapper _mapper;

        public UserManager(HttpClient client, IMapper mapper)
        {
            _service = new UserService(client);
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers(UserDTOSorter sorter, UserDTOFiltrator filtrator)
        {
            var dataSorter = _mapper.Map<UserSorter>(sorter);
            var dataFiltrator = _mapper.Map<UserFiltrator>(filtrator);
            var dataUsers = await _service.GetUsers(dataSorter, dataFiltrator);
            var users = _mapper.Map<IEnumerable<UserDTO>>(dataUsers);
            return users;
        }

        public async Task<IEnumerable<UserDTO>> GetUsersPage(int pageSize, int pageNumber, UserDTOSorter sorter, UserDTOFiltrator filtrator)
        {
            var dataSorter = _mapper.Map<UserSorter>(sorter);
            var dataFiltrator = _mapper.Map<UserFiltrator>(filtrator);
            var dataUsers = await _service.GetUsersPage(pageSize, pageNumber, dataSorter, dataFiltrator);
            var users = _mapper.Map<IEnumerable<UserDTO>>(dataUsers);
            return users;
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            var dataUsers = await _service.GetUserById(id);
            var user = _mapper.Map<UserDTO>(dataUsers);
            return user;
        }

        public async Task<UserDTO> GetAuthodizedUser()
        {
            UserDTO user;
            var dataUsers = await _service.GetAuthodizedUser();
            user = _mapper.Map<UserDTO>(dataUsers);
            return user;
        }

        public async Task<ManagerRezult> Create(AddUserDTOModel addModel)
        {
            var validator = new AddUserDTOValidator();
            var validationResult = validator.Validate(addModel);
            var rezult = new ManagerRezult(validationResult);
            if (validationResult.IsValid)
            {
                var addDataModel = _mapper.Map<AddUserModel>(addModel);
                var serviceRezult = await _service.Create(addDataModel);
                rezult.Errors.AddRange(serviceRezult.Errors);
            }
            return rezult;
        }

        public async Task<ManagerRezult> UpdateUserData(UpdateUserDTODataModel updateModel)
        {
            var validator = new UpdateUserDTODataValidator();
            var validationResult = validator.Validate(updateModel);
            var rezult = new ManagerRezult(validationResult);
            if (validationResult.IsValid)
            {
                var updateDataModel = _mapper.Map<UpdateUserDataModel>(updateModel);
                var serviceRezult = await _service.UpdateUserData(updateDataModel);
                rezult.Errors.AddRange(serviceRezult.Errors);
            }
            return rezult;
        }

        public async Task<ManagerRezult> UpdateUserPassword(UpdateUserDTOPasswordModel updateModel)
        {
            var validator = new UpdateUserDTOPasswordValidator();
            var validationResult = validator.Validate(updateModel);
            var rezult = new ManagerRezult(validationResult);
            if (validationResult.IsValid)
            {
                var updateDataModel = _mapper.Map<UpdateUserPasswordModel>(updateModel);
                var serviceRezult = await _service.UpdateUserPassword(updateDataModel);
                rezult.Errors.AddRange(serviceRezult.Errors);
            }
            return rezult;
        }

        public async Task<ManagerRezult> DeleteById(int id)
        {
            var rezult = new ManagerRezult();
            var serviceRezult = await _service.DeleteById(id);
            rezult.Errors.AddRange(serviceRezult.Errors);
            return rezult;
        }

        public async Task<AuthorizeModelDTO?> Authorize(string email, string password)
        {
            var token = await _service.Authorize(email, password);

            return _mapper.Map<AuthorizeModelDTO?>(token);
        }
    }
}
