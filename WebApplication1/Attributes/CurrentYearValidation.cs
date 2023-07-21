using System.ComponentModel.DataAnnotations;

namespace CarShopAPI.Attributes
{
    public class CurrentYearValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                int year = (int)value;
                int currentYear = DateTime.Now.Year;

                if (year > currentYear)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
