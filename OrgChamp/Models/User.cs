using System.ComponentModel.DataAnnotations;

namespace OrgChamp.Models
{
    public class User
    {
        public Guid UserId { get; set; }

        [Required]
        public required string UserName { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? AvatarUrl { get; set; }

        [Required]
        public required string Auth0Id { get; set; }

        [Required]
        public required DateTime FirstLogin { get; set; }

        [Required]
        public required DateTime LastSeen { get; set; }

        public UserDetails? UserDetails { get; set; }
        public List<TeamMember> Teams { get; set; } = new();
    }
}
