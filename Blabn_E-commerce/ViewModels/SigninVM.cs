using System.ComponentModel.DataAnnotations;

namespace Blabn_E_commerce.ViewModels
{
    public class SigninVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
