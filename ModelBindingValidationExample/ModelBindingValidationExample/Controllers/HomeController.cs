﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using ModelBindingValidationExample.CustomModelBinders;
using ModelBindingValidationExample.Models;

namespace ModelBindingValidationExample.Controllers
{
    [Controller]
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return Ok("You have reached Home");
        }

        [Route("bookstore/{bookid}")]
        public IActionResult BookStore(int? bookid, [FromQuery] bool? isloggedin, Books book)
        {
            if (bookid.HasValue == false)
            { 
                return BadRequest("Book id is not supplied.");
            }

            if (!bookid.HasValue)
            {
                return BadRequest("Bookid can't be null or empty.");
            }

            if (!isloggedin.HasValue)
            {
                return BadRequest("IsLoggedin can't be null or empty.");
            }

            return Ok($"Successful it is ... {bookid} Book: {book} ollo has logged in {isloggedin}, {book.Comments}");
        }
    }
}
