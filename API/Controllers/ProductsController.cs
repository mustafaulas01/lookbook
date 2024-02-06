using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dto;
using Business;
using Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly INebimIntegrationService _nebimIntegrationService;
        public ProductsController(INebimIntegrationService nebimIntegrationService)
        {
            _nebimIntegrationService=nebimIntegrationService;
        }


        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var nebimParameter = new ProcedureRequest
            {
                ProcName = "usp_B2B_Products",
                Parameters = new List<ProcParameterModel>
                {

                }
            };
            var productList = _nebimIntegrationService.RunNebimProc<List<GetB2BProductsResponse>>(nebimParameter);

            if (productList.Any())
            return Ok(productList);
            else 
            return NotFound();
        }
    }
}