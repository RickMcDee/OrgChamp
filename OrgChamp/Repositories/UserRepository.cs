using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace OrgChamp.Repositories
{
    public class UserRepository
    {
        private static readonly string AUTH0ID_CLAIM = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        private static readonly string NICKNAME_CLAIM = "nickname";
        private static readonly string GIVENNAME_CLAIM = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";
        private static readonly string SURNAME_CLAIM = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";
        private static readonly string AVATAR_CLAIM = "picture";

        private readonly IDbContextFactory<DatabaseContext> dbContextFactory;
        private readonly IMapper mapper;

        public UserRepository(IDbContextFactory<DatabaseContext> dbContextFactory, IMapper mapper)
        {
            this.dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        internal async Task<Guid> LoginUser(ClaimsPrincipal claimsPrincipal)
        {
            using var context = dbContextFactory.CreateDbContext();

            var externalProviderId = claimsPrincipal.FindFirst(AUTH0ID_CLAIM)!.Value;
            var user = await context.Users.FirstOrDefaultAsync(u => u.Auth0Id == externalProviderId);
            var timestamp = DateTime.Now;
            if (user == null)
            {
                user = new User
                {
                    UserId = Guid.NewGuid(),
                    Auth0Id = externalProviderId,
                    UserName = claimsPrincipal.FindFirst(NICKNAME_CLAIM)!.Value,
                    FirstName = claimsPrincipal.FindFirst(GIVENNAME_CLAIM)?.Value,
                    LastName = claimsPrincipal.FindFirst(SURNAME_CLAIM)?.Value,
                    AvatarUrl = claimsPrincipal.FindFirst(AVATAR_CLAIM)?.Value,
                    FirstLogin = timestamp,
                    LastSeen = timestamp,
                    UserDetails = new(),
                };

                await context.Users.AddAsync(user);
            }

            user.LastSeen = timestamp;
            await context.SaveChangesAsync();

            return user.UserId;
        }

        internal async Task<UserViewModel> GetUserById(Guid userId)
        {
            using var context = dbContextFactory.CreateDbContext();
            var user = await context.Users
                                                .Include(i => i.UserDetails)
                                                .SingleAsync(u => u.UserId == userId);

            var result = mapper.Map<UserViewModel>(user);

            return result;
        }

        internal async Task SaveUser(UserViewModel user)
        {
            if (user is null || user.Id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(user));
            }

            using var context = dbContextFactory.CreateDbContext();
            var entity = await context.Users
                                    .Include(i => i.UserDetails)
                                    .SingleAsync(u => u.UserId == user.Id);

            entity.FirstName = user.FirstName;
            entity.LastName = user.LastName;
            entity.AvatarUrl = user.AvatarUrl;
            entity.UserDetails ??= new();
            entity.UserDetails.Email = user.Email;
            entity.UserDetails.Homepage = user.Homepage;
            entity.UserDetails.Street = user.Address.Street;
            entity.UserDetails.Housenumber = user.Address.Housenumber;
            entity.UserDetails.Zipcode = user.Address.Zipcode;
            entity.UserDetails.City = user.Address.City;
            entity.UserDetails.Country = user.Address.Country;

            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
