using System.ComponentModel.DataAnnotations;

namespace Blabn_E_commerce.ViewModels
{
    public class SignupVM
    {
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Full name")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
    }
}
