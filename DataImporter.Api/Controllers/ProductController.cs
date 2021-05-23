using DataImporter.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DataImporter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(long companyId, long feedId)
        {
            try
            {
                if (companyId > 0 && feedId > 0)
                {
                    var products = await _productService.GetProductByCompanyIdandFeedId(companyId, feedId);
                    return Ok(products);
                }
                else
                {
                    _logger.LogInformation($"Bad Request recieved. company Id - {companyId}, feedId - {feedId}");
                    return BadRequest("Please check the information provided");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Processing Request.");
                return StatusCode(500, "Error Processing Request.");
            }
        }
    }
}
