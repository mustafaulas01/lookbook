using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dto;
using Business;
using Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly INebimIntegrationService _nebimIntegrationService;
        private readonly string cacheKey="product_list";

        public ProductsController(INebimIntegrationService nebimIntegrationService,IMemoryCache memoryCache)
        {
            _nebimIntegrationService=nebimIntegrationService;
            _memoryCache=memoryCache;
        }


        [HttpGet]
        public IActionResult GetAllProducts()
        {
        
            List<GetB2BProductsResponse> productList=new List<GetB2BProductsResponse>();
            var nebimParameter = new ProcedureRequest
            {
                ProcName = "usp_B2B_Products",
                Parameters = new List<ProcParameterModel>
                {

                }
            };
        
            if (!_memoryCache.TryGetValue(cacheKey, out productList))
            {
               productList= _nebimIntegrationService.RunNebimProc<List<GetB2BProductsResponse>>(nebimParameter);


                var cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(12)).SetPriority(CacheItemPriority.Normal)
                    .SetAbsoluteExpiration(TimeSpan.FromHours(12));

                _memoryCache.Set(cacheKey, productList, cacheOptions);
            }
      

            if (productList.Any())
            return Ok(productList);
            else 
            return NotFound();
        }
    }
}