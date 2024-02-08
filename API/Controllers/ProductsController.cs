using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dto;
using Business;
using Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly INebimIntegrationService _nebimIntegrationService;
        private readonly string cacheKey="product_list";

        private  List<GetB2BProductsResponse> productList=new List<GetB2BProductsResponse>();

        public ProductsController(INebimIntegrationService nebimIntegrationService,IMemoryCache memoryCache)
        {
            _nebimIntegrationService=nebimIntegrationService;
            _memoryCache=memoryCache;
        }


        [HttpGet]
        public IActionResult GetAllProducts([FromQuery]string?gender,[FromQuery]string?category,[FromQuery]string? sort,
        [FromQuery] int pageNumber=1,[FromQuery] int pageSize=12,[FromQuery] string? search=null,bool?isAscending=true)
        {
        
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
        

            if(!string.IsNullOrEmpty(search))
            search=search.ToUpper();

             bool isAsc = isAscending?? false;

            if(string.IsNullOrEmpty(search)==false)
            {
                productList=productList.Where(a=>a.ProductName.ToUpper().Contains(search)).ToList();
            }

            if(string.IsNullOrEmpty(sort)==false)
            {
                if(sort.Equals("Name"))
                {
                    productList=isAsc? productList.OrderBy(a=>a.ProductName).ToList():productList.OrderByDescending(a=>a.ProductName).ToList();
                }
                //price olsa ona göre de devam edebilir.
            }

            /*

    {name:'All',value:'All'},
    {name:'Accessories',value:'Accessories'},
    {name:'Apparel',value:'Apparel'},
    {name:'Bags',value:'Bags'},
    {name:'Shoes',value:'Shoes'}
            */

//Cinsiyet
            if (gender == "Women")
                productList = productList.Where(a => a.Gender == "Women").ToList();
            else if (gender == "Men")
                productList = productList.Where(a => a.Gender == "Men").ToList();
            else if (gender == "Kids")
                productList = productList.Where(a => a.Gender == "Kids").ToList();
            else if (gender == "Unisex")
                productList = productList.Where(a => a.Gender == "Unisex").ToList();
            //kategori

            if (category == "Accessories")
                productList = productList.Where(a => a.MainGroup == "Accessories").ToList();
            else if (category == "Apparel")
                productList = productList.Where(a => a.MainGroup == "Apparel").ToList();
            else if (category == "Bags")
                productList = productList.Where(a => a.MainGroup == "Bags").ToList();
            else if (category == "Shoes")
                productList = productList.Where(a => a.MainGroup == "Shoes").ToList();

            var totalcount = productList.Count;
            var skipResults=(pageNumber-1)*pageSize;

             productList=    productList.Skip(skipResults).Take(pageSize).ToList();
         
            var pResponse = new ProductResponseDto()
            {
                Data = productList,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalcount

            };

       
            return Ok(pResponse);
       
        }

        [HttpGet]
        [Route("{id}")]
       
        public IActionResult GetProduct([FromRoute] string id)
        {
                   
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
            
            var product=productList.FirstOrDefault(a=>a.ProductCode==id);

            if(product!=null)
            return Ok(product);
            else
            return NotFound(); 

        }
    }
}