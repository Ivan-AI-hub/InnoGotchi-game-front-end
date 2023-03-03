using AutoMapper;
using InnoGotchiGameFrontEnd.BLL.ComandModels.Farm;
using InnoGotchiGameFrontEnd.BLL.ComandModels.Pet;
using InnoGotchiGameFrontEnd.BLL.ComandModels.User;
using InnoGotchiGameFrontEnd.BLL.Filtrators;
using InnoGotchiGameFrontEnd.BLL.Model;
using InnoGotchiGameFrontEnd.BLL.Model.Identity;
using InnoGotchiGameFrontEnd.BLL.Model.Pet;
using InnoGotchiGameFrontEnd.BLL.Sorters;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.ColaborationRequestAggregate;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.FarmAggregate;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.FarmAggregate.Comands;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.FarmAggregate.Filtrators;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.FarmAggregate.Sorters;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate.Comands;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate.Filtrators;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate.Sorters;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PictureAggregate;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate.Comands;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate.Filtrators;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate.Sorters;

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
            CreateMap<PetDTOFiltrator, PetFiltrator>()
                .ForMember(x => x.DaysAlive, opt => opt.MapFrom(x => x.Age * 7))
                .ForMember(x => x.FeedingInterval, opt => opt.MapFrom(x => x.HungerLevel.HasValue ? DateToHungerLevelConvertor.GetInterval(x.HungerLevel.Value) : null))
                .ForMember(x => x.DrinkingInterval, opt => opt.MapFrom(x => x.ThirstyLevel.HasValue ? DateToThirstyLevelConvertor.GetInterval(x.ThirstyLevel.Value) : null));
            CreateMap<AddPetModel, AddPetDTOModel>().ReverseMap();
            CreateMap<UpdatePetModel, UpdatePetDTOModel>().ReverseMap();

            CreateMap<Picture, PictureDTO>().ReverseMap();
            CreateMap<PictureDTOFiltrator, PictureFiltrator>();
        }
    }
}
