using Google.Protobuf.WellKnownTypes;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace starter_serv.Model
{
    public class Roles
    {
        [Key]
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
