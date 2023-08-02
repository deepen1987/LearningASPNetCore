using Microsoft.AspNetCore.Mvc;

namespace ModelBindingValidationExample.Models
{
    public class Books
    {
        //[FromRoute]
        public int BookID { get; set; }
        public string? Author { get; set; }
        public List<string?> Tags { get; set; } = new List<string?>();
    }
}
