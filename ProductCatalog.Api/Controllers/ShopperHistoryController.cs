using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polly;
using ProductCatalog.Api.Authorization;
using ProductCatalog.Interfaces;
using ProductCatalog.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using static ProductCatalog.Library.Constants;

namespace ProductCatalog.Api.Controllers
{
    [Route(EndpointsConstants.ShopperHistory)]
    [ApiController]
    public class ShopperHistoryController : BaseApiController
    {
        private readonly IShopperHistoryService _shopperHistoryService;
        public ShopperHistoryController(IShopperHistoryService shopperHistoryService, ILogger<ProductController> logger) : base(logger)
        {
            _shopperHistoryService = shopperHistoryService;
        }

        [HttpGet]
        [Authorize(Policy = ApiPermissions.OnlineWooliesApp)]
        [SwaggerOperation(Description = "Get Shopper History", OperationId = "ShopperHistory_GET")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IEnumerable<Product>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ErrorResponse), description: "Invalid Get Shopper History Request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, type: typeof(ErrorResponse), description: "Shopper History not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse), description: "Get Shopper History failed")]
        [SwaggerResponse(StatusCodes.Status429TooManyRequests, type: typeof(ErrorResponse))]
        public async Task<ActionResult<IEnumerable<ShopperHistory>>> GetShoppersHistoryAsync(CancellationToken cancellationToken)
        {
            
            var result = await _shopperHistoryService.GetShoppersHistoryAsync(cancellationToken);
            return Ok(result);
        }


    }
}
