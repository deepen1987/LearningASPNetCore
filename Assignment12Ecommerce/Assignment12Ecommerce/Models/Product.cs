﻿using System.ComponentModel.DataAnnotations;

namespace Assignment12Ecommerce.Models
{
    public class Product
    {
        [Required(ErrorMessage = "{0} can't be blank.")]
        [Display(Name = "Product Code")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} should be between valid number")]
        public int ProductCode { get; set;}

        [Required(ErrorMessage = "{0} can't be blank")]
        [Display(Name = "Product Price")]
        [Range(1, double.MaxValue, ErrorMessage = "{0} should be between valid number")]
        public double Price { get; set; }

        [Required(ErrorMessage = "{0} can't be blank.")]
        [Display(Name = "Product Quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} should be between valid number")]
        public int Quantity { get; set; }
    }
}
