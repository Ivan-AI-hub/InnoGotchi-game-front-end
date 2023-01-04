using AutoMapper;
using InnoGotchiGameFrontEnd.BLL.ComandModels.Farm;
using InnoGotchiGameFrontEnd.BLL.ComandModels.Pet;
using InnoGotchiGameFrontEnd.BLL.ComandModels.User;
using InnoGotchiGameFrontEnd.BLL.Filtrators;
using InnoGotchiGameFrontEnd.BLL.Model;
using InnoGotchiGameFrontEnd.BLL.Model.Identity;
using InnoGotchiGameFrontEnd.BLL.Model.Pet;
using InnoGotchiGameFrontEnd.BLL.Sorters;
using InnoGotchiGameFrontEnd.DAL.Models;
using InnoGotchiGameFrontEnd.DAL.Models.Farms;
using InnoGotchiGameFrontEnd.DAL.Models.Pets;
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

            CreateMap<AuthorizeModel, AuthorizeModelDTO>().ReverseMap();

            CreateMap<PetFarm, PetFarmDTO>().ReverseMap();
            CreateMap<FarmSorter, FarmDTOSorter>().ReverseMap();
            CreateMap<FarmSortRule, FarmDTOSortRule>().ReverseMap();
            CreateMap<FarmFiltrator, FarmDTOFiltrator>().ReverseMap();
            CreateMap<AddFarmModel, AddFarmDTOModel>().ReverseMap();
            CreateMap<UpdateFarmModel, UpdateFarmDTOModel>().ReverseMap();


            CreateMap<ColaborationRequest, ColaborationRequestDTO>().ReverseMap();
            CreateMap<ColaborationRequestStatus, ColaborationRequestStatusDTO>().ReverseMap();

            CreateMap<Pet, PetDTO>().ReverseMap();
            CreateMap<PetStatistic, PetStatisticDTO>().ForMember(x => x.IsAlive, opt => opt.MapFrom(x => x.IsAlive)).ReverseMap();
            CreateMap<PetView, PetViewDTO>().ReverseMap();
            CreateMap<PetSorter, PetDTOSorter>().ReverseMap();
            CreateMap<PetSortRule, PetDTOSortRule>().ReverseMap();
            CreateMap<PetFiltrator, PetDTOFiltrator>().ReverseMap();
            CreateMap<AddPetModel, AddPetDTOModel>().ReverseMap();
            CreateMap<UpdatePetModel, UpdatePetDTOModel>().ReverseMap();

            CreateMap<Picture, PictureDTO>().ReverseMap();
        }
    }
}
