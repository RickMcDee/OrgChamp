namespace OrgChamp.Services
{
    public class UserService
    {
        public Guid UserId { get; private set; }

        internal void SetUserId(Guid userId)
        {
            UserId = userId;
        }
    }
}
