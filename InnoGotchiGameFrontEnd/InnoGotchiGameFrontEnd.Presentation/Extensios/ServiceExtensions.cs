using InnoGotchiGameFrontEnd.BLL;
using InnoGotchiGameFrontEnd.DAL.Services;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.ColaborationRequestAggregate;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PictureAggregate;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate;

namespace InnoGotchiGameFrontEnd.Presentation.Extensios
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IColaborationRequestService, ColaborationRequestService>();
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IPictureService, PictureService>();
            services.AddScoped<IUserService, UserService>();
        }
        public static void ConfigureManagers(this IServiceCollection services)
        {
            services.AddScoped<UserManager>();
            services.AddScoped<ColaborationRequestManager>();
            services.AddScoped<FarmManager>();
            services.AddScoped<PetManager>();
            services.AddScoped<PictureManager>();
        }
    }
}
