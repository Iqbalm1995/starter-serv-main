using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace starter_serv.BindingModel.AuthLogin
{

    [ExcludeFromCodeCoverage]
    public class AuthBindingModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
