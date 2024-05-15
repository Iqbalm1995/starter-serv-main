using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace starter_serv.BindingModel.Users
{
    [ExcludeFromCodeCoverage]
    public class UpdateUserBindingModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }
    }
}
