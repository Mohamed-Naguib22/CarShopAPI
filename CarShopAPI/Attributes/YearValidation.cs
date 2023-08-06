using System.ComponentModel.DataAnnotations;

namespace CarShopAPI.Attributes
{
    public class YearValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (value is int year && year <= DateTime.Now.Year)
                {
                    if (year.ToString().Length != 4)
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                }
                else
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
