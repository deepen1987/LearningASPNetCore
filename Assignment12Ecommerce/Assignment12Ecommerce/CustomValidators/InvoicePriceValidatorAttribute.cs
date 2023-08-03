using Assignment12Ecommerce.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Assignment12Ecommerce.CustomValidators
{
    public class InvoicePriceValidatorAttribute : ValidationAttribute
    {
        public string OtherPropertyName { get; set; }
        public string DefaultErrorMessage { get; set; } = "Invoice Price should be equal to the total cost of all products (i.e. {0}) in the order.";
        public InvoicePriceValidatorAttribute(string otherPropertyName)
        {
            OtherPropertyName = otherPropertyName;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            double actualPrice = (double)value;
            double totalPrice = 0;
            if (value != null)
            {
                PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(nameof(Order.Products));
                if (otherProperty != null)
                {
                    List<Product> products = (List<Product>)otherProperty.GetValue(validationContext.ObjectInstance);
                    foreach (var product in products){
                        totalPrice += product.Price * product.Quantity;
                    }
                    if(totalPrice > 0)
                    {
                        if(totalPrice  != actualPrice){
                            return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, totalPrice));
                        }
                    }
                    else
                    {
                        return new ValidationResult("No Products found to validate invoice price.");
                    }

                    return ValidationResult.Success;
                }
                else
                {
                    return null;
                }
            }

            return null;
        }
    }
}
