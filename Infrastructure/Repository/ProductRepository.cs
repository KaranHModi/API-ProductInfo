using Delta.Infrastructure.Abstraction;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Infrastructure.Services
{
    public class ProductRepository : IProductService
    {
        private readonly MyDBContext _myDBContext;

        public ProductRepository(MyDBContext myDBContext)
        {
            _myDBContext = myDBContext;
        }

        /// <summary>
        /// Get All Product List
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await _myDBContext.Products.ToListAsync();
        }

        /// <summary>
        /// Get Product by productId
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Product> GetProductById(int Id)
        {
            return await _myDBContext.Products.FindAsync(Id);
        }

        /// <summary>
        /// Add Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<bool> AddProduct(Product product)
        {
            try {
                _myDBContext.Products.Add(product);
                await _myDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<bool> UpdateProduct(Product product)
        {
            try {
                _myDBContext.Products.Update(product);
                await _myDBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteProduct(int Id)
        {
            try {
                var objProduct = await _myDBContext.Products.FindAsync(Id);
                if (objProduct != null)
                {
                    _myDBContext.Products.Remove(objProduct);
                    await _myDBContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
