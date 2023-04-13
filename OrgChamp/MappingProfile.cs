using AutoMapper;

namespace OrgChamp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // DTO to ViewModel
            CreateMap<User, UserViewModel>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserDetails == null ? string.Empty : src.UserDetails.Email))
                    .ForMember(dest => dest.Homepage, opt => opt.MapFrom(src => src.UserDetails == null ? string.Empty : src.UserDetails.Homepage))
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.UserDetails));
            CreateMap<UserDetails, AddressViewModel>();

            // ViewModel to DTO
            CreateMap<UserViewModel, User>()
                    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
