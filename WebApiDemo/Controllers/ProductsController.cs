using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.DataAccess;
using WebApiDemo.Entities;

namespace WebApiDemo.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        IProductDal _productDal;

        public ProductsController(IProductDal productDal)
        {
            _productDal = productDal;
        }


        [HttpGet("")]
        public IActionResult Get()
        {
            var product = _productDal.GetList();
            return Ok(product);
        }
        [HttpGet("{productId?}")]
        public IActionResult Get(int productId)
        {
            try
            {
                var product = _productDal.Get(filter => filter.ProductId == productId);
                if (product == null)
                {
                    return NotFound($"There is no product with id= {productId} ");
                }
                return Ok(product);
            }

            catch (Exception) { }

            return BadRequest();
        }
        public IActionResult Post(Product product)
        {
            try
            {
                _productDal.Add(product);
                return new StatusCodeResult(201);
            }
            catch (Exception)
            {
            }
            return BadRequest();
        }
        [HttpPut]
        public IActionResult Put(Product product)
        {
            try
            {
                _productDal.Update(product);
                return Ok(product);
            }
            catch
            {
            }
            return BadRequest();
        }
        [HttpDelete("{productId}")]
        public IActionResult Delete(int productId)
        {
            try
            {
                _productDal.Delete(new Product { ProductId = productId });
                return Ok();
            }
            catch
            {
            }
            return BadRequest();
        }
        [HttpGet("GetProductDetails")]
        public IActionResult GetProductWithDetails()
        {
            try
            {
                return Ok(_productDal.GetProductWithDetails());
            }
            catch
            { 
            }
            return BadRequest();
        }
    }
}
