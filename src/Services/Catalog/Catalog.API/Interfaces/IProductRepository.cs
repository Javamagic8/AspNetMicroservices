using Catalog.API.Entities;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Catalog.API.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(string Id);
        Task<IEnumerable<Product>> GetProductByName(string Name);
        Task<IEnumerable<Product>> GetProductByCategory(string CategoryName);

        Task CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string Id);
    }
}
