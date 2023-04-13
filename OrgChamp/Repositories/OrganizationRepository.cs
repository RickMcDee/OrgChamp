using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace OrgChamp.Repositories
{
    public class OrganizationRepository
    {
        private readonly IDbContextFactory<DatabaseContext> dbContextFactory;
        private readonly IMapper mapper;

        public OrganizationRepository(IDbContextFactory<DatabaseContext> dbContextFactory, IMapper mapper)
        {
            this.dbContextFactory = dbContextFactory;
            this.mapper = mapper;
        }

        #region Teams

        internal async Task<IEnumerable<TeamViewModel>> GetTeams()
        {
            using var context = dbContextFactory.CreateDbContext();
            var teams = await context.Teams.Where(i => i.IsActive).ToListAsync();
            var result = mapper.Map<List<Team>, IEnumerable<TeamViewModel>>(teams);

            return result;
        }

        internal async Task<Guid> SaveTeam(TeamViewModel team, UserViewModel creatingUser)
        {
            using var context = dbContextFactory.CreateDbContext();
            var entity = await context.Teams.FindAsync(team.TeamId);

            if (entity is null)
            {
                entity = new Team
                {
                    TeamName = team.TeamName,
                    CreatedById = creatingUser.Id,
                    CreatedAt = DateTime.Now,
                };

                entity.Member.Add(new TeamMember
                {
                    TeamMemberId = Guid.NewGuid(),
                    TeamMemberName = creatingUser.UserName,
                    TeamId = entity.TeamId,
                    Role = Enums.TeamMemberRole.Owner,
                    UserId = creatingUser.Id,
                });

                await context.Teams.AddAsync(entity);
            }

            entity.TeamDescription = team.TeamDescription;
            await context.SaveChangesAsync();

            return entity.TeamId;
        }

        #endregion
    }
}
