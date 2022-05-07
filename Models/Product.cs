using System.ComponentModel.DataAnnotations;

namespace dotnet_webapi_example.Models
{
    public class Product : BaseModel
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }
    }
}
