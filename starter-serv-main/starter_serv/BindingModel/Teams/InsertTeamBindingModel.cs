using System.ComponentModel.DataAnnotations;

namespace starter_serv.BindingModel
{
    public class InsertTeamBindingModel
    {
        [Required]
        public string TeamName { get; set; } = null!;

        public int TeamLeader { get; set; }

        public string? Description { get; set; }

        public List<InsertTeamMemberBindingModel> TeamMember { get; set; }
    }

    public class InsertTeamMemberBindingModel
    {
        [Required]
        public int UserId { get; set; }
    }
}
