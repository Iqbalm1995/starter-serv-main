using System.ComponentModel.DataAnnotations;

namespace starter_serv.Model
{
    public class Menus
    {
        [Key]
        public Int64 Id { get; set; }
        public Int64 ParentMenuId { get; set; }
        public string? Module { get; set; }
        public string Label { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
        public int Order { get; set; }
        public int Subscription { get; set; }
        public int Status { get; set; }
    }
}