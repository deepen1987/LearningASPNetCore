using System.ComponentModel.DataAnnotations;

namespace AppWeb.Core.Domain.Entities
{
    public class City
    {
        [Key]
        public Guid Id { get; set; }
        public string? CityName { get; set; }
    }
}
