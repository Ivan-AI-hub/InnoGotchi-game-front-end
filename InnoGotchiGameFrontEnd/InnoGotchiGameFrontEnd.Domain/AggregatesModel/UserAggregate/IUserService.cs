using InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate.Comands;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate.Filtrators;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate.Sorters;

namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate
{
    public interface IUserService
    {
        Task<AuthorizeModel?> AuthorizeAsync(string email, string password, CancellationToken cancellationToken = default);
        Task<IServiceResult> CreateAsync(AddUserModel addModel, CancellationToken cancellationToken = default);
        Task<IServiceResult> DeleteByIdAsync(int userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> GetAsync(UserSorter? sorter = null, UserFiltrator? filtrator = null, CancellationToken cancellationToken = default);
        Task<User?> GetAuthodizedUserAsync(CancellationToken cancellationToken = default);
        Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> GetPageAsync(int pageSize, int pageNumber, UserSorter sorter, UserFiltrator filtrator, CancellationToken cancellationToken = default);
        Task<IServiceResult> UpdateDataAsync(UpdateUserDataModel updateModel, CancellationToken cancellationToken = default);
        Task<IServiceResult> UpdatePasswordAsync(UpdateUserPasswordModel updateModel, CancellationToken cancellationToken = default);
    }
}