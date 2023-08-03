using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Assignment12Ecommerce.CustomValidators
{
    public class MinimumDateValidatorAttribute : ValidationAttribute
    {
        public DateTime MinimumDate { get; set; } = Convert.ToDateTime("1990-01-01");
        public string DefaultErrorMessage { get; set; } = "Date shoule be less than {0}";
        public MinimumDateValidatorAttribute() { }
        public MinimumDateValidatorAttribute(string minimumDate)
        {
            MinimumDate = Convert.ToDateTime(minimumDate);
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date = (DateTime)value;

                if (date <= MinimumDate)
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, MinimumDate.ToString("yyyy-MM-dd"), validationContext.DisplayName));
                }
                else
                {
                    return ValidationResult.Success;
                }
            }

            return null;
        }
    }
}
