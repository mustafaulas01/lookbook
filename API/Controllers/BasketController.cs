using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : Controller
    {
        private readonly IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository=basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>>GetBasketById(string id)
        {
          var basket=await _basketRepository.GetBasketAsync(id);
          //If there is not such  a id create a empty basket which has a new Id
          return Ok(basket??new CustomerBasket(id));
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var updatedBasket= await _basketRepository.UpdateBasketAsync(basket);

            return Ok(updatedBasket);
        }
        [HttpDelete]
        public async Task DeleteBasketAsync(string id) 
        {
          
          await _basketRepository.DeleteBasketAsync(id);
        }
    }
}