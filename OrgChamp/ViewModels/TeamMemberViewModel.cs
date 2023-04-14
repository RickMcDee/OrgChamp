namespace OrgChamp.ViewModels
{
    public class TeamMemberViewModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public TeamMemberRole Role { get; set; }
    }
}
