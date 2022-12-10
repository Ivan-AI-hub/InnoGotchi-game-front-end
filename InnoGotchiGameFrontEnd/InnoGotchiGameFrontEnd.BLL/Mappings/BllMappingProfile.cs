using AutoMapper;
using InnoGotchiGameFrontEnd.BLL.ComandModels.User;
using InnoGotchiGameFrontEnd.BLL.Filtrators;
using InnoGotchiGameFrontEnd.BLL.Model;
using InnoGotchiGameFrontEnd.BLL.Sorters;
using InnoGotchiGameFrontEnd.DAL.Models;
using InnoGotchiGameFrontEnd.DAL.Models.Users;

namespace InnoGotchiGameFrontEnd.BLL.Mappings
{
    public class BllMappingProfile : Profile
    {
        public BllMappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserFiltrator, UserDTOFiltrator>().ReverseMap();
            CreateMap<UserSorter, UserDTOSorter>().ReverseMap();
            CreateMap<UserSortRule, UserDTOSortRule>().ReverseMap();
            CreateMap<AddUserModel, AddUserDTOModel>().ReverseMap();
            CreateMap<UpdateUserDataModel, UpdateUserDTODataModel>().ReverseMap();
            CreateMap<UpdateUserPasswordModel, UpdateUserDTOPasswordModel>().ReverseMap();

            CreateMap<PetFarm, PetFarmDTO>().ReverseMap();
            CreateMap<ColaborationRequest, ColaborationRequestDTO>().ReverseMap();
            CreateMap<ColaborationRequestStatus, ColaborationRequestStatusDTO>().ReverseMap();

            CreateMap<Pet, PetDTO>().ReverseMap();
            CreateMap<PetStatistic, PetStatisticDTO>().ReverseMap();
            CreateMap<PetView, PetViewDTO>().ReverseMap();
        }
    }
}
