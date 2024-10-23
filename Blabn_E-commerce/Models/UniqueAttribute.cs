using System.ComponentModel.DataAnnotations;

namespace Blabn_E_commerce.Models
{
    public class UniqueAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null;
            }
            string NewEmail= value.ToString();
            Blabn_Context context = new Blabn_Context();
            User userfromDB = context.Users.FirstOrDefault(E=>E.Email==NewEmail);
            User userform = (User)validationContext.ObjectInstance;
            if (userfromDB != null) {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;

        }
    }
}
