using dotnet_webapi_example.Models;

namespace dotnet_webapi_example.DTOs.V1.Responses
{
    public class ProductsGetAllResponse
    {
        public int Count { get; set; }
        public List<Product> Products { get; set; }
    }
}
