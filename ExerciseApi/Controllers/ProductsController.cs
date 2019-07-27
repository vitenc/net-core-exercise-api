using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExerciseApi.Models;
using ExerciseApi.ServiceLayer;


namespace ExerciseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _products;

        public ProductsController(IProductService products)
        {
            _products = products;
        }

        // GET api/products - ALL
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return await _products.GetAll();
        }
        
         // GET api/products/Active
        [HttpGet]
        //Custom route for Active Products
        [Route("/api/products/ActiveProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> ActiveProducts()
        {
            return await _products.GetActive();
        }

        // GET api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            return await _products.GetById(id);
        }

        

        // POST api/products
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Product value)
        {
            await _products.Add(value);
            return Ok();
        }

        // PUT api/products/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] Product value)
        {
            await _products.Update(id, value);            
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _products.Delete(id);
            return Ok();
        }
    }
}
