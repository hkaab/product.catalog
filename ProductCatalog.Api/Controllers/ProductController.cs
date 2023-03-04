using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polly;
using ProductCatalog.Api.Authorization;
using ProductCatalog.Interfaces;
using ProductCatalog.Models;
using ProductCatalog.Models.Enums;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using static ProductCatalog.Library.Constants;

namespace ProductCatalog.Api.Controllers
{
    [Route(EndpointsConstants.Products)]
    [ApiController]
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService, ILogger<ProductController> logger) : base(logger)
        {
            _productService = productService;
        }

        [HttpGet]
        [Authorize(Policy = ApiPermissions.OnlineWooliesApp)]
        [SwaggerOperation(Description = "Get Products", OperationId = "Products_GET")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IEnumerable<Product>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ErrorResponse), description: "Invalid GetProductsRequest")]
        [SwaggerResponse(StatusCodes.Status404NotFound, type: typeof(ErrorResponse), description: "Product not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse), description: "Get Product failed")]
        [SwaggerResponse(StatusCodes.Status429TooManyRequests, type: typeof(ErrorResponse))]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsAsync(CancellationToken cancellationToken,[FromQuery] string? sortAttribute = "Name", [FromQuery] SortOptions? sortOptions = SortOptions.Ascending)
        {
            var result = await _productService.GetProductsAsync(sortAttribute,sortOptions,cancellationToken);
            return Ok(result);
        }

    }
}
