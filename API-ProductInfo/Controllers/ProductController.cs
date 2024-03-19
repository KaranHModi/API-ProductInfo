using Delta.Infrastructure.Abstraction;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API_ProductInfo.Controllers
{
    [ApiController]
    [Route("controller/api")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        /// <summary>
        /// counstructor
        /// </summary>
        /// <param name="productService"></param>
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get All Product List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> GetAllProduct()
        {
            try
            {
                var lstProduct = await _productService.GetAllProduct();
                if (lstProduct != null)
                {
                    return Ok(lstProduct);
                }
                else
                {
                    return NotFound("Product Not Found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get Product details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("products/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            if (id == 0)
                return StatusCode(StatusCodes.Status400BadRequest, "Product Id required");

            try
            {
                var lstProduct = await _productService.GetProductById(id);
                if (lstProduct != null)
                {
                    return Ok(lstProduct);
                }
                else
                {
                    return NotFound("Product Not Found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add Products
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("products")]
        public async Task<IActionResult> SaveProduct(Product product)
        {
            if (string.IsNullOrEmpty(product.Name))
                return StatusCode(StatusCodes.Status400BadRequest, "Name is required");
            if (product.Price == 0)
                return StatusCode(StatusCodes.Status400BadRequest, "Price is required");
            if (string.IsNullOrEmpty(product.Color))
                return StatusCode(StatusCodes.Status400BadRequest, "Product Color is required");
            if (string.IsNullOrEmpty(product.Model))
                return StatusCode(StatusCodes.Status400BadRequest, "Model is required");

            try {
                var lstProduct = await _productService.GetAllProduct();
                int cntProduct = lstProduct != null ? lstProduct.Where(x => x.Name.ToLower() == product.Name.ToLower()).Count() : 0;
                if (cntProduct == 0)
                { 
                    bool result = await _productService.AddProduct(product);
                    if (result)
                    {
                        return Ok("Product Insert successfully!!");
                    }
                    else {                        
                        return StatusCode(StatusCodes.Status400BadRequest, "Error occured");
                    }
                }
                else {
                    return StatusCode(StatusCodes.Status400BadRequest, "Product already exits");                    
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("products/{id}")]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            if (string.IsNullOrEmpty(product.Name))
                return StatusCode(StatusCodes.Status400BadRequest, "Name is required");
            if (product.Price == 0)
                return StatusCode(StatusCodes.Status400BadRequest, "Price is required");
            if (string.IsNullOrEmpty(product.Color))
                return StatusCode(StatusCodes.Status400BadRequest, "Product Color is required");
            if (string.IsNullOrEmpty(product.Model))
                return StatusCode(StatusCodes.Status400BadRequest, "Model is required");

            try
            {
                var objProduct = await _productService.GetProductById(product.Id);
                if (objProduct != null)
                {
                    bool result = await _productService.UpdateProduct(product);
                    if (result)
                    {
                        return Ok("Product Updated successfully!!");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "Error occured");
                    }
                }
                else 
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Product Not Found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);                
            }
        }

        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id == 0)
                return StatusCode(StatusCodes.Status400BadRequest, "ProductId is required");

            try {
                var objProduct = await _productService.GetProductById(id);
                if (objProduct != null)
                {
                    var result = await _productService.DeleteProduct(id);
                    if (result)
                    {
                        return Ok("Product Deleted");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "Error occured");
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Product Not Found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}
