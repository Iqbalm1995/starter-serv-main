using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace starter_serv.BindingModel.Users
{
    [ExcludeFromCodeCoverage]
    public class InsertUserBindingModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public char StatusUser { get; set; }
    }
}
