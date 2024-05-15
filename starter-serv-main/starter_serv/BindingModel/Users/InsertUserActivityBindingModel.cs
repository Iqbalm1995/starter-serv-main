using starter_serv.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace starter_serv.BindingModel
{
    [ExcludeFromCodeCoverage]
    public class InsertUserActivityBindingModel
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        public string Module { get; set; } = null!;

        public int? SelfParentId { get; set; }

        [Required]
        public int ModuleFieldId { get; set; }

        [Required]
        public string Action { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public string IpAddress { get; set; } = null!;

        public string? DiffForHumans { get; set; }
    }
}
