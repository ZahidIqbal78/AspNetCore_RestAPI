using AspNetCore_RestAPI.Routes;
using dotnet_webapi_example.DTOs.V1.Requests;
using dotnet_webapi_example.DTOs.V1.Responses;
using dotnet_webapi_example.Models;
using dotnet_webapi_example.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_webapi_example.Controllers.V1
{
    //[Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet(ApiRoutes.Products.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(new ProductsGetAllResponse
            {
                Count = products.Count,
                Products = products
            });
        }

        [HttpGet(ApiRoutes.Products.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            if (product is null) return NotFound();
            return Ok(product);
        }

        [HttpPost(ApiRoutes.Products.Create)]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest productRequest)
        {
            var product = new Product
            {
                Name = productRequest.Name,
                Description = string.IsNullOrEmpty(productRequest.Description) ? null : productRequest.Description
            };

            await _productService.CreateProductAsync(product);
            var baseAddress = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseAddress + "/" + ApiRoutes.Products.Get.Replace("{productId}", product.Id.ToString());

            var response = new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = string.IsNullOrEmpty(product.Description) ? null : product.Description
            };
            return Created(locationUrl, response);
        }

        [HttpPut(ApiRoutes.Products.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid productId, [FromBody] UpdateProductRequest productRequest)
        {
            var product = new Product
            {
                Id = productId,
                Name = productRequest.Name,
                Description = string.IsNullOrEmpty(productRequest.Description) ? null : productRequest.Description
            };

            var isUpdated = await _productService.UpdateProductAsync(product);
            if (isUpdated) return Ok(product);

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Products.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid productId)
        {
            var isDeleted = await _productService.DeleteProductAsync(productId);
            if (!isDeleted) return NotFound();
            return NoContent();
        }
    }
}
