using AutoMapper;

namespace OrgChamp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // DTO to ViewModel
            CreateMap<Team, TeamViewModel>();
            CreateMap<TeamMember, TeamMemberViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TeamMemberId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.TeamMemberName}{(src.User == null ? string.Empty : $" ({src.User.LastName}, {src.User.FirstName})")}"));
            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserDetails == null ? string.Empty : src.UserDetails.Email))
                .ForMember(dest => dest.Homepage, opt => opt.MapFrom(src => src.UserDetails == null ? string.Empty : src.UserDetails.Homepage))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.UserDetails))
                .ForMember(dest => dest.Teams, opt => opt.Ignore());
            CreateMap<UserDetails, AddressViewModel>();

            // ViewModel to DTO
            CreateMap<UserViewModel, User>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
