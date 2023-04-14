using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrgChamp.Models;

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
            var result = mapper.Map<IEnumerable<TeamViewModel>>(teams);

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

        internal IEnumerable<TeamMemberViewModel> GetTeamMember(Guid teamId)
        {
            // This is synchronous on purpose -> TeamManagement.razor cant handle async in Razor code
            using var context = dbContextFactory.CreateDbContext();
            var teamMember = context.TeamMembers
                .Include(i => i.User)
                .Where(i => i.TeamId == teamId);
            var result = mapper.Map<IEnumerable<TeamMemberViewModel>>(teamMember);

            return result;
        }

        internal async Task RemoveUserFromTeam(Guid teamId, Guid userId)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();
            var teamMember = await context.TeamMembers.FirstAsync(i => i.TeamId == teamId && i.UserId == userId);

            if (teamMember.Role == TeamMemberRole.Owner)
            {
                throw new InvalidOperationException("Cannot remove owner from team. Team has to be deleted");
            }

            context.TeamMembers.Remove(teamMember);
            await context.SaveChangesAsync();
        }

        internal async Task AddUserToTeam(Guid teamId, Guid userId, TeamMemberRole role = TeamMemberRole.Guest)
        {
            using var context = await dbContextFactory.CreateDbContextAsync();
            if (await context.TeamMembers.AnyAsync(i => i.TeamId == teamId && i.UserId == userId))
            {
                throw new InvalidOperationException("User is already member of this team");
            }

            var user = await context.Users.FindAsync(userId);
            await context.TeamMembers.AddAsync(new TeamMember
            {
                TeamMemberId = Guid.NewGuid(),
                TeamMemberName = user!.UserName,
                TeamId = teamId,
                Role = role,
                UserId = user.UserId,
            });
            await context.SaveChangesAsync();
        }

        #endregion
    }
}
