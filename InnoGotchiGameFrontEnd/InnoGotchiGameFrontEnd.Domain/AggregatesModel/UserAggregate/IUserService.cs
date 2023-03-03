using InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate.Comands;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate.Filtrators;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate.Sorters;

namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate
{
    public interface IUserService
    {
        Task<AuthorizeModel?> AuthorizeAsync(string email, string password, CancellationToken cancellationToken = default);
        Task<IServiceRezult> CreateAsync(AddUserModel addModel, CancellationToken cancellationToken = default);
        Task<IServiceRezult> DeleteByIdAsync(int userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> GetAsync(UserSorter? sorter = null, UserFiltrator? filtrator = null, CancellationToken cancellationToken = default);
        Task<User?> GetAuthodizedUserAsync(CancellationToken cancellationToken = default);
        Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> GetPageAsync(int pageSize, int pageNumber, UserSorter sorter, UserFiltrator filtrator, CancellationToken cancellationToken = default);
        Task<IServiceRezult> UpdateDataAsync(UpdateUserDataModel updateModel, CancellationToken cancellationToken = default);
        Task<IServiceRezult> UpdatePasswordAsync(UpdateUserPasswordModel updateModel, CancellationToken cancellationToken = default);
    }
}