using System.ComponentModel.DataAnnotations;

namespace OrgChamp.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel()
        {

        }

        public UserViewModel(UserViewModel original)
        {
            Id = original.Id;
            UserName = original.UserName;
            FirstName = original.FirstName;
            LastName = original.LastName;
            Email = original.Email;
            Homepage = original.Homepage;
            AvatarUrl = original.AvatarUrl;
            Address = new AddressViewModel(original.Address);
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, ErrorMessage = "Name length can't be more than 20.")]
        public string UserName { get; set; } = null!;

        [StringLength(20, ErrorMessage = "Name length can't be more than 20.")]
        public string? FirstName { get; set; }

        [StringLength(20, ErrorMessage = "Name length can't be more than 20.")]
        public string? LastName { get; set; }

        [EmailAddress(ErrorMessage = "The Email address efield is not a valid e-mail address.")]
        public string? Email { get; set; }

        //[Url]
        public string? Homepage { get; set; }

        [Url]
        public string? AvatarUrl { get; set; }

        [ValidateComplexType]
        public AddressViewModel Address { get; set; } = new();

        public List<TeamViewModel> Teams { get; set; } = new();

        public override bool Equals(object? obj)
        {
            if (obj is UserViewModel user)
            {
                return Id == user.Id
                    && UserName == user.UserName
                    && FirstName == user.FirstName
                    && LastName == user.LastName
                    && Email == user.Email
                    && Homepage == user.Homepage
                    && AvatarUrl == user.AvatarUrl
                    && Address.Equals(user.Address);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
