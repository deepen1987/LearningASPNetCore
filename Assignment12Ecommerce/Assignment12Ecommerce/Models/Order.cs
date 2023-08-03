using Assignment12Ecommerce.CustomValidators;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Assignment12Ecommerce.Models
{
    public class Order
    {
        [BindNever]
        public int OrderNo { get; set; } = new Random().Next(1, 99999);

        [Required(ErrorMessage = "{0} can't be blank.")]
        [Display(Name = "Order Date")]
        [MinimumDateValidatorAttribute("2000-01-01", ErrorMessage = "{1} should be greatr than or equal to {0}")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        [Display(Name = "Invoice Price")]
        [Range(1, double.MaxValue, ErrorMessage = "{0} should be between a valid number")]
        [InvoicePriceValidatorAttribute("Products")]
        public double InvoicePrice { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
