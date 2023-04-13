namespace OrgChamp.Services
{
    public class UserService
    {
        private readonly UserRepository userRepository;

        public Guid UserId { get; private set; }
        public UserViewModel User { get; private set; } = default!;

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        internal async Task SetUser(Guid userId)
        {
            UserId = userId;
            User = await userRepository.GetUserById(userId);
        }

        internal void SetUser(UserViewModel user)
        {
            UserId = user.Id;
            User = user;
        }

        internal async Task ReloadUserDetails()
        {
            User = await userRepository.GetUserById(UserId);
        }
    }
}
