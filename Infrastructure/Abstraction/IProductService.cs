using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Infrastructure.Abstraction
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProduct();

        Task<Product> GetProductById(int Id);

        Task<bool> AddProduct(Product product);

        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int Id);
    }
}
